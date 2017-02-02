using UnityEngine;
using System.Xml;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 加载登录界面的基本数据，完成后通知UI显示正确的界面
/// </summary>
public class CareerInfoModel : MonoBehaviour {

    //枚举不和读取的数据做关联，但是两边的数据应一致
    public enum Type { Knight=1, Barbarian, Archer, Wizard };

    public enum KightSkillType { Attack01=1,Attack02,DoubleAttack,CastSpell };

    public string carrerXMLPath = "C:/Users/Administrator/Desktop/ProjectsFiles/FootmanGame/Assets/_FightGame/Xml/CareerInfo.xml";

    public static CareerItem[] careers;

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

            XmlNodeList skillNodeList = nodeChild.Item(2).ChildNodes;
            careers[i].skills = new SkillItem[skillNodeList.Count];
            int j = 0;
            foreach (XmlNode skill in skillNodeList)
            {
                careers[i].skills[j] = new SkillItem();
                careers[i].skills[j].skillId = Int32.Parse(skill.ChildNodes.Item(0).InnerText);
                careers[i].skills[j].skillName = skill.ChildNodes.Item(1).InnerText;
                j++;
            }
            i++;
        }

        NotificationCenter.DefaultCenter.PostNotification(this, LoginEvent.DataIsReady);
    }

}
