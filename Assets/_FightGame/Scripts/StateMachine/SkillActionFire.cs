﻿using UnityEngine;
using System;


/// <summary>
/// 检测技能动作发生期间,动作属于击中或释放法术的一段时间
/// </summary>
public class SkillActionFire : MonoBehaviour
{

    public Action<bool> OnSkillFired { set; get; }

    private bool _touched;
    public bool touched
    {
        get { return _touched; }
        set
        {
            _touched = value;
            Action<bool> localOnChange = OnSkillFired;
            if (localOnChange != null)//如果没有订阅，这里会一直为空
            {
                localOnChange(value);
            }

        }
    }

    private int _skillId;
    public int skillId { set; get; }

    void Start()
    {
        touched = false;
        skillId = 0;
    }

    void SkillFireStart(int id)
    {
        touched = true;
        skillId = id;
        //Debug.Log(id + "    " + touched);
    }

    void SkillFireEnd(int id)
    {
        touched = false;
        skillId = id;
    }

}
