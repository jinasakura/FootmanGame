using UnityEngine;
using System.Collections;

//2017/1/5
//
public class ContactNotices : MonoBehaviour {

    //private object _data;
    //public object data
    //{
    //    get { return _data; }
    //    set { _data = value; }
    //}

	public void PostNotification(string name,Hashtable data)
    {
        NotificationCenter.DefaultCenter.PostNotification(this, name, data);
    }
}
