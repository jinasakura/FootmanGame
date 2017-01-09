using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckOnceAction : StateMachineBehaviour {
    
    public enum State { None, Start, End };

    private State _actionState = State.None;

    public string stateBegin = "OnOnceActionStateBegin";
    public string stateEnd = "OnOnceActionStateEnd";
    //外部链接的prefab
    //public GameObject contact;
    ////将上面的prefab实例化
    ////private GameObject contactObj;
    //private ContactNotices contactNotices;

    void Awake()
    {
        //GameObject contactObj = Instantiate(contact, Vector3.zero, Quaternion.identity) as GameObject;
        //contactNotices = contactObj.GetComponent<ContactNotices>();
    }

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int onceActionType = animator.GetInteger("onceActionType");
        NotificationCenter.Notification message = new NotificationCenter.Notification(animator, stateBegin, onceActionType);
        NotificationCenter.DefaultCenter.PostNotification(message);
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NotificationCenter.DefaultCenter.PostNotification(animator, stateEnd, stateInfo);
    }

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMachineEnter is called when entering a statemachine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
    //
    //}

    // OnStateMachineExit is called when exiting a statemachine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
    //
    //}
}
