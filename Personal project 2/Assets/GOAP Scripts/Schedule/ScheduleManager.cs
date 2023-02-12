using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System;
using Scheduler;


// Limitation: There is only one instance of each schedule with the same id.
// If two agents share a schedule they will shaer the same instance, leading to unpredicatable behaviour.
// One solution would be to implement clone methods in the schedule hierarchy and assigne each agent a unique schedule system.

// This script should be attached to a GameObject accessible by all agents wishing to use the schedule system
public class ScheduleManager : MonoBehaviour {
    [HideInInspector]
    public List<Schedule> schedules;

	void Awake () {
       // schedules = GetManualSchedules();
       schedules = GetXMLSchedules();
	}


    // Agents invoke to aquire a schedule
    public Schedule GetSchedule(int id) {
        return schedules.Find(s => s.ID == id);
    }

    // Create the schedule information anually
  //  public static List<Schedule> GetManualSchedules() {
  //      List<Schedule> manualSchedules = new List<Schedule>();
  //      Schedule schedule = new Schedule();
  //      SimpleScheduleEntry sse1 = new SimpleScheduleEntry(0f);
  //      SimpleScheduleEntry sse2 = new SimpleScheduleEntry(5f);
  //      WeightedRandomScheduleEntry sse3 = new WeightedRandomScheduleEntry(20f);
  //      SequentialScheduleEntry sse4 = new SequentialScheduleEntry(40f);
  //      SimpleScheduleEntry sse5 = new SimpleScheduleEntry(500f);
  //      schedule.AddEntry(sse1);
  //      schedule.AddEntry(sse2);
  //      schedule.AddEntry(sse3);
  //      schedule.AddEntry(sse4);
  //      schedule.AddEntry(sse5);
  ////      sse1.AddAction(new TakeMobileCallAction(1, 2, 6,""));
  //      sse2.AddAction(new GoToDestinationAction(1, 7, 11,"destination1"));
  //      sse3.AddAction(new DrinkBreakAction(20, 7, 11,""));
  //      sse3.AddAction(new TakeMobileCallAction(5, 7, 11,""));
  //      sse3.AddAction(new TalkAction(15, 7, 11,""));
  //      sse4.AddAction(new PickUpItemAction(1, 10, 15,""));
  //      sse4.AddAction(new OpenBoxAction(1, 10, 15,""));
  //      sse5.AddAction(new TalkAction(1, 490, 500,""));
  //      manualSchedules.Add(schedule);
  //      return manualSchedules;
 //   }

    // Load the schedule from an XML document
    public static List<Schedule> GetXMLSchedules() {
        XmlSerializer deserializer = new XmlSerializer(typeof(List<Schedule>), new XmlRootAttribute("ScheduleConfiguration"));
        TextReader textReader = new StreamReader("Assets/Scripts/Schedule/ScheduleData.xml");
        List<Schedule> xmlSchedules;
        // Use the Deserialize method to restore the object's state with data from the XML document. 
        xmlSchedules = (List<Schedule>)deserializer.Deserialize(textReader);
        textReader.Close();
        return xmlSchedules;
    }

}


