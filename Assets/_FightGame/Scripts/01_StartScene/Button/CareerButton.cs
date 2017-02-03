using UnityEngine;
using UnityEngine.UI;

public class CareerButton : MonoBehaviour {

    private CareerItem _btnCareerInfo;
    public CareerItem btnCareerInfo
    {
        set
        {
            _btnCareerInfo = value;
            Text txt = GetComponentInChildren<Text>();
            txt.text = _btnCareerInfo.careerName;
        }
        get { return _btnCareerInfo; }
    }

    private Button btn;
	void Start () {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnChangeCareer);
	}
	
	void OnChangeCareer()
    {
        //LoginUserInfo.userCareer = btnCareerInfo;
        //Debug.Log("按下" + btnCareerInfo.careerName);
        object info = btnCareerInfo;
        NotificationCenter.DefaultCenter.PostNotification(this, LoginEvent.SelectedCareer, info);
    }
}
