using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour {

	public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    protected virtual void OnDestroy()
    {

    }

    //原来并未提供有参数传入的情况
    public virtual void HandleParamers(object info)
    {

    }

}
