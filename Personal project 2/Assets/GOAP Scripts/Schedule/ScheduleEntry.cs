using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Scheduler {

    // Manages a list of one or more Actions
    public abstract class ScheduleEntry {
        [XmlAttribute("runTime")]
        public float runTime;
        [XmlArray("Actions")]
        [XmlArrayItem("DrinkBreak", typeof(DrinkBreakAction))]
        [XmlArrayItem("GoToDestination", typeof(GoToDestinationAction))]
        [XmlArrayItem("OpenBox", typeof(OpenBoxAction))]
        [XmlArrayItem("PickUpItem", typeof(PickUpItemAction))]
        [XmlArrayItem("Sit", typeof(SitAction))]
        [XmlArrayItem("Talk", typeof(TalkAction))]
        [XmlArrayItem("UseFax", typeof(UseFaxAction))]
        [XmlArrayItem("Meeting", typeof(MeetingAction))]
        [XmlArrayItem("SitAtWorkstation", typeof(SitAtWorkstationAction))]
        [XmlArrayItem("StandAtWorkstation", typeof(StandAtWorkstationAction))]
        [XmlArrayItem("Text", typeof(TextAction))]
        public List<Action> actions;
        protected int currentActionIndex;
        protected int actionsExecuted;
        protected Action defaultAction;
        public bool IsDefaultAction { get {return (defaultAction != null);}}

        // Constructor invoked from XML
        public ScheduleEntry() {
            currentActionIndex = -1;
            actionsExecuted = 0;
            defaultAction = null;
        }

        // Non XML approach to instantiation
        public ScheduleEntry(float runTime) {
            this.runTime = runTime;
            actions = new List<Action>();
            currentActionIndex = -1;
            actionsExecuted = 0;
            defaultAction = null;
        }

        // Non XML approach to populating Actions list
        public abstract void AddAction(Action action);

        // Get the next entry action
        public abstract Action ChooseNewAction(Action lastAction);

        // Schedule Entry run time has elapsed
        public virtual bool IsFinished(float startTime) {
           // Debug.Log("Entry time left : " + ((startTime + runTime) - Time.time));
            bool finished = (Time.time > (startTime + runTime));
            // Reset members incase schedule enty repeats
            if (finished) {
                currentActionIndex = -1;
                actionsExecuted = 0;
                defaultAction = null;
            }
            return finished;
        }

        public void Print() {
            foreach (Action a in actions) {
                Debug.Log("     Action : " + a.ToString());
            }
        }
    }

}