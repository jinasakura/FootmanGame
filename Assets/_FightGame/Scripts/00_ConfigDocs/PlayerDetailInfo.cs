using System;
using UnityEngine;

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

    public Action<float> OnHPChange { set; get; }
    public Action<float> OnMpChange { set; get; }

    public int level;
    public int careerId;

    private float _currentHp;
    public float currentHp { set; get; }

    public void DeductHp(float amount)
    {
        if (currentHp >= amount) {
            currentHp -= amount;
        }
        else { currentHp = 0; }
        InvokeHpChange();
        //Action<float> localOnChange = OnHPChange;
        //if (localOnChange != null)
        //{
        //    localOnChange(currentHp);
        //    //Debug.Log("currentHp " + currentHp);
        //}
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
        InvokeHpChange();
    }

    private void InvokeHpChange()
    {
        Action<float> localOnChange = OnHPChange;
        if (localOnChange != null)
        {
            localOnChange(currentHp);
            //Debug.Log("currentHp " + currentHp);
        }
    }

    private float _currentMp;
    public float currentMp
    {
        set; get;
    }

    public void DeductMp(float amount)
    {
        if (currentMp >= amount)
        {
            currentMp -= amount;
        }
        else { currentMp = 0; }
        Action<float> localOnChange = OnMpChange;
        if (localOnChange != null)
        {
            localOnChange(currentMp);
        }
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
        InvokeMpChange();
    }

    private void InvokeMpChange()
    {
        Action<float> localOnChange = OnMpChange;
        if (localOnChange != null)
        {
            localOnChange(currentMp);
        }
    }
}
