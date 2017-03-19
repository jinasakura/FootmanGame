using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour {

	public virtual void Enter()
    {
        AddListeners();
    }

    public virtual void Exit()
    {
        RemoveListeners();
    }

    protected virtual void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void AddListeners()
    {

    }

    protected virtual void RemoveListeners()
    {

    }

    //原来并未提供有参数传入的情况
    public virtual void HandleParamers(object info)
    {

    }

}
