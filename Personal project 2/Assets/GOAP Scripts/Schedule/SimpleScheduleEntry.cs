using UnityEngine;
using System.Collections;

namespace Scheduler {

    // Executes a single action
    public class SimpleScheduleEntry : ScheduleEntry {

        // Constructor invoked from XML
        public SimpleScheduleEntry() {}

        // Non XML approach to instantiation
        public SimpleScheduleEntry(float startTime):base(startTime){}

        // Non XML approach to populating Actions list
        public override void AddAction(Action action) {
            actions.Add(action);
        }

        // When first invoked execute the first action in the list
        // Subsequent invokations run the default action
        public override Action ChooseNewAction(Action lastAction) {
            // If the last action has finihed and this was not the default action
            if (actionsExecuted >= 1 && defaultAction == null) {
                actionsExecuted++;
                // Instantiate and run the default action
                defaultAction = new DefaultAction();
                return defaultAction;
            }
            else {
                actionsExecuted++;
                return actions[0];
            }
        }
    }

}