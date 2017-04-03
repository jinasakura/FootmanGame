using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 主屏幕上左上角的血条和蓝条
/// </summary>
public class UserInfoHandle : MonoBehaviour
{

    [SerializeField]
    private Text userName;
    [SerializeField]
    private SimpleColorSlider healthSlider;
    [SerializeField]
    private SimpleColorSlider mpSlider;

    void Awake()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.MainSceneIsReady);


        //NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.UserHpChange);
        //NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.UserMpChange);
    }

    private void MainSceneIsReady()
    {
        //Debug.Log("isReady");
        //PlayerInfo playerInfo = SceneManager.instance.GetPlayerInfoById(LoginUserInfo.playerId);

        //userName.text = playerInfo.playerName;
        //SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        //foreach (SimpleColorSlider slider in sliders)
        //{
        //    if (slider.name == "HealthSlider") healthSlider = slider;
        //    else mpSlider = slider;
        //}
        
        //healthSlider.maxValue = playerInfo.detail.currentHp;
        //mpSlider.maxValue = playerInfo.detail.currentMp;

        //playerInfo.detail.OnHPChange += healthSlider.UpdateCurrentValue;
        //playerInfo.detail.OnMpChange += mpSlider.UpdateCurrentValue;
    }

    //void UserHpChange(NotificationCenter.Notification info)
    //{
    //    float amount = (float)info.data;
    //    healthSlider.UpdateValue(amount);
    //}

    //void UserMpChange(NotificationCenter.Notification info)
    //{
    //    float amount = (float)info.data;
    //    mpSlider.UpdateValue(amount);

    //}
}
