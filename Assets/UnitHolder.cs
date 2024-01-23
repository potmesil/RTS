using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHolder : MonoBehaviour
{
    public static UnitHolder shared;

    void Awake()
    {
        if (shared == null)
        {
            shared = this;
        }
    }
}