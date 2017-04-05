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
    }

    private void MainSceneIsReady()
    {
        //Debug.Log("isReady");
        PlayerInfo playerInfo = PlayerModel.GetPlayerInfoInfoById(LoginUserInfo.playerId);

        userName.text = playerInfo.playerName;
        SimpleColorSlider[] sliders = GetComponentsInChildren<SimpleColorSlider>();
        foreach (SimpleColorSlider slider in sliders)
        {
            if (slider.name == "HealthSlider") healthSlider = slider;
            else mpSlider = slider;
        }

        healthSlider.maxValue = playerInfo.detail.currentHp;
        mpSlider.maxValue = playerInfo.detail.currentMp;

        playerInfo.detail.OnHPChange += healthSlider.UpdateCurrentValue;
        playerInfo.detail.OnMpChange += mpSlider.UpdateCurrentValue;
    }

}
