using UnityEngine;
using System.Collections;

public class UserInfo : MonoBehaviour {

    private string _userName;
    public string userName
    {
        set { _userName = value; }
        get { return _userName; }
    }

    private CareerItem _careerInfo;
    public CareerItem careerInfo
    {
        set { _careerInfo = value; }
        get { return _careerInfo; }
    }

}
