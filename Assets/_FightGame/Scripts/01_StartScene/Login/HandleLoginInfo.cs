using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/// <summary>
/// 登录信息加载完毕后，通知此类完成登录信息面板刷新工作
/// </summary>
public class HandleLoginInfo : MonoBehaviour {

    [SerializeField]
    private InputField inputName;
    [SerializeField]
    private GameObject careerBtn;
    [SerializeField]
    private Transform careerBtnPanel;
    [SerializeField]
    private Button confirmBtn;

    private CareerToggleBtn[] careerBtns;

    void Awake()
    {
        NotificationCenter.DefaultCenter.AddObserver(this, LoginEvent.DataIsReady);
        NotificationCenter.DefaultCenter.AddObserver(this, LoginEvent.SelectedCareer);
    }

    void Start()
    {
        inputName.onValueChanged.AddListener(delegate { InputNameCheck(); });
        confirmBtn.onClick.AddListener(EnterGame);
    }

    private void initLoginInfo()
    {
        LoginUserInfo.playerInfo = gameObject.AddComponent<PlayerInfo>();

        LoginUserInfo.playerInfo.playerId = 0;
        LoginUserInfo.playerInfo.playerName = "Test";
        LoginUserInfo.playerInfo.detail = new PlayerDetailInfo();
        LoginUserInfo.playerInfo.detail.level = 1;
    }

    private void ChangeCareer(CareerItem item)
    {
        LoginUserInfo.playerInfo.careerId = item.careerId;
        LoginUserInfo.playerInfo.modelName = item.modelName;

        LoginUserInfo.careerLevel = item.GetCareerLevel(LoginUserInfo.playerInfo.detail.level);
        LoginUserInfo.skillLevels = SkillModel.GetAllSkillLevels(item.careerId, LoginUserInfo.playerInfo.detail.level);
    }

    void DataIsReady()
    {
        //Debug.Log("数据已准备好");
        initLoginInfo();

        //初始化对的职业按钮
        careerBtns = new CareerToggleBtn[CareerModel.GetCareerCount()];
        int i = 0;
        Dictionary<int, CareerItem>.ValueCollection values = CareerModel.GetAllCareerItem();
        foreach (CareerItem item in values)
        {
            GameObject cbtn = Instantiate(careerBtn, Vector3.zero, Quaternion.identity, careerBtnPanel) as GameObject;
            CareerToggleBtn carBtn = cbtn.GetComponent<CareerToggleBtn>();
            if (i == 0)
            {
                carBtn.defaultSelectedState = true;
                ChangeCareer(item);
            }
            else { carBtn.defaultSelectedState = false; }
            carBtn.btnCareerInfo = item;
            careerBtns[i] = carBtn;
            i++;
        }

        

    }

    void SelectedCareer(NotificationCenter.Notification careerInfo)
    {
        CareerItem career = careerInfo.data as CareerItem;
        ChangeCareer(career);
        foreach (CareerToggleBtn item in careerBtns)
        {
            if(item.btnCareerInfo.careerId != LoginUserInfo.playerInfo.careerId) { item.setToggleOff(); }
        }
    }

    void EnterGame()
    {
        if (LoginUserInfo.playerInfo.playerName != "")
        {
            if (LoginUserInfo.careerLevel != null)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
            }
            else
            {
                Debug.LogWarning("请选择职业");
            }
        }
        else
        {
            Debug.LogWarning("请填入姓名");
        }

    }

    void InputNameCheck()
    {
        LoginUserInfo.playerInfo.playerName = inputName.text;
    }
}
