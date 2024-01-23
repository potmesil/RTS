using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static StaticTypes;

[ExecuteInEditMode]
public class BuildButton : MonoBehaviour
{
    public UnitType UnitToBuild;
    public TextMeshProUGUI CostText;

    void Start()
    {
        UpdateUI();
    }

    void OnValidate()
    {
        UpdateUI();
    }

    public void TryToBuyThis()
    {
        GetComponentInParent<UnitBuilder>().BuildUnit(UnitToBuild);
    }
    
    void UpdateImage()
    {
        if (DataManager.shared != null)
        {
            Sprite sprite = DataManager.shared.AllUnits.GetUnit(UnitToBuild).UnitSprite;
            GetComponent<Image>().sprite = sprite;
        }
    }

    void UpdateText()
    {
        if (DataManager.shared != null)
        {
            CostText.text = DataManager.shared.AllUnits.GetUnit(UnitToBuild).UnitCost.ToString();
        }
    }

    void UpdateUI()
    {
        UpdateImage();
        UpdateText();
    }
}