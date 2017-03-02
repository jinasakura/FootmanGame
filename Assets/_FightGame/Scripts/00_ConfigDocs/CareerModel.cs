using System.Collections.Generic;
public class CareerModel{

    private static Dictionary<int, CareerItem> _careerDict = new Dictionary<int, CareerItem>();
    public static Dictionary<int,CareerItem> careerDict
    {
        set { _careerDict = value; }
    }

    public static CareerLevelItem GetLevelItem(int careerId,int level)
    {
        CareerItem careerItem = _careerDict[careerId];
        CareerLevelItem desLevel = null;
        foreach (CareerLevelItem item in careerItem.levels)
        {
            if(item.id == level)
            {
                desLevel = item;
                break;
            }
        }
        return desLevel;
    }

}
