using System.Collections.Generic;

public class CareerItem{

    public int careerId;
    public string careerName;
    public string modelName;
    //public int maxHealth;
    //public SkillItem[] skills;
    public CareerLevelItem[] levels;

    public CareerLevelItem GetCareerLevel(int level)
    {
        CareerLevelItem careerLevel = null;
        foreach (CareerLevelItem item in levels)
        {
            if (item.id == level)
            {
                careerLevel = item;
                break;
            }
        }
        return careerLevel;
    }
}
