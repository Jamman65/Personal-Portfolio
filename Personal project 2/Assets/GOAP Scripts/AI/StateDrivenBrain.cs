using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GOAP;
using JO;


public class StateDrivenBrain : BasicAIController
{
    private int healthLevel;
    protected float thinkInterval = 0.25f;
    [HideInInspector]
    public AIStateWeaponController aiStateWeaponController;

    public UnityEngine.AI.NavMeshAgent navmeshAgent;
    public enum GOAPStates { Destination, Animate, Interact };
    [HideInInspector]
    public FSM<GOAPStates> GOAPStateMachine;
    // Toggle on/off tactical state behaviour. 
    public bool tacticalStateActive = true;
    private EnergyManager energyManager;
    public bool displayFSMTransitions = false;
    // A list of all actions
    private List<Action> actions;
    // The current action being processed by the current state
    [HideInInspector]
    public Action currentAction;


    // Contains the actions required to complete the current goal
    public Stack<Action> plan;
    public Transform hand;

    [HideInInspector]
    public WorldState startWS;

    private List<Goal> goals;

    public Goal currentGoal;
    public EnemyAnimator enemyanim; 
    public EnemyWeaponSlotManager weaponslot; //Defines the slot for the weapon to be placed in
    public WeaponItem Sword; //Defines the Weapon item that the AI will equip
    public WeaponItem Shield;
    public GameObject weapon;
    public GameObject ShieldWeapon;
    public PlayerStats playerstats;
    public GameObject RightHandWeapon;
    public InputHandler input;
    public bool IsBlock;
    public Transform RotationPoint;
    public int rotationspeed = 10;
    public int RollCount = 3; //Controls the amount of dodges the AI can perform
    public int BlockCount = 2; //Controls the amount of Blocks the AI can perform
    public bool blocking;












