using UnityEngine;
using System.Collections;

public class TowerHealth : MonoBehaviour {

    private PlayerInfo playerInfo;
    private SimpleColorSlider healthSlider;

    void Start()
    {
        playerInfo = GetComponent<PlayerInfo>();

        SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        foreach (SimpleColorSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
        }
        healthSlider.maxValue = playerInfo.currentHp = 100;

        playerInfo.OnHPAddChange += healthSlider.UpdateCurrentValue;
        playerInfo.OnHPDeductChange += healthSlider.UpdateCurrentValue;

    }
}
