using TMPro;
using UnityEngine;

public class ResourceUpdater : MonoBehaviour
{
    public TextMeshProUGUI CrystalCount;

    private float _crystalAmount;

    void Start()
    {
        UpdateCrystalUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (_crystalAmount != PlayerData.Shared.CrystalAmount)
        {
            UpdateCrystalUI();
        }
    }

    void UpdateCrystalUI()
    {
        _crystalAmount = PlayerData.Shared.CrystalAmount;
        CrystalCount.text = _crystalAmount.ToString();
    }
}
