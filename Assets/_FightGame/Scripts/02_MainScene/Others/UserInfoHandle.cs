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

    void Start()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, MainSceneEvent.MainSceneIsReady);
    }

    private void MainSceneIsReady()
    {
        //Debug.Log("isReady");
        PlayerInfo playerInfo = PlayerModel.GetPlayerInfoInfoById(LoginUserInfo.playerId);

        userName.text = playerInfo.eName;
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
