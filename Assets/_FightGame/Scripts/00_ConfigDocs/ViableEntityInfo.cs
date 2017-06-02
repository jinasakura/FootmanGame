using UnityEngine;
using System;

/// <summary>
/// 有生命的对象
/// </summary>
public class ViableEntityInfo : BaseGameEntity {

    public float maxHp;
    private float _currentHp;
    public float currentHp { get; set; }

    public Action<float> OnHPDeductChange { set; get; }

    public void DeductHp(float amount)
    {
        currentHp -= amount;
        if (currentHp < 0) { currentHp = 0; }
        Action<float> localOnChange = OnHPDeductChange;
        if (localOnChange != null)
        {
            localOnChange(currentHp);
        }
    }

}