    protected void Awake()
    {
        weapon.SetActive(false);
        
       

        // Set the current World State
        startWS = new WorldState();
        // What the AI knows about the environment
        // Controls the worldstate for each action and allows the AI to know about the current environment and perform a specific action
        startWS.SetValue(Atom.KnowledgeOfWeapon, true);
        startWS.SetValue(Atom.KnowledgeOfShield, true);
        startWS.SetValue(Atom.CanDodge, true);
        startWS.SetValue(Atom.CanBlock, true);
        


        actions = new List<Action>();

        Action attackPlayer = new AttackPlayer("Attack Player", 1, this, GOAPStates.Destination);//This creates a new action for the GOAP plan 
        attackPlayer.SetPreCondition(Atom.HaveWeapon, true); //Preconditions control which action is going to be executed next since an action will only 
        attackPlayer.SetPreCondition(Atom.HaveShield, true); //perform if the AI has the correct preconditions
        attackPlayer.SetPreCondition(Atom.PlayerFound, true);
        attackPlayer.SetPreCondition(Atom.Dodge, true);
        attackPlayer.SetPreCondition(Atom.Block, true);
        attackPlayer.SetEffect(Atom.PlayerDead, true); //This sets the effect for the next action allowing the worldstate to change for the AI when the action is complete
        attackPlayer.destination = GameObject.FindGameObjectWithTag(Tags.Player).transform; //Destination allows the AI to know where to navigate to in the action
        actions.Add(attackPlayer);

        Action FindPlayer = new FindPlayer("Find Player", 3, this, GOAPStates.Destination);
        FindPlayer.SetPreCondition(Atom.HaveWeapon, true);
        FindPlayer.SetPreCondition(Atom.HaveShield, true);
        FindPlayer.SetEffect(Atom.PlayerFound, true);
        FindPlayer.destination = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        actions.Add(FindPlayer);

        Action Dodge = new Dodge("Dodge", 1, this, GOAPStates.Destination);
        Dodge.SetPreCondition(Atom.HaveWeapon, true);
        Dodge.SetPreCondition(Atom.HaveShield, true);
        Dodge.SetPreCondition(Atom.PlayerFound, true);
        Dodge.SetPreCondition(Atom.CanDodge, true);
        Dodge.SetEffect(Atom.Dodge, true);
        Dodge.destination = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        actions.Add(Dodge);

        Action Block = new Block("Block", 1, this, GOAPStates.Interact);
        Block.SetPreCondition(Atom.HaveWeapon, true);
        Block.SetPreCondition(Atom.HaveShield, true);
        Block.SetPreCondition(Atom.PlayerFound, true);
        Block.SetPreCondition(Atom.CanBlock, true);
        Block.SetEffect(Atom.Block, true);
        Block.destination = GameObject.FindGameObjectWithTag(Tags.Player).transform;
        actions.Add(Block);

       




        Action idle = new Idle("Idle", 1, this, GOAPStates.Animate); 
        idle.SetEffect(Atom.OnGuard, true);
        actions.Add(idle);


        Action GetWeapon = new GetWeapon("Get Weapon", 2, this, GOAPStates.Destination);
        GetWeapon.SetPreCondition(Atom.KnowledgeOfWeapon, true);
        GetWeapon.SetEffect(Atom.HaveWeapon, true);
        GetWeapon.destination = GameObject.Find("Sword2").transform;
        actions.Add(GetWeapon);

        Action GetShield = new GetShield("Get Shield", 1, this, GOAPStates.Destination);
        GetShield.SetPreCondition(Atom.KnowledgeOfShield, true);
        GetShield.SetPreCondition(Atom.HaveWeapon, true);
        GetShield.SetEffect(Atom.HaveShield, true);
        GetShield.destination = GameObject.Find("ShieldPickup").transform;
        actions.Add(GetShield);


        goals = new List<Goal>(); //This creates a new list for Goals to be created for the GOAP Plan 
        Goal idleGoal = new Goal(1);
        idleGoal.condition.SetValue(Atom.OnGuard, true);
        goals.Add(idleGoal);
        Goal combatGoal = new Goal(10);
        combatGoal.condition.SetValue(Atom.HaveWeapon, true); //These condtions specify which action will be executed first since the Goal will match the actions effect
        combatGoal.condition.SetValue(Atom.HaveShield, true);
        combatGoal.condition.SetValue(Atom.PlayerFound, true);
        combatGoal.condition.SetValue(Atom.Dodge, true);
        combatGoal.condition.SetValue(Atom.Block, true);
        combatGoal.condition.SetValue(Atom.PlayerDead, true);
         // ALL OF THESE ARE EFFECTS FROM THE ACTIONS TO ALLOW THE GOALS TO BE COMPLETED
        goals.Add(combatGoal); //This adds a goal to the GOAP plan with all the conditions included










        base.Awake();
        // Build the Finite State Machine
        GOAPStateMachine = new FSM<GOAPStates>(displayFSMTransitions);
        GOAPStateMachine.AddState(new Destination<GOAPStates>(GOAPStates.Destination, this, 0f));
        GOAPStateMachine.AddState(new Animate<GOAPStates>(GOAPStates.Animate, this, 0f));
        GOAPStateMachine.AddState(new Interact<GOAPStates>(GOAPStates.Interact, this, 0f));

        GOAPStateMachine.AddTransition(GOAPStates.Destination, GOAPStates.Animate);
        GOAPStateMachine.AddTransition(GOAPStates.Destination, GOAPStates.Interact);
        GOAPStateMachine.AddTransition(GOAPStates.Destination, GOAPStates.Destination);
        GOAPStateMachine.AddTransition(GOAPStates.Animate, GOAPStates.Destination);
        GOAPStateMachine.AddTransition(GOAPStates.Animate, GOAPStates.Interact);
        GOAPStateMachine.AddTransition(GOAPStates.Animate, GOAPStates.Animate);
        GOAPStateMachine.AddTransition(GOAPStates.Interact, GOAPStates.Destination);
        GOAPStateMachine.AddTransition(GOAPStates.Interact, GOAPStates.Animate);
        GOAPStateMachine.AddTransition(GOAPStates.Interact, GOAPStates.Interact);


    }

