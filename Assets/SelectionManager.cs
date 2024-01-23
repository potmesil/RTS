using UnityEngine;
using UnityEngine.EventSystems;
using static StaticTypes;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Shared;

    [Header("Game stats")]
    public RTSObject SelectedObject;

    [Header("Unity setup")]
    public Camera MainCamera;

    private void Awake()
    {
        if (Shared == null)
        {
            Shared = this;
        }
    }

    public void SelectThisObject(RTSObject rtsObject)
    {
        DeselectAll();

        if (rtsObject.TeamNumber == PlayerData.Shared.TeamNumber)
        {
            rtsObject.SelectionToggle(true);
            rtsObject.GetComponent<MainBuildingLogic>()?.SelectionToggle(true);

            SelectedObject = rtsObject;
        }
    }


    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetAxis("Fire2") > 0f && SelectedObject != null)
            {
                SendRTSObject();
            }
        }
    }
    
    

    public void DeselectAll()
    {
        if (SelectedObject != null)
        {
            SelectedObject.SelectionToggle(false);
            SelectedObject.GetComponent<MainBuildingLogic>()?.SelectionToggle(false);
            SelectedObject = null;
        }
    }

    void SendRTSObject()
    {
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            if (SelectedObject.GetComponent<WorkerLogic>() != null)
            {
                WorkerLogicMethod(hit);
            }

            if (SelectedObject.GetComponent<AttackerBehaviour>() != null)
            {
                AttackerLogicMethod(hit);
            }
        }
    }

    private void AttackerLogicMethod(RaycastHit hit)
    {
        AttackerBehaviour selectedAttacker = SelectedObject.GetComponent<AttackerBehaviour>();

        if (hit.collider.GetComponent<FloorLogic>() != null)
        {
            SelectedObject.GoHere(hit.point);
        }
        else if (hit.collider.GetComponent<RTSObject>() != null)
        {
            selectedAttacker.SetTarget(hit.collider.GetComponent<RTSObject>());
        }
    }

    void WorkerLogicMethod(RaycastHit hit)
    {
        WorkerLogic selectedWorker = SelectedObject.GetComponent<WorkerLogic>();

        if (hit.collider.GetComponent<FloorLogic>() != null)
        {
            selectedWorker.GoToLocation(hit.point);
        }
        else if (hit.collider.GetComponent<GatherPoint>() != null)
        {
            selectedWorker.GoGatherHere(hit.collider.GetComponent<GatherPoint>());
        }
        else if (hit.collider.GetComponent<MainBuildingLogic>() != null)
        {
            selectedWorker.GoToMainBuilding(hit.collider.GetComponent<MainBuildingLogic>());
        }
    }
}