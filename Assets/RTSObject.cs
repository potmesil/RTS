using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static StaticTypes;

[ExecuteInEditMode]
public class RTSObject : MonoBehaviour
{
    public TeamType TeamNumber;
    public float Health;

    public GameObject DieParticles;
    public Canvas SelectionCanvas;

    [HideInInspector]
    public Vector3 CurrentDestionation;

    [HideInInspector]
    public NavMeshAgent NavAgent;

    public void SelectionToggle(bool toggle)
    {
        SelectionCanvas.enabled = toggle;
    }

    public void GoHere(Vector3 location)
    {
        if (NavAgent != null)
        {
            CurrentDestionation = location;
            NavAgent.destination = CurrentDestionation;
        }
    }

    public void GetHurt(float damage)
    {
        Health -= damage;

        if (Health <= 0f)
        {
            Health = 0f;
            Die();
        }
    }

    void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        SelectionToggle(false);
        UpdateTeamColor();
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            SelectionManager.Shared.SelectThisObject(this);
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(DieParticles, transform.position, Quaternion.identity, null);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }

    void UpdateTeamColor()
    {
        if (DataManager.shared != null)
        {
            Material material = DataManager.shared.TeamDataInfo.GetTeamMaterial(TeamNumber);
            MeshRenderer rend = GetComponent<MeshRenderer>();

            rend.sharedMaterial = material;
        }
    }

    void OnValidate()
    {
        UpdateTeamColor();
    }
}