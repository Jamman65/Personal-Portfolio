using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Scheduler;

// Attached to Agents whoes behaviour is controlled by the Schedule System
public class ActionScheduler :MonoBehaviour {
    public int scheduleID;
    private Schedule schedule;
    public bool interupt;

	void Start () {
        ScheduleManager scheduleManager = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<ScheduleManager>();
        // Get the schedule
        schedule = scheduleManager.GetSchedule(scheduleID);
        // Initialise the schedule and start at entry 0
        schedule.Initialise(gameObject, 0);
        interupt = false;
	}

	void Update () {
        if (!interupt) {
            // Check the scheule status and update the actions behaviour
            schedule.Update();


        }
        else {
            GetComponent<Animator>().SetTrigger("Duck");
            this.enabled = false;
        }
	}

    // Invoked by animations that are being run by the currently scheduled action
    public void OnActionAnimationEnd() {
        schedule.OnActionAnimationEnd();
    }


    void OnTriggerEnter(Collider other) {
        schedule.OnTriggerEnter(other);
    }



}
