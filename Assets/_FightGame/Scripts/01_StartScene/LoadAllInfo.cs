using UnityEngine;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 加载登录界面的基本数据，完成后通知UI显示正确的界面
/// </summary>
public class LoadAllInfo : MonoBehaviour {


    public string carrerXMLPath;
    public string skillXMLPath;
    public string aiXMLPath;


    void Start()
    {
        HandleCareerXml();
        HandleSkillXml();
        HandleAIXml();

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
            item.releaseDistance = float.Parse(nodeChild.Item(10).InnerText);
            item.damageRadius = float.Parse(nodeChild.Item(11).InnerText);
            item.damagePeople = Int16.Parse(nodeChild.Item(12).InnerText);
            item.releaseSpeed = float.Parse(nodeChild.Item(13).InnerText);
            item.lancherName = nodeChild.Item(14).InnerText;
            item.firePoint = Int16.Parse(nodeChild.Item(15).InnerText);
            item.canMove = Boolean.Parse(nodeChild.Item(16).InnerText);
            item.loopTimes = Int16.Parse(nodeChild.Item(17).InnerText);
            SkillModel.SetSkillItem(item.careerId, item);
        }
    }

    private void HandleAIXml()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(aiXMLPath);

        XmlNode xn = doc.SelectSingleNode("info");
        XmlNodeList xnl = xn.ChildNodes;

        //CareerItem[] careers = new CareerItem[xnl.Count];
        AIType item = null;
        foreach (XmlNode node in xnl)
        {
            XmlElement xe = (XmlElement)node;
            XmlNodeList nodeChild = xe.ChildNodes;
            item = new AIType();
            item.typeId = Int32.Parse(nodeChild.Item(1).InnerText);
            item.stay = Boolean.Parse(nodeChild.Item(2).InnerText);
            item.waypointIndex = Int32.Parse(nodeChild.Item(3).InnerText);
            item.patrolSpeed = float.Parse(nodeChild.Item(4).InnerText);
            item.chaseSpeed = float.Parse(nodeChild.Item(5).InnerText);
            item.waypointDistance = float.Parse(nodeChild.Item(6).InnerText);
            item.warnRadius = float.Parse(nodeChild.Item(7).InnerText);
            item.patrolGapTime = float.Parse(nodeChild.Item(7).InnerText);
            item.cameraFar = float.Parse(nodeChild.Item(7).InnerText);

            AIModel.SetAiType(item.typeId, item);
        }
    }



}
