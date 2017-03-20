using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserInfoHandle : MonoBehaviour {

    [SerializeField]
    private Text userName;
    [SerializeField]
    private SimpleColorSlider healthSlider;
    [SerializeField]
    private SimpleColorSlider mpSlider;

	void Start() {
        userName.text = LoginUserInfo.playerInfo.playerName;
        SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        foreach (SimpleColorSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
            else mpSlider = slider;
        }
        healthSlider.maxValue = LoginUserInfo.playerInfo.detail.currentHp;
        mpSlider.maxValue = LoginUserInfo.playerInfo.detail.currentMp;

        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.UserHpChange);
        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.UserMpChange);
    }
	
	void UserHpChange(NotificationCenter.Notification info)
    {
        float amount = (float)info.data;
        healthSlider.UpdateValue(amount);
    }

    void UserMpChange(NotificationCenter.Notification info)
    {
        float amount = (float)info.data;
        mpSlider.UpdateValue(amount);
        
    }
}
