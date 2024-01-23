using UnityEngine;
using static StaticTypes;

public class UnitBuilder : MonoBehaviour
{
    public static UnitBuilder shared;

    void Awake()
    {
        if (shared == null)
        {
            shared = this;
        }

        ToggleCanvas(false);
    }

    public void BuildUnit(UnitType unitType)
    {
        var unitCost = DataManager.shared.AllUnits.GetUnit(unitType).UnitCost;

        if (PlayerData.Shared.CrystalAmount >= unitCost)
        {
            SelectionManager.Shared.SelectedObject.GetComponent<MainBuildingLogic>()?.SpawnUnit(unitType);
            PlayerData.Shared.RemoveCrystals(unitCost);
        }
    }

    public void ToggleCanvas(bool toggle)
    {
        GetComponent<Canvas>().enabled = toggle;
    }
}