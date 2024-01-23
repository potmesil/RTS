using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class AttackerBehaviour : MonoBehaviour
{
    [Header("Enemy stats")]
    public float Range = 5f;
    public float Damage = 50f;
    public float GunCooldown = 1f;
    public float ProjectileSpeed = 5f;

    [Header("Unity setup")]
    public RTSObject TargetingEnemy;
    public GameObject Projectile;

    DetectionComponent _detectionComponent;
    RTSObject _myRTSObject;
    NavMeshAgent _navMeshAgent;
    AudioSource _audioSource;
    float _lastTimeShot = Mathf.NegativeInfinity;

    void Awake()
    {
        _detectionComponent = GetComponentInChildren<DetectionComponent>();
        _myRTSObject = GetComponent<RTSObject>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
    }
    
    void FixedUpdate()
    {
        if (TargetingEnemy == null)
        {
            FindClosestEnemy();
        }

        if (TargetingEnemy != null)
        {
            AttackEnemy();
        }
    }

    public void SetTarget(RTSObject target)
    {
        if (target.TeamNumber != _myRTSObject.TeamNumber)
        {
            TargetingEnemy = target;
        }
        else
        {
            _myRTSObject.GoHere(target.transform.position);
        }
    }

    bool IsEnemyInRange()
    {
        return (TargetingEnemy.transform.position - transform.position).magnitude <= Range;
    }

    void FindClosestEnemy()
    {
        RTSObject closestEnemy = null;
        float minDistance = Mathf.Infinity;

        _detectionComponent.EnemiesInRange = _detectionComponent.EnemiesInRange.Where(x => x != null).ToList();

        foreach (var enemy in _detectionComponent.EnemiesInRange)
        {
            var distance = (enemy.transform.position - transform.position).magnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = enemy;
            }
        }

        TargetingEnemy = closestEnemy;
    }

    void AttackEnemy()
    {
        if (IsEnemyInRange())
        {
            TryToShoot();
        }
        else
        {
            MoveToTarget();
        }
    }

    void TryToShoot()
    {
        if (_lastTimeShot + GunCooldown < Time.time)
        {
            _lastTimeShot = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        _audioSource.Play();

        GameObject projectile = Instantiate(Projectile, transform.position, Quaternion.identity, null);
        projectile.GetComponent<Projectile>().SetupProjectile(Damage, ProjectileSpeed, TargetingEnemy, _myRTSObject.TeamNumber);
    }

    void MoveToTarget()
    {
        Vector3 vectorToAttacker = transform.position - TargetingEnemy.transform.position;
        Vector3 directionOfAttacker = vectorToAttacker.normalized;
        Vector3 targetPosition = TargetingEnemy.transform.position + directionOfAttacker * (Range - 1f);

        _navMeshAgent.destination = targetPosition;
    }
}