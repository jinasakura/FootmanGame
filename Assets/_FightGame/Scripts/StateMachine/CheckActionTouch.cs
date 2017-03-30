using UnityEngine;
using System.Collections;


/// <summary>
/// 检测技能动作发生期间,动作属于击中的一段时间
/// </summary>
public class CheckActionTouch : MonoBehaviour {

    
    private bool _touched;
    public bool touched { set; get; }

    private int _skillId;
    public int skillId { set; get; }

    void Start()
    {
        touched = false;
        skillId = 0;
    }

    void TouchEnemy(int id)
    {
        touched = true;
        skillId = id;
        //Debug.Log("touched");
    }

    void LeaveEnemy(int id)
    {
        touched = false;
        skillId = id;
    }

}
