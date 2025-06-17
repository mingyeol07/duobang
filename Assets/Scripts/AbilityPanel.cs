using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text mainValue;
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private TMP_Text valueAmount;

    [SerializeField] private TMP_Text price;

    [SerializeField] private string unique = "";
    [SerializeField] private bool isUnique = false;

    public void Setup(int mainValue, int valueAmount, int price, int level, string lastUnit = "")
    {
        if (unique.Length > 0)
        {
            this.mainValue.text = unique.ToString() + mainValue.ToString() + lastUnit;
        }
        else
        {
            this.mainValue.text = mainValue.ToString() + lastUnit;
        }
        this.valueAmount.text = "+" + valueAmount.ToString() + lastUnit;

        this.price.text = price.ToString();
        levelText.text = "Lv. " + level.ToString();
    }

    public void Setup(float mainValue, float valueAmount, int price, int level, string lastUnit ="")
    {
        if (unique.Length > 0)
        {
            this.mainValue.text = unique.ToString() + mainValue.ToString("F1") + lastUnit   ;
        }
        else
        {
            this.mainValue.text = mainValue.ToString("F1") + lastUnit;
        }
        this.valueAmount.text = "+" + valueAmount.ToString("F1") + lastUnit;

        this.price.text = price.ToString();
        levelText.text = "Lv. " + level.ToString();
    }
}
