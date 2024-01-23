using UnityEngine;
using static StaticTypes;

[CreateAssetMenu(menuName = "Datascripts/AllUnitData")]
public class AllUnitData : ScriptableObject
{
    public UnitData Attacker;
    public UnitData Worker;

    public UnitData GetUnit(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Attacker:
                return Attacker;
            case UnitType.Worker:
                return Worker;
            default:
                return null;
        }
    }
}