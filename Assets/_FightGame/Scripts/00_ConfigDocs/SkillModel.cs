using System.Collections.Generic;
using UnityEngine;

public class SkillLevelItem
{
    public int id;
    public int careerId;
    public string skillName;


    /// <summary>
    /// 技能消耗hp
    /// </summary>
    public float damageHp;

    //技能限制等级，对应CareerLevelItem里的id
    public int level;

    /// <summary>
    /// 技能消耗mp
    /// </summary>
    public float mp;

    //被动治愈技能加多少血
    public float healHp;
    public float healMp;

    public float distance;

    //0-主动技能-false ; 1-被动技能-true
    public bool passive;

    //1-单人；2-群人；3-对地
    public int skillType;

    public bool CheckCondition(PlayerDetailInfo playerRequirement)
    {
        bool use = false;
        if (level <= playerRequirement.level)
        {
            use = true;
            if (playerRequirement.currentMp >= mp)
            {
                use = true;
            }
            else { use = false; }
        }
        else { use = false; }
        return use;
    }
}

//public class SkillItem
//{

//    public int careerId;
//    public SkillLevelItem[] levels;

//    public List<SkillLevelItem> GetSkillLevels(int level)
//    {
//        List<SkillLevelItem> skillLevels = new List<SkillLevelItem>();
//        foreach (SkillLevelItem item in levels)
//        {
//            if (item.level <= level)
//            {
//                skillLevels.Add(item);
//            }
//            else
//            {
//                break;
//            }
//        }
//        return skillLevels;
//    }
//}

public class SkillModel
{
    /// <summary>
    /// int-careerId
    /// </summary>
    private static Dictionary<int, List<SkillLevelItem>> skillDict = new Dictionary<int, List<SkillLevelItem>>();
    private static Dictionary<int, SkillLevelItem> skillIdDict = new Dictionary<int, SkillLevelItem>();
    private static List<SkillLevelItem> skillList = new List<SkillLevelItem>();

    public static void SetSkillItem(int careerId, SkillLevelItem item)
    {
        skillList.Add(item);
        skillIdDict[item.id] = item;

        if (!skillDict.ContainsKey(careerId))
        {
            skillDict[careerId] = new List<SkillLevelItem>();
        }
        List<SkillLevelItem> items = skillDict[careerId];
        items.Add(item);
    }

    //public static void SetSkillInfoByName(string skillName,SkillLevelItem item)
    //{
    //    skillInfoDict[skillName] = item;
    //}

    public static List<SkillLevelItem> GetAllSkillByCondition(int careerId,int level=1)
    {
        List<SkillLevelItem> items = new List<SkillLevelItem>();
        if (!skillDict.ContainsKey(careerId)) { Debug.Log("找不到职业id为->" + careerId + "的技能！"); }
        else
        {
            List<SkillLevelItem> skills = skillDict[careerId];
            foreach (SkillLevelItem item in skills)
            {
                if (item.level <= level)
                {
                    items.Add(item);
                }
            }
        }
        return items;
    }

    public static List<SkillLevelItem> GetAllSkillType()
    {
        //SkillLevelItem item = skillDict[careerId];
        List<SkillLevelItem> items = new List<SkillLevelItem>();
        int flagCareerId = 0;
        foreach (SkillLevelItem item in skillList)
        {
            if (flagCareerId != item.careerId)
            {
                items.Add(item);
                flagCareerId = item.careerId;
            }
        }
        return items;
    }

    public static SkillLevelItem GetSkillById(int careerId, int skillId)
    {
        List<SkillLevelItem> items = skillDict[careerId];
        SkillLevelItem level = null;
        foreach (SkillLevelItem item in items)
        {
            if (item.id == skillId)
            {
                level = item;
                break;
            }
        }
        return level;
    }

    public static SkillLevelItem GetSkillLevelByName(int skillId)
    {
        SkillLevelItem result;
        if (skillIdDict.TryGetValue(skillId, out result))
        {
            return result;
        }
        return null;
    }

}
