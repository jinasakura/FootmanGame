using UnityEngine;
using System;

/// <summary>
/// 玩家信息
/// </summary>
public class PlayerInfo : ViableEntityInfo
{

    public string modelName;
    //阵营
    public int camp;

    //public int career;
    //public int careerId;
    //public PlayerDetailInfo detail;

    public int level;
    public int careerId;

    private float _currentMp;
    public float currentMp { set; get; }

    public Action<float> OnHPAddChange { set; get; }
    public Action<float> OnMpAddChange { set; get; }
    public Action<float> OnMpDeductChange { set; get; }

    public void AddHp(float amount)
    {
        CareerItem careerLevel = CareerModel.GetLevelItem(careerId, level);
        currentHp += amount;
        if(currentHp > careerLevel.maxHp) { currentHp = careerLevel.maxHp; }
        Action<float> localOnChange = OnHPAddChange;
        if (localOnChange != null)
        {
            localOnChange(currentHp);
            //Debug.Log("currentHp " + currentHp);
        }
    }

    public void DeductMp(float amount)
    {
        currentMp -= amount;
        if (currentMp < 0) { currentMp = 0; }
        Action<float> localOnChange = OnMpDeductChange;
        if (localOnChange != null)
        {
            localOnChange(currentMp);
        }
    }

    public void AddMp(float amount)
    {
        currentMp += amount;
        CareerItem careerLevel = CareerModel.GetLevelItem(careerId, level);
        if (currentMp> careerLevel.maxMp) { currentMp = careerLevel.maxMp; }
        Action<float> localOnChange = OnMpAddChange;
        if (localOnChange != null)
        {
            localOnChange(currentMp);
        }
    }

}
