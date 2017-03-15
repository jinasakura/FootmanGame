public class PlayerDetailInfo
{

    public int level;

    private float _currentHp;
    public float currentHp { set; get; }

    public void DeductHp(float amount)
    {
        if (_currentHp >= amount) { _currentHp -= amount; }
        else { _currentHp = 0; }
    }


    private float _currentMp;
    public float currentMp { set; get; }

    public void DeductMp(float amount)
    {
        if (_currentMp >= amount) { _currentMp -= amount; }
        else { _currentMp = 0; }
    }

}
