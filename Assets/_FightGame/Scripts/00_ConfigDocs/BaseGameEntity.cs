using UnityEngine;
using System.Collections;

public class BaseGameEntity : MonoBehaviour {

    public int id;

    private string _eName;
    public string eName
    {
        set
        {
            gameObject.name = value;
            _eName = value;
        }
        get { return _eName; }
    }

}
