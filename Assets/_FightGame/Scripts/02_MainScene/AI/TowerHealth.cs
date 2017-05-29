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
        playerInfo.detail = new PlayerDetailInfo();
        healthSlider.maxValue = playerInfo.detail.currentHp = 100;

        playerInfo.detail.OnHPAddChange += healthSlider.UpdateCurrentValue;
        playerInfo.detail.OnHPDeductChange += healthSlider.UpdateCurrentValue;

    }
}
