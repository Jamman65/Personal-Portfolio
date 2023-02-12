using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Scheduler {

    // Sequentially executes a list of actions
    public class SequentialScheduleEntry : ScheduleEntry {

        // Constructor invoked from XML
        public SequentialScheduleEntry() {}

        // Non XML approach to instantiation
        public SequentialScheduleEntry(float startTime)
            : base(startTime) { }

        // Non XML approach to populating Actions list
        public override void AddAction(Action action) {
            actions.Add(action);
        }

        // Execute the next action in the list
        public override Action ChooseNewAction(Action lastAction) {
            actionsExecuted++;
            currentActionIndex++;
            // If the last action has finihed and this was not the default action
            if ( actionsExecuted > actions.Count && defaultAction == null) {
                // Instantiate and run the default action
                defaultAction = new  DefaultAction();
                return defaultAction;
            }
            else return actions[currentActionIndex];
        }

    }

}