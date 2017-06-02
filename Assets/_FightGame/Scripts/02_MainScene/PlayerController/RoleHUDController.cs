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
        healthSlider.maxValue = playerInfo.currentHp;
        mpSlider.maxValue = playerInfo.currentMp;

        playerInfo.OnHPAddChange += healthSlider.UpdateCurrentValue;
        playerInfo.OnHPDeductChange += healthSlider.UpdateCurrentValue;

        playerInfo.OnMpAddChange += mpSlider.UpdateCurrentValue;
        playerInfo.OnMpDeductChange += mpSlider.UpdateCurrentValue;
    }

    
}
