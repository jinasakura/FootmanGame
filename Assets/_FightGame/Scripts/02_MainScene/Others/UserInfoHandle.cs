using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserInfoHandle : MonoBehaviour {

    [SerializeField]
    private Text userName;
    [SerializeField]
    private HealthSlider healthSlider;
    [SerializeField]
    private HealthSlider mpSlider;

	void OnEnable() {
        userName.text = LoginUserInfo.playerInfo.playerName;
        HealthSlider[] sliders = GetComponentsInChildren<HealthSlider>();
        foreach (HealthSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
            else mpSlider = slider;
        }
        CareerLevelItem item = CareerModel.GetLevelItem(LoginUserInfo.playerInfo.careerId, LoginUserInfo.playerInfo.level);
        healthSlider.maxValue = item.hp;
        mpSlider.maxValue = item.mp;

        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.UserHpChange);
        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.UserMpChange);
    }
	
	void UserHpChange(NotificationCenter.Notification info)
    {
        int amount = (int)info.data;
        healthSlider.TakeDamage(amount);
    }

    void UserMpChange(NotificationCenter.Notification info)
    {
        int amount = (int)info.data;
        mpSlider.TakeDamage(amount);
    }
}
