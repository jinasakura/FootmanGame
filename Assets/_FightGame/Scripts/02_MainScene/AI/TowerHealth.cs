using UnityEngine;
using System.Collections;

public class TowerHealth : MonoBehaviour {

    private ViableEntityInfo playerInfo;
    private SimpleColorSlider healthSlider;

    void Start()
    {
        playerInfo = GetComponent<ViableEntityInfo>();

        SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        foreach (SimpleColorSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
        }
        healthSlider.maxValue = playerInfo.currentHp = 200;

        playerInfo.OnHPDeductChange += healthSlider.UpdateCurrentValue;

    }
}
