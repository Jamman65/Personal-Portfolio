using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;



namespace Scheduler {
    // Manages a list of schedule entries
    // Each schedule entry has one or more actions
    // Actions implement the agent's behaviour
    // Entries run for a set period of time
    // If an action is running when the entry time has elapsed the entry will complete when the action ends.
    // If all entry actions have finished and the schedule is still running the default action will be executed.
    // Schedule enties will wrap
    // Schedules, Entries and Actions are instantiated from an XML document
    public class Schedule {
        [XmlAttribute("id")]
        public int ID;
        [XmlArray("Entries")]
        [XmlArrayItem("Simple", typeof(SimpleScheduleEntry))]
        [XmlArrayItem("Random", typeof(WeightedRandomScheduleEntry))]
        [XmlArrayItem("Sequential", typeof(SequentialScheduleEntry))]
        public List<ScheduleEntry> entries;
        // The current schedule enty index 
        private int entryIndex;
        // The action that is currenty executing
        private Action currentAction;
        // The GameObject that the schedule has been applied to
        private GameObject agent;
        // When the entry started
        private float entryStartTime;
        // State of current action
        Action.ActionStates actionStatus;

        // Constructor invoked from XML
        public Schedule(){
            this.entryIndex = -1;
            entryStartTime = 0f;
        }

        // Non XML approach to instantiation
        public void AddEntry(ScheduleEntry entry){
            entries.Add(entry);
        }

        // Is the schedule entry running
        private bool EntryIsLive(float currentTime) {
            // Has the current entry run time elapsed
            if (entries[entryIndex].IsFinished(entryStartTime)) return false;
            return true;
        }

        // The entry is running the default action
        private bool EntryRunningDefaultAction() {
            return entries[entryIndex].IsDefaultAction;
        }

        // Return the next schedule entry index
        private int GetNextEntryIndex(int index) {
            ++index;
            // If all schedules have been executed then loop back to first entry
            if (index >= entries.Count) {
                index = 0;
            }
            entryStartTime = Time.time;
            return index;
        }

        // Get the enties next action
        private Action ChooseNewAction(Action lastAction) {
            return entries[entryIndex].ChooseNewAction(lastAction);
        }

        // Invoked before the schedule starts executing the entries
        // The schedule starts executing the enty with the index, passed as a parameter
        public void Initialise(GameObject agent, int index) {
            // Is the index valid
            if (index < entries.Count) {
                this.agent = agent;
                entryIndex = index;
                currentAction = ChooseNewAction(null);
                currentAction.Initialise(agent);
            }
            else {
                Debug.LogError("Initial Schedule Entry Index is out of range");
            }
        }

        // Manages the transitions from schedule entries and actions
        public void Update() {
            if (entryIndex == -1 || agent == null) {
                Debug.LogError("Schedule must be Initialised");
                return;
            }
            // Execute action code
            actionStatus = currentAction.Update();
            // The schedule is still running
            if (EntryIsLive(Time.time)) {
                // The action has finished and the default action not playing
                // Default actions do not finish but run until the end of the schedule entry
                if (actionStatus == Action.ActionStates.Success && !EntryRunningDefaultAction()) {
                    currentAction.Terminate(agent);
                    // Start the next action
                    currentAction = ChooseNewAction(currentAction);
                    currentAction.Initialise(agent);
                }
            }
            else {
                //Debug.Log("Entry Finished 1 " + actionStatus);
                // The action and schedule have finished 
                if (actionStatus == Action.ActionStates.Success) {
                    currentAction.Terminate(agent);
                  //  Debug.Log("Entry Finished 2");
                    // Get the next schedule index
                    entryIndex = GetNextEntryIndex(entryIndex);
                  //  Debug.Log("Get next entry : " + entries[entryIndex].ToString() + " Time : " + Time.time);
                    // Get the schedule's first action
                    currentAction = ChooseNewAction(currentAction);
                    currentAction.Initialise(agent);
                }
            }
        }

        // Invoked when an Action's animation has ended
        public void OnActionAnimationEnd() {
            // Inform the action that the animation has completed
            currentAction.OnAnimationEnd();
        }

        // Invoked when an Action's animation has ended
        public void OnTriggerEnter(Collider other) {
            // Inform the action that the animation has completed
            currentAction.OnTriggerEnter(other);
        }

        public void Print(){
            foreach (ScheduleEntry se in entries) {
                Debug.Log("Schedule Entry : " + se.ToString());
                se.Print();
            }
        }

}


}

