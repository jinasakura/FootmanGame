using UnityEngine;
using System.Collections;

public class RoleHUDController : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private SimpleColorSlider healthSlider;
    private SimpleColorSlider mpSlider;

    void Start()
    {
        playerInfo = GetComponentInParent<PlayerInfo>();

        SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        foreach (SimpleColorSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
            else mpSlider = slider;
        }
        healthSlider.maxValue = playerInfo.detail.currentHp;
        mpSlider.maxValue = playerInfo.detail.currentMp;

        playerInfo.detail.OnHPAddChange += healthSlider.UpdateCurrentValue;
        playerInfo.detail.OnHPDeductChange += healthSlider.UpdateCurrentValue;

        playerInfo.detail.OnMpAddChange += mpSlider.UpdateCurrentValue;
        playerInfo.detail.OnMpDeductChange += mpSlider.UpdateCurrentValue;
    }

    
}
