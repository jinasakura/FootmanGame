using UnityEngine;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 加载登录界面的基本数据，完成后通知UI显示正确的界面
/// </summary>
public class LoadAllInfo : MonoBehaviour {


    public string carrerXMLPath = "C:/Users/Administrator/Desktop/ProjectsFiles/FootmanGame/Assets/_FightGame/xmls/CareerInfo.xml";
    public string skillXMLPath = "C:/Users/Administrator/Desktop/ProjectsFiles/FootmanGame/Assets/_FightGame/xmls/SkillInfo.xml";


    void Start()
    {
        HandleCareerXml();
        HandleSkillXml();

        NotificationCenter.DefaultCenter.PostNotification(this, LoginEvent.DataIsReady);
    }

    private void HandleCareerXml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(carrerXMLPath);

        XmlNode xn = doc.SelectSingleNode("info");
        XmlNodeList xnl = xn.ChildNodes;

        //CareerItem[] careers = new CareerItem[xnl.Count];
        CareerItem item = null;
        foreach (XmlNode node in xnl)
        {
            XmlElement xe = (XmlElement)node;
            XmlNodeList nodeChild = xe.ChildNodes;
            item = new CareerItem();
            item.careerId = Int32.Parse(nodeChild.Item(1).InnerText);
            item.careerName = nodeChild.Item(2).InnerText;
            item.modelName = nodeChild.Item(3).InnerText;
            item.level = Int32.Parse(nodeChild.Item(4).InnerText);
            item.exp = float.Parse(nodeChild.Item(5).InnerText);
            item.maxHp = float.Parse(nodeChild.Item(6).InnerText);
            item.maxMp = float.Parse(nodeChild.Item(7).InnerText);

            CareerModel.SetCareerItem(item.careerId, item);
        }
    }

    private void HandleSkillXml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(skillXMLPath);

        XmlNode xn = doc.SelectSingleNode("info");
        XmlNodeList xnl = xn.ChildNodes;

        //SkillItem[] skills = new SkillItem[xnl.Count];
        SkillLevelItem item;
        //int i = 0;
        foreach (XmlNode node in xnl)
        {
            XmlElement xe = (XmlElement)node;
            XmlNodeList nodeChild = xe.ChildNodes;
            item = new SkillLevelItem();
            item.id = Int32.Parse(nodeChild.Item(0).InnerText);
            item.careerId = Int32.Parse(nodeChild.Item(1).InnerText);
            item.skillName = nodeChild.Item(2).InnerText;
            item.damageHp = float.Parse(nodeChild.Item(3).InnerText);
            item.level = Int32.Parse(nodeChild.Item(4).InnerText);
            item.mp = float.Parse(nodeChild.Item(5).InnerText);
            item.healHp = float.Parse(nodeChild.Item(6).InnerText);
            item.healMp = float.Parse(nodeChild.Item(7).InnerText);
            item.passive = bool.Parse(nodeChild.Item(8).InnerText);
            item.skillType = Int32.Parse(nodeChild.Item(9).InnerText);
            SkillModel.SetSkillItem(item.careerId, item);
        }
    }



}
