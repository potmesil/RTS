using UnityEngine;
using static StaticTypes;

[CreateAssetMenu(menuName = "Datascripts/TeamData")]
public class TeamData : ScriptableObject
{
    public Material[] TeamMaterials;

    public Material GetTeamMaterial(TeamType teamNumber)
    {
        return TeamMaterials[(int)teamNumber];
    }
}