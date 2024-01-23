using System;
using UnityEngine;
using static StaticTypes;

[Serializable]
public class UnitData
{
    [SerializeField]
    public GameObject UnitPrefab;

    [SerializeField]
    public Sprite UnitSprite;

    [SerializeField]
    public int UnitCost;

    [SerializeField]
    public ResourceType Resource;
}