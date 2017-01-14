using UnityEngine;
using System.Collections;

public class Career : MonoBehaviour {

    public enum KightSkillType
    { Attack01 = 1, Attack02, DoubleAttack, CastSpell };

    public struct KightSkillValue
    {
        public string skillName;
        public int skillType;
    }

    public static KightSkillValue[] kightSkills;

    void Start()
    {
        kightSkills = new KightSkillValue[4];
        string[] strNames = new string[4];
        int i = 0;
        foreach (KightSkillType type in System.Enum.GetValues(typeof(KightSkillType)))
        {
            string strName = System.Enum.GetName(typeof(KightSkillType), type);
            kightSkills[i] = new KightSkillValue();
            kightSkills[i].skillName = strName;
            kightSkills[i].skillType = (int)type;
            i++;
        }
    }


}
