using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    public class AStar
    {
        private List<Node> considerNodes;
        private List<Node> visitedNodes;
        private Stack<Action> plan;
        private List<Action> actions;

        public AStar(List<Action> actions)
        {
            this.actions = actions;
            considerNodes = new List<Node>();
            visitedNodes = new List<Node>();
            plan = new Stack<Action>();
        }


        private Node GetMatchingNodeInConsidered(WorldState ws)
        {
            foreach (Node n in considerNodes)
            {
                if (n.ws.AreEqual(ws)) return n;
            }
            return null;
        }

        private Node GetMatchingNodeInVisited(WorldState ws)
        {
            foreach (Node n in visitedNodes)
            {
                if (n.ws.AreEqual(ws)) return n;
            }
            return null;
        }


        private List<Action> GetPossibleTransitions(WorldState from, ref List<WorldState> tos)
        {
            List<Action> transitions = new List<Action>();
            foreach (Action a in actions)
            {
                if (from.PreConditionsMeet(a.preConditions))
                {
                    transitions.Add(a);
                    tos.Add(from.GetWSWithEffectsApplied(a.effects));
                }
            }
            return transitions;
        }

        private Stack<Action> ReconstructPlan(Node goalNode)
        {
            Node currentNode = goalNode;
            // The start Node's action has been set to null
            while (currentNode != null && currentNode.action != null)
            {
                plan.Push(currentNode.action);
                // Trace the path back through the parent
                currentNode = GetMatchingNodeInVisited(currentNode.parentWS);
            }
            return plan;
        }




        public Stack<Action> GetPlan(WorldState start, Goal currentGoal)
        {
            considerNodes.Clear();
            visitedNodes.Clear();
            WorldState goal = currentGoal.condition;
            // Create a Node to encapsualte the start World State
            Node n0 = new Node();
            n0.ws = start;
            n0.parentWS = start;
            // Cost to get to this node from start
            n0.g = 0; // g = guess
            // A guess as to how far we are from the goal
            n0.h = start.GetNumberMismatchingAtoms(goal);
            // Guess of overall cost from start to goal
            n0.f = n0.g + n0.h;
            // The Action associated with the Node
            n0.action = null;
            // Add the Node to consider
            considerNodes.Add(n0);

            do
            {
                if (considerNodes.Count == 0)
                {
                    Debug.Log("Did not find a path");
                    return null;
                }
                // Search Open List for Node with the lowest guested cost (closest to Goal).
                int lowestVal = 100000;
                Node lowestNode = null;
                foreach (Node n in considerNodes)
                {
                    if (n.f < lowestVal)
                    {
                        lowestVal = n.f;
                        lowestNode = n;
                    }
                }

                // Set the lowest cost Node as the current Node
                Node currentNode = lowestNode;
                // Remove Node 
                considerNodes.Remove(lowestNode);
                // If the current Node's World State match the Goal we are finished
                if (currentNode.ws.GoalAchieved(goal))
                {
                    return ReconstructPlan(currentNode);
                }
                // Add Current Node to visited List
                visitedNodes.Add(currentNode);
                List<WorldState> tos = new List<WorldState>();
                List<Action> transitionActions = GetPossibleTransitions(currentNode.ws, ref tos);


                int index = 0;
                // For each of currents Node's adjacent Actions        
                foreach (Action a in transitionActions)
                {
                    // Get the Actions World State after effects have been applied
                    WorldState to = tos[index];
                    index++;
                    // Calcuate the cost from current to the completed adjacent Action
                    int cost = currentNode.g + a.Cost;
                    // The Node may already be under consideration
                    Node openNode = GetMatchingNodeInConsidered(to);
                    // The Node may already have been processed
                    Node closedNode = GetMatchingNodeInVisited(to);
                    // If already under consideration check the cost as it may be cheaper coming via this route
                    if (openNode != null && cost < openNode.g)
                    {
                        considerNodes.Remove(openNode);
                        // Force the Node to be created 
                        openNode = null;
                    }
                    // if the Node has been visited check the cost as it may be cheaper coming via this route
                    if (closedNode != null && cost < closedNode.g)
                    {
                        visitedNodes.Remove(closedNode);
                    }



                    // If adjacent Action not in visited or considered Lists
                    if (openNode == null && closedNode == null)
                    {
                        // Ecapsulate the adjacent Action in a Node
                        Node nb = new Node();
                        // The World State after Action effects are applied
                        nb.ws = to;
                        nb.g = cost;
                        // Number of mismatched Atoms between goal and current Node.
                        // A heuristic (guess) measure to how close we are to Goal.
                        nb.h = nb.ws.GetNumberMismatchingAtoms(goal);
                        nb.f = nb.g + nb.h;
                        nb.action = a;
                        // The World State before Action effects were applied
                        // This allows us to trace our way back to the start
                        nb.parentWS = currentNode.ws;
                        // Add Node to List for consideration
                        considerNodes.Add(nb);
                    }
                }
            } while (true);
        }










    }
}
