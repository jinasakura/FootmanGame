using System;

public class PlayerDetailInfo
{
    //使用参数后，不知道怎么连接发布者和订阅者
    //public class PlayerDetailArgs:System.EventArgs
    //{
    //    public PlayerDetailArgs(float amount)
    //    {
    //        newAmount = amount;
    //    }

    //    public float newAmount { set; get; }
    //    private float _newAmount;
    //}

    //public event EventHandler<PlayerDetailArgs> OnHpChange = delegate { };
    //public event EventHandler<PlayerDetailArgs> OnMpChange = delegate { };

    public Action<float> OnHPChange { set; get; }
    public Action<float> OnMpChange { set; get; }

    public int level;

    private float _currentHp;
    public float currentHp
    {
        get { return _currentHp; }
        set
        {
            if (value != currentHp)
            {
                _currentHp = value;
                //OnHpChange(this, new PlayerDetailArgs(value));
                Action<float> localOnChange = OnHPChange;
                if (localOnChange != null)
                {
                    localOnChange(value);
                }
            }
        }
    }

    public void DeductHp(float amount)
    {
        if (currentHp >= amount) { currentHp -= amount; }
        else { currentHp = 0; }
    }

    private float _currentMp;
    public float currentMp
    {
        get { return _currentMp; }
        set
        {
            if (value != currentMp)
            {
                _currentMp = value;
                //OnMpChange(this, new PlayerDetailArgs(value));
                Action<float> localOnChange = OnMpChange;
                if (localOnChange != null)
                {
                    localOnChange(value);
                }
            }
        }
    }

    public void DeductMp(float amount)
    {
        if (currentMp >= amount) { currentMp -= amount; }
        else { currentMp = 0; }
    }


}
