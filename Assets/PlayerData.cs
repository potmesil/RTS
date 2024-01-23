using UnityEngine;
using static StaticTypes;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Shared;

    public float CrystalAmount;

    public TeamType TeamNumber;

    private void Awake()
    {
        if (Shared == null)
        {
            Shared = this;
        }
    }

    public void AddCrystals(float crystals)
    {
        CrystalAmount += crystals;
    }

    public void RemoveCrystals(float crystals)
    {
        CrystalAmount -= crystals;
    }
}