using UnityEngine;
using UnityEngine.UI;

public class SimpleColorSlider : MonoBehaviour
{
    [SerializeField]       
    private Slider slider;                          
    [SerializeField]
    private Image fillImage;                           
    [SerializeField]
    private Color fullHealthColor = Color.green;
    //public Color zeroHealthColor = Color.red;

    public float currentValue;
    public float maxValue;

    //这里不能OnEnable,因为会造成这里先运行，而playerInfo还没初始化好，娶不到数据
    void Start()
    {
        currentValue = maxValue;
        fillImage.color = fullHealthColor;
        UpdateSliderValue();
    }


    public void UpdateValue(float amount)
    {
        if (currentValue - amount > 0)
        {
            currentValue -= amount;
        }
        else
        {
            currentValue = 0;
        }
        UpdateSliderValue();
    }


    private void UpdateSliderValue()
    {
        slider.value = currentValue/maxValue*100;
        //fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentValue / maxValue);
    }

}