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
    private Text displayCareerTxt;//显示选择了哪个职业的文本框
    [SerializeField]
    private Button confirmBtn;

    // Use this for initialization
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
        foreach (CareerItem item in CareerInfoModel.careers)
        {
            GameObject cbtn = Instantiate(careerBtn, Vector3.zero, Quaternion.identity, careerBtnPanel) as GameObject;
            CareerButton carBtn = cbtn.GetComponent<CareerButton>();
            carBtn.btnCareerInfo = item;
        }
    }

    void SelectedCareer(NotificationCenter.Notification careerInfo)
    {
        LoginUserInfo.userCareer = careerInfo.data as CareerItem;
        displayCareerTxt.text = "你选择的职业是：" + LoginUserInfo.userCareer.careerName;
    }

    void EnterGame()
    {
        //测试用代码
        LoginUserInfo.userName = "Test";
        LoginUserInfo.userCareer = CareerInfoModel.careers[0];
        SceneManager.LoadScene("MainScene");

        //if (LoginUserInfo.userName != "")
        //{
        //    if (LoginUserInfo.userCareer != null)
        //    {
        //        SceneManager.LoadScene("MainScene");
        //    }
        //    else
        //    {
        //        Debug.LogWarning("请选择职业");
        //    }
        //}
        //else
        //{
        //    Debug.LogWarning("请填入姓名");
        //}

    }

    void InputNameCheck()
    {
        //Debug.Log(inputName.text);
        LoginUserInfo.userName = inputName.text;
    }
}
