using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这个类目前没法保存函数指针，怎么保存不知道

public class CallbackCenter :System.Object {

    private static CallbackCenter defaultCenter;
    public static CallbackCenter DefaultCenter
    {
        get
        {
            if (defaultCenter == null)
            {
                System.Object callbackObject = new System.Object();

            }

            return defaultCenter;
        }
    }

    Hashtable notifications = new Hashtable();

    public void AddCallback(string name, Action action)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Null name specified for addCallback");
            return;
        }
        if (action == null)
        {
            Debug.Log("Null action specified for addCallback");
            return;
        }

        if (notifications[name] == null)
        {
            notifications[name] = new Dictionary<Action, object>();
        }

        Dictionary<Action, object> dict = notifications[name] as Dictionary<Action, object>;

        if (!dict.ContainsKey(action)) { dict.Add(action,null); }
    }

    public void RemoveCallback(string name,Action action)
    {
        Dictionary<Action, object> dict = (Dictionary<Action, object>)notifications[name];
        if (dict != null)
        {
            if (dict.ContainsKey(action))
            {
                dict.Remove(action);
            }
            if (dict.Count == 0)
            {
                notifications.Remove(name);
            }
        }
    }


    public void PostCallback(string name, object data)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Null name sent to PostCallback");
            return;
        }

        Dictionary<Action<object>, object> dict = notifications[name] as Dictionary<Action<object>, object>;
        if (dict == null)
        {
            Debug.Log("dict list not found in PostCallback: " + name);
            return;
        }

        foreach(KeyValuePair<Action<object>, object> i in dict)
        {
            i.Key(i.Value);
        }
    }

    private class CallbackInfo
    {
        public Action action;
        public string name;
        public Hashtable data;
        public CallbackInfo(Action aAction,string aName)
        {
            action = aAction;
            name = aName;
        }
        public CallbackInfo(Action aAction,string aName,Hashtable aData)
        {
            action = aAction;
            name = aName;
            data = aData;
        }
    }
    
}
