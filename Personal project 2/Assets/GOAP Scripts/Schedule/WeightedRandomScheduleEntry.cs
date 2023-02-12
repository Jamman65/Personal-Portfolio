using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Scheduler {

    // Picks an action randomly from a list of actions
    public class WeightedRandomScheduleEntry : ScheduleEntry {
        private int totalWeight;

        // Constructor invoked from XML
        public WeightedRandomScheduleEntry() { }

        // Non XML approach to instantiation
        public WeightedRandomScheduleEntry(float startTime) : base(startTime) {
            totalWeight = 0;
        }

        // Non XML approach to populating Actions list
        public override void AddAction(Action action) {
            actions.Add(action);
            totalWeight += action.weight;
        }

        // Get a random Action.
        // The actions weight influences its chance of being selected.
        public override Action ChooseNewAction(Action lastAction) {
            // XML version circumvents AddAction method so total weights must be calculated here
            totalWeight = 0;
            // Calculate total weights
            foreach (Action a in actions) {
                totalWeight += a.weight;
            }
            int random = Random.Range(0, totalWeight);
            int cumlativeTotal = 0;
            int index = actions.Count - 1;
            foreach (Action action in actions) {
                cumlativeTotal += action.weight;
                if (random < cumlativeTotal ) {
                    return  action;
                }
            }
            return null;
        }
    }


}