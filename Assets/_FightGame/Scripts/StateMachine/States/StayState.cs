﻿using UnityEngine;
using System.Collections;

public class StayState : State {

    private Animator animator;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        this.name = "StayState";
    }

    protected override void AddListeners()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, StateMachineEvent.HandleParamers);
    }

    protected override void RemoveListeners()
    {
        NotificationCenter.DefaultCenter.RemoveObserver(this, StateMachineEvent.HandleParamers);
    }

    void HandleParamers(NotificationCenter.Notification info)
    {
        StateMachineParams data = (StateMachineParams)info.data;
        animator.SetInteger("stayState", data.stayState);
        //if (!data.notMove)
        //{
        //    data.notMove = true;
        //    //Debug.Log("转换到了staty");
        //}
    }


}
