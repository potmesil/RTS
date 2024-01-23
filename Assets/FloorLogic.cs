using UnityEngine;
using UnityEngine.EventSystems;

public class FloorLogic : MonoBehaviour
{
    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            SelectionManager.Shared.DeselectAll();
        }
    }
}