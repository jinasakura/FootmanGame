using UnityEngine;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 加载登录界面的基本数据，完成后通知UI显示正确的界面
/// </summary>
public class AllInfoModel : MonoBehaviour {


    public string carrerXMLPath = "C:/Users/Administrator/Desktop/ProjectsFiles/FootmanGame/Assets/_FightGame/Xml/CareerInfo.xml";
    public string skillXMLPath = "C:/Users/Administrator/Desktop/ProjectsFiles/FootmanGame/Assets/_FightGame/Xml/SkillInfo.xml";

    //private static CareerItem[] careers;

    //将模型名和模型对应起来
    public static Dictionary<string, GameObject> modelDict;

    //职业id与职业信息
    //public static Dictionary<int, CareerItem> careerDict;
    //public static Dictionary<int, SkillItem> skillDict;

    [SerializeField]
    private GameObject[] models;

    void Start()
    {
        HandleCareerXml();
        HandleSkillXml();

        HandleModelInfo();

        //careerDict = new Dictionary<int, CareerItem>();
        //skillDict = new Dictionary<int, SkillItem>();
        //foreach (SkillItem item in skillDict)
        //{
            //careerDict[item.careerId] = item;
            //foreach (SkillItem skill in item.skills)
            //{
            //    skillDict[skill.skillId] = skill;
            //}
        //}

        //NotificationCenter.DefaultCenter.PostNotification(this, LoginEvent.DataIsReady);
    }

    private void HandleCareerXml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(carrerXMLPath);

        XmlNode xn = doc.SelectSingleNode("info");
        XmlNodeList xnl = xn.ChildNodes;

        CareerItem[] careers = new CareerItem[xnl.Count];
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
                XmlNodeList levelNodeList = nodeChild.Item(3).ChildNodes;
                careers[i].levels = new CareerLevelItem[levelNodeList.Count];
                int j = 0;
                foreach (XmlNode skill in levelNodeList)
                {
                    careers[i].levels[j] = new CareerLevelItem();
                    careers[i].levels[j].id = Int32.Parse(skill.ChildNodes.Item(0).InnerText);
                    careers[i].levels[j].exp = Int32.Parse(skill.ChildNodes.Item(1).InnerText);
                    careers[i].levels[j].hp = Int32.Parse(skill.ChildNodes.Item(2).InnerText);
                    careers[i].levels[j].mp = Int32.Parse(skill.ChildNodes.Item(3).InnerText);
                    j++;
                }
            }

            CareerModel.careerDict[careers[i].careerId] = careers[i];

            i++;
        }
    }

    private void HandleSkillXml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(carrerXMLPath);

        XmlNode xn = doc.SelectSingleNode("info");
        XmlNodeList xnl = xn.ChildNodes;

        SkillItem[] skills = new SkillItem[xnl.Count];
        int i = 0;
        foreach (XmlNode node in xnl)
        {
            XmlElement xe = (XmlElement)node;
            XmlNodeList nodeChild = xe.ChildNodes;
            skills[i] = new SkillItem();
            skills[i].careerId = Int32.Parse(nodeChild.Item(0).InnerText);

            if (nodeChild.Item(1).HasChildNodes)
            {
                XmlNodeList levelNodeList = nodeChild.Item(1).ChildNodes;
                skills[i].levels = new SkillLevelItem[levelNodeList.Count];
                int j = 0;
                foreach (XmlNode skill in levelNodeList)
                {
                    skills[i].levels[j] = new SkillLevelItem();
                    skills[i].levels[j].id = Int32.Parse(skill.ChildNodes.Item(0).InnerText);
                    skills[i].levels[j].name = skill.ChildNodes.Item(1).InnerText;
                    skills[i].levels[j].damage = Int32.Parse(skill.ChildNodes.Item(2).InnerText);
                    skills[i].levels[j].level = Int32.Parse(skill.ChildNodes.Item(3).InnerText);
                    skills[i].levels[j].mp = Int32.Parse(skill.ChildNodes.Item(4).InnerText);
                    skills[i].levels[j].passive = bool.Parse(skill.ChildNodes.Item(5).InnerText);
                    j++;
                }
            }

            SkillModel.skillDict[skills[i].careerId] = skills[i];

            i++;
        }
    }

    private void HandleModelInfo()
    {
        //初始化模型数据
        modelDict = new Dictionary<string, GameObject>();
        foreach (GameObject item in models)
        {
            modelDict[item.name] = item;
        }
    }

}