    private Goal GetGoal()
    {
        Goal temp = null;
        foreach (Goal g in goals)
        {
            if (temp == null)
            {
                temp = g;
            }
            else
            {
                if (temp.priority < g.priority)
                {
                    temp = g;
                }
            }
        }
        return temp;
    }

    public void GenerateAStarPlan()
    {
        Debug.Log("Generating Plan");
        AStar aStar = new AStar(actions);
        // Generate the plan that meets the highest priority goal
        currentGoal = GetGoal();
        plan = aStar.GetPlan(startWS, currentGoal);
        // Goal can not be achieved
        if (plan == null)
        {
            Debug.Log("Assigning Default Goal");
            // The default goal being the first in the List
            // Rather crude solution.
            currentGoal = goals[0];
            plan = aStar.GetPlan(startWS, currentGoal);
            // Display the plan
            foreach (Action a in plan)
            {
                Debug.Log("Action : " + a.Name);
            }
            // No need to Pop the Action off the plan as this will be performed by the
            // Current state's OnLeave call-back.
            return;
        }
        // Display the plan
        foreach (Action a in plan)
        {
            Debug.Log("Action : " + a.Name);
        }
        // Get the first action
        currentAction = plan.Pop();
        // Manually set the state for the first action
        GOAPStateMachine.SetInitialState(currentAction.MoveToState);
    }

    private void CheckPlan()
    {
        // Don't generate a plan until the state has finished
        if (GOAPStateMachine.CurrentState.StateFinished)
        {
            // The plan has completed
            if (plan.Count == 0)
            {
                // Set it to a low priority to ensure higher priority goals are meet first
                // This is an area that requires additional work.
                currentGoal.priority = 0;
                // Generate a new plan.
                GenerateAStarPlan();
            }
            // The last action failed
            if (GOAPStateMachine.CurrentState.ActionStatus == ActionStates.Failed)
            {
                GenerateAStarPlan();
            }
        }
    }





    public void Start()
    {
        base.Start();
        RightHandWeapon = GameObject.Find("Sword_OH(Clone)"); //Assigns the weapon to the weapon in the scene
        RightHandWeapon.SetActive(false);
        // Generate the plan
        GenerateAStarPlan();
        aiStateWeaponController = GetComponent<AIStateWeaponController>();
        energyManager = GetComponent<EnergyManager>();
        navmeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void Update()
    {
        base.Update();
        if (tacticalStateActive & plan != null && GOAPStateMachine.CurrentState != null)
        {
            GOAPStateMachine.CurrentState.Act();
            CheckPlan();
            GOAPStateMachine.Check();
        }
    }




    // Transition methods for FSM. Note it is possible to transist to the same state.
    // The States themselves determine when they should transist based on their interactions with the coresponding Action.
    // The State to transist to is determined by the State of the next Action in the Stack.
    public bool GuardDestinationToAnimate(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Animate);
    }
    public bool GuardDestinationToInteract(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Interact);
    }
    public bool GuardDestinationToDestination(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Destination);
    }
    public bool GuardAnimateToDestination(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Destination);
    }
    public bool GuardAnimateToInteract(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Interact);
    }
    public bool GuardAnimateToAnimate(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Animate);
    }
    public bool GuardInteractToDestination(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Destination);
    }
    public bool GuardInteractToAnimate(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Animate);
    }
    public bool GuardInteractToInteract(State<GOAPStates> currentState)
    {
        return (currentState.StateFinished && plan.Peek().MoveToState == GOAPStates.Interact);
    }


    // Ensure current tactical state is notified when a trigger is entered
    protected virtual void OnTriggerEnter(Collider collider)
    {
        GOAPStateMachine.CurrentState.OnStateTriggerEnter(collider);
    }

  


}
