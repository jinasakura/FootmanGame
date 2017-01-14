using UnityEngine;
using System.Collections;
using FightDemo.ThirdPerson;

public class SkillTextButton : MonoBehaviour {

    private UltimateButton btn;
    private FootmanCharacter _character;
    public FootmanCharacter character
    {
        set { _character=value; }
    }

    private string _btnName;
    public string btnName
    {
        set { _btnName = value; }
        get { return _btnName; }
    }

    private int _onceActionType;
    public int onceActionType
    {
        set { _onceActionType = value; }
        get { return _onceActionType; }
    }

	// Use this for initialization
	void Start () {
        if (_character == null)
        {
            Debug.LogWarning("没有给技能按钮关联上对象");
        }
	}
	
	// Update is called once per frame
	void Update () {
        //if (UltimateButton.GetButtonDown(btnName)!=null && UltimateButton.GetButtonDown(btnName))
        //{
        //    _character.onceActionType = onceActionType;
        //    _character.isTrigger = true;
        //    Debug.Log("按下"+btnName);
        //}
    }
}
