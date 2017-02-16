using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    void DataIsReady()
    {
        //Debug.Log("数据已准备好");
        //初始化对的职业按钮
        careerBtns = new CareerToggleBtn[CareerInfoModel.careers.Length];
        int i = 0;
        foreach (CareerItem item in CareerInfoModel.careers)
        {
            GameObject cbtn = Instantiate(careerBtn, Vector3.zero, Quaternion.identity, careerBtnPanel) as GameObject;
            CareerToggleBtn carBtn = cbtn.GetComponent<CareerToggleBtn>();
            if (i == 0)
            {
                carBtn.defaultSelectedState = true;
                LoginUserInfo.userCareer = item;
            }
            else { carBtn.defaultSelectedState = false; }
            carBtn.btnCareerInfo = item;
            careerBtns[i] = carBtn;
            i++;
        }

        LoginUserInfo.playerInfo = new PlayerInfo();
        LoginUserInfo.playerInfo.playerId = 0;
        LoginUserInfo.playerInfo.playerName = "Test";
    }

    void SelectedCareer(NotificationCenter.Notification careerInfo)
    {
        LoginUserInfo.userCareer = careerInfo.data as CareerItem;
        foreach (CareerToggleBtn item in careerBtns)
        {
            if(item.btnCareerInfo.careerId != LoginUserInfo.userCareer.careerId) { item.setToggleOff(); }
        }
    }

    void EnterGame()
    {
        if (LoginUserInfo.playerInfo.playerName != "")
        {
            if (LoginUserInfo.userCareer != null)
            {
                SceneManager.LoadScene("MainScene");
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
