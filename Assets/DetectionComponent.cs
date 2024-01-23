using System.Collections.Generic;
using UnityEngine;
using static StaticTypes;

public class DetectionComponent : MonoBehaviour
{
    public List<RTSObject> EnemiesInRange = new();

    private void OnTriggerEnter(Collider other)
    {
        RTSObject myRTS = GetComponentInParent<RTSObject>();
        RTSObject otherRTS = other.GetComponent<RTSObject>();

        if (otherRTS != null)
        {
            if (myRTS.TeamNumber != otherRTS.TeamNumber)
            {
                EnemiesInRange.Add(otherRTS);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RTSObject otherRTS = other.GetComponent<RTSObject>();

        if (otherRTS != null && EnemiesInRange.Contains(otherRTS))
        {
            EnemiesInRange.Remove(otherRTS);
        }
    }
}