using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text mainValue;
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private TMP_Text valueAmount;

    [SerializeField] private TMP_Text price;

    public void Setup(int mainValue, int valueAmount, int price, int level)
    {
        this.mainValue.text = mainValue.ToString();
        this.valueAmount.text = "+" + valueAmount.ToString();

        this.price.text = price.ToString();
        levelText.text = level.ToString();
    }

    public void Setup(float mainValue, float valueAmount, int price, int level)
    {
        this.mainValue.text = mainValue.ToString("F1");
        this.valueAmount.text = "+" + valueAmount.ToString("F1");

        this.price.text = price.ToString();
        levelText.text = level.ToString();
    }
}
