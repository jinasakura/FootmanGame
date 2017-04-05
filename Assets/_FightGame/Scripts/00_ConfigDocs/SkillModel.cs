using System.Collections.Generic;

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

public class SkillModel {
    /// <summary>
    /// int-careerId
    /// </summary>
    private static Dictionary<int, List<SkillLevelItem>> skillDict = new Dictionary<int, List<SkillLevelItem>>();
    private static Dictionary<string, SkillLevelItem> skillNameDict = new Dictionary<string, SkillLevelItem>();
    //private static Dictionary<string, SkillLevelItem> skillInfoDict = new Dictionary<string, SkillLevelItem>();
    private static List<SkillLevelItem> skillList = new List<SkillLevelItem>();

    public static void SetSkillItem(int careerId,SkillLevelItem item)
    {
        skillList.Add(item);
        skillNameDict[item.skillName] = item;

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

    public static List<SkillLevelItem> GetAllSkillType()
    {
        //SkillLevelItem item = skillDict[careerId];
        List<SkillLevelItem> items = new List<SkillLevelItem>();
        int flagCareerId = 0;
        foreach (SkillLevelItem item in skillList)
        {
            if(flagCareerId != item.careerId)
            {
                items.Add(item);
                flagCareerId = item.careerId;
            }
        }
        return items;
    }

    public static SkillLevelItem GetSkillById(int careerId,int skillId)
    {
        List<SkillLevelItem> items = skillDict[careerId];
        SkillLevelItem level = null;
        foreach (SkillLevelItem item in items)
        {
            if(item.id == skillId)
            {
                level = item;
                break;
            }
        }
        return level;
    }

    public static SkillLevelItem GetSkillLevelByName(string skillName)
    {
        SkillLevelItem result;
        if (skillNameDict.TryGetValue(skillName, out result))
        {
            return result;
        }
        return null;
    }

}
