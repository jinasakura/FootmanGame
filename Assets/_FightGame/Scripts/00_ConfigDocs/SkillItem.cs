using System.Collections.Generic;

public class SkillItem {

    public int careerId;
    public SkillLevelItem[] levels;

    public List<SkillLevelItem> GetSkillLevels(int level)
    {
        List<SkillLevelItem> skillLevels = new List<SkillLevelItem>();
        foreach (SkillLevelItem item in levels)
        {
            if(item.level <= level)
            {
                skillLevels.Add(item);
            }
            else
            {
                break;
            }
        }
        return skillLevels;
    }
}
