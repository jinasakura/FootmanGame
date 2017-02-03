using UnityEngine;
using UnityEngine.UI;

public class CareerToggleBtn : MonoBehaviour {

    public bool defaultSelectedState;

    private CareerItem _btnCareerInfo;
    public CareerItem btnCareerInfo
    {
        set
        {
            //这里先于Start
            _btnCareerInfo = value;
            Text txt = GetComponentInChildren<Text>();
            txt.text = _btnCareerInfo.careerName;
        }
        get { return _btnCareerInfo; }
    }

    private Toggle btn;
    void Start()
    {
        btn = GetComponent<Toggle>();
        btn.isOn = defaultSelectedState;
        btn.onValueChanged.AddListener(OnChangeCareer);

    }

    void OnChangeCareer(bool check)
    {
        if (check)
        {
            object info = btnCareerInfo;
            NotificationCenter.DefaultCenter.PostNotification(this, LoginEvent.SelectedCareer, info);
        }
    }

    public void setToggleOff()
    {
        if(btn) btn.isOn = false;

    }

    public void setToggleOn()
    {
        if (btn) btn.isOn = true;
    }

}
