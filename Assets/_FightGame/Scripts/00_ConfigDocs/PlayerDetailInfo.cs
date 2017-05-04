using System;
using UnityEngine;
using System.Collections.Generic;

public class PlayerDetailInfo
{
    //使用参数后，不知道怎么连接发布者和订阅者
    //public class PlayerDetailArgs:System.EventArgs
    //{
    //    public PlayerDetailArgs(float amount)
    //    {
    //        newAmount = amount;
    //    }

    //    public float newAmount { set; get; }
    //    private float _newAmount;
    //}

    //public event EventHandler<PlayerDetailArgs> OnHpChange = delegate { };
    //public event EventHandler<PlayerDetailArgs> OnMpChange = delegate { };

    public Action<float> OnHPAddChange { set; get; }
    public Action<float> OnMpAddChange { set; get; }
    public Action<float> OnHPDeductChange { set; get; }
    public Action<float> OnMpDeductChange { set; get; }

    public int level;
    public int careerId;

    private float _currentHp;
    public float currentHp { set; get; }

    private float _currentMp;
    public float currentMp{ set; get; }


    public void DeductHp(float amount)
    {
        if (currentHp >= amount) {
            currentHp -= amount;
        }
        else { currentHp = 0; }
        //InvokeHpAddChange();
        Action<float> localOnChange = OnHPDeductChange;
        if (localOnChange != null)
        {
            localOnChange(currentHp);
        }
        //RegisterAction(OnHPDeductChange);
    }

    public void AddHp(float amount)
    {
        CareerItem careerLevel = CareerModel.GetLevelItem(careerId, level);
        if (currentHp + amount > careerLevel.maxHp)
        {
            currentHp = careerLevel.maxHp;
        }
        else
        {
            currentHp += amount;
        }
        InvokeHpAddChange();
        //RegisterAction(OnHPAddChange);
    }

    private void InvokeHpAddChange()
    {
        Action<float> localOnChange = OnHPAddChange;
        if (localOnChange != null)
        {
            localOnChange(currentHp);
            //Debug.Log("currentHp " + currentHp);
        }
    }



    public void DeductMp(float amount)
    {
        if (currentMp >= amount)
        {
            currentMp -= amount;
        }
        else { currentMp = 0; }
        Action<float> localOnChange = OnMpDeductChange;
        if (localOnChange != null)
        {
            localOnChange(currentMp);
        }
        //RegisterAction(OnMpDeductChange);
    }

    public void AddMp(float amount)
    {
        CareerItem careerLevel = CareerModel.GetLevelItem(careerId, level);
        if (currentMp + amount > careerLevel.maxMp)
        {
            currentMp = careerLevel.maxMp;
        }
        else
        {
            currentMp += amount;
        }
        InvokeMpAddChange();
        //RegisterAction(OnHPAddChange);
    }

    private void InvokeMpAddChange()
    {
        Action<float> localOnChange = OnMpAddChange;
        if (localOnChange != null)
        {
            localOnChange(currentMp);
        }
    }

    //private void RegisterAction(Action<float> numberChange)
    //{
    //    Action<float> localOnChange = numberChange;
    //    if (localOnChange != null)
    //    {
    //        localOnChange(currentMp);
    //    }
    //}
}
