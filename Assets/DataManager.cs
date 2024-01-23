using UnityEngine;

[ExecuteInEditMode]
public class DataManager : MonoBehaviour
{
    public static DataManager shared;

    public AllUnitData AllUnits;
    public TeamData TeamDataInfo;

    void Awake()
    {
        SetupSingleton();
    }

    void OnValidate()
    {
        SetupSingleton();
    }

    void SetupSingleton()
    {
        if (shared == null)
        {
            shared = this;
        }
    }
}