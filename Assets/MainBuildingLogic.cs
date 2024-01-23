using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using static StaticTypes;

public class MainBuildingLogic : MonoBehaviour
{
    public Transform SpawnLocation;

    RTSObject _myRTSObject;

    void Awake()
    {
        _myRTSObject = GetComponent<RTSObject>();
    }

    public void SelectionToggle(bool toggle)
    {
        UnitBuilder.shared.ToggleCanvas(toggle);
    }

    public void SpawnUnit(UnitType unitType)
    {
        GameObject prefab = DataManager.shared.AllUnits.GetUnit(unitType).UnitPrefab;
        GameObject unit = Instantiate(prefab, SpawnLocation.position, Quaternion.identity, UnitHolder.shared.transform);

        unit.GetComponent<RTSObject>().TeamNumber = _myRTSObject.TeamNumber;
        unit.GetComponent<WorkerLogic>()?.SetMainBuilding(this);

        SendWorker(unit);
    }
    
    private void SendWorker(GameObject worker)
    {
        GatherPoint[] gatherPoints = FindObjectsOfType<GatherPoint>();
        GatherPoint closestGatherPoint = FindClosestGatherPoint(gatherPoints);

        if (closestGatherPoint != null)
        {
            worker.GetComponent<WorkerLogic>()?.GoGatherHere(closestGatherPoint);
        }
    }

    GatherPoint FindClosestGatherPoint(GatherPoint[] gatherPoints)
    {
        GatherPoint closestGatherPoint = null;
        float minDistance = Mathf.Infinity;

        foreach (var gatherPoint in gatherPoints)
        {
            if (gatherPoint.CurrentWorker == null)
            {
                float distance = (gatherPoint.GatherLocation.transform.position - transform.position).magnitude;

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestGatherPoint = gatherPoint;
                }
            }
        }

        return closestGatherPoint;
    }
}