using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
                  
    public Slider slider;                             
    public Image fillImage;                           
    public Color fullHealthColor = Color.green;       
    public Color zeroHealthColor = Color.red;

    public float currentValue;
    public float maxValue;

    private void OnEnable()
    {
        currentValue = maxValue;

        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        if (currentValue - amount > 0)
        {
            currentValue -= amount;
        }
        else
        {
            currentValue = 0;
        }
        SetHealthUI();
    }


    private void SetHealthUI()
    {
        slider.value = currentValue;
        fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentValue / maxValue);
    }

}