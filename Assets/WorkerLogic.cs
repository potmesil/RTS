using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WorkerLogic : MonoBehaviour
{
    public enum WorkerStates
    {
        Idle,
        Collect,
        Return
    }

    [Header("Game stats")]
    public float CollectedResources;
    public float MaxCollectedResources;
    public float CollectionRate;

    [Header("Unity setup")]
    public GatherPoint CurrentGatherPoint;
    public WorkerStates State = WorkerStates.Idle;
    public MeshRenderer CrystalInventory;

    [Header("PrioritySetup")]
    public int CollectPriority = 1;
    public int IdlePriority = 2;
    public int ReturnPriority = 0;

    private MainBuildingLogic _mainBuilding;
    private RTSObject _myRTSObject;

    
    private void Awake()
    {
        _myRTSObject = GetComponent<RTSObject>();
    }

    private void Update()
    {
        if (CrystalInventory.enabled && CollectedResources == 0)
        {
            CrystalInventory.enabled = false;
        }

        if (!CrystalInventory.enabled && CollectedResources > 0f)
        {
            CrystalInventory.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        switch (State)
        {
            case WorkerStates.Idle:
                break;
            case WorkerStates.Collect:
                CollectResourceUpdate();
                break;
            case WorkerStates.Return:
                ReturnResourceUpdate();
                break;
            default: throw new ArgumentOutOfRangeException();
        }
    }
    
    public void GoGatherHere(GatherPoint gatherPoint)
    {
        ResetGatherPoint();

        if (gatherPoint.CurrentWorker == null)
        {
            gatherPoint.CurrentWorker = this;
            CurrentGatherPoint = gatherPoint;

            GoToGatherPoint();
        }
    }

    void ResetGatherPoint()
    {
        if (CurrentGatherPoint != null)
        {
            CurrentGatherPoint.CurrentWorker = null;
            CurrentGatherPoint.StopMining();
            CurrentGatherPoint = null;
        }
    }

    private void GoToGatherPoint()
    {
        if (CurrentGatherPoint != null)
        {
            State = WorkerStates.Collect;
            _myRTSObject.NavAgent.avoidancePriority = CollectPriority;
            _myRTSObject.GoHere(CurrentGatherPoint.GatherLocation.position);
        }
    }

    public void SetMainBuilding(MainBuildingLogic mainBuilding)
    {
        _mainBuilding = mainBuilding;
    }

    public void GoToMainBuilding()
    {
        State = WorkerStates.Return;
        _myRTSObject.NavAgent.avoidancePriority = ReturnPriority;
        _myRTSObject.GoHere(_mainBuilding.SpawnLocation.position);
    }

    public void GoToMainBuilding(MainBuildingLogic newBuilding)
    {
        ResetGatherPoint();
        _mainBuilding = newBuilding;
        GoToMainBuilding();
    }


    private void CollectResourceUpdate()
    {
        float distanceToCollectionPoint = FindDistanceToTarget();

        if (distanceToCollectionPoint < 0.2f)
        {
            CollectResourcesNow();
            CurrentGatherPoint.StartMining();
        }
    }

    private float FindDistanceToTarget()
    {
        Vector2 workerPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 destionationPos = new Vector2(_myRTSObject.CurrentDestionation.x, _myRTSObject.CurrentDestionation.z);

        return (destionationPos - workerPos).magnitude;
    }

    private void CollectResourcesNow()
    {
        float collectionPerFrame = CollectionRate * Time.fixedDeltaTime;

        CollectedResources += collectionPerFrame;

        if (CollectedResources > MaxCollectedResources)
        {
            CurrentGatherPoint.StopMining();
            CollectedResources = MaxCollectedResources;
            GoToMainBuilding();
        }
    }


    private void ReturnResourceUpdate()
    {
        float distanceToMainBuilding = FindDistanceToTarget();

        if (distanceToMainBuilding < 0.5f)
        {
            ReturnResourceNow();
        }
    }

    private void ReturnResourceNow()
    {
        PlayerData.Shared.AddCrystals(CollectedResources);
        CollectedResources = 0f;

        GoToGatherPoint();
    }

    public void GoToLocation(Vector3 location)
    {
        ResetGatherPoint();

        State = WorkerStates.Idle;
        _myRTSObject.NavAgent.avoidancePriority = IdlePriority;
        _myRTSObject.GoHere(location);
    }
}