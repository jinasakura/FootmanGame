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

    protected virtual void AddListeners()
    {

    }

    protected virtual void RemoveListeners()
    {

    }
}
