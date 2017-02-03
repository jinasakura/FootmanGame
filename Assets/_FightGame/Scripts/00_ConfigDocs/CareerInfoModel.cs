using UnityEngine;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 加载登录界面的基本数据，完成后通知UI显示正确的界面
/// </summary>
public class CareerInfoModel : MonoBehaviour {


    public string carrerXMLPath = "C:/Users/Administrator/Desktop/ProjectsFiles/FootmanGame/Assets/_FightGame/Xml/CareerInfo.xml";

    public static CareerItem[] careers;

    //将模型名和模型对应起来
    public static Dictionary<string, GameObject> modelDict;

    [SerializeField]
    private GameObject[] models;

    void Start()
    {
        //读取职业数据
        XmlDocument doc = new XmlDocument();
        doc.Load(carrerXMLPath);

        XmlNode xn = doc.SelectSingleNode("info");
        XmlNodeList xnl = xn.ChildNodes;

        careers = new CareerItem[xnl.Count];
        int i = 0;
        foreach (XmlNode node in xnl)
        {
            XmlElement xe = (XmlElement)node;
            XmlNodeList nodeChild = xe.ChildNodes;
            careers[i] = new CareerItem();
            careers[i].careerId = Int32.Parse(nodeChild.Item(0).InnerText);
            careers[i].careerName = nodeChild.Item(1).InnerText;
            careers[i].modelName = nodeChild.Item(2).InnerText;

            if (nodeChild.Item(3).HasChildNodes)
            {
                XmlNodeList skillNodeList = nodeChild.Item(3).ChildNodes;
                careers[i].skills = new SkillItem[skillNodeList.Count];
                int j = 0;
                foreach (XmlNode skill in skillNodeList)
                {
                    careers[i].skills[j] = new SkillItem();
                    careers[i].skills[j].skillId = Int32.Parse(skill.ChildNodes.Item(0).InnerText);
                    careers[i].skills[j].skillName = skill.ChildNodes.Item(1).InnerText;
                    j++;
                }
            }
            
            i++;
        }

        NotificationCenter.DefaultCenter.PostNotification(this, LoginEvent.DataIsReady);

        //初始化模型数据
        modelDict = new Dictionary<string, GameObject>();
        foreach (GameObject item in models)
        {
            modelDict[item.name] = item;
        }
    }

}
