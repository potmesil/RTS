using UnityEngine;
using static StaticTypes;

public class Projectile : MonoBehaviour
{
    public GameObject ExplosionEffect;

    float _damage = 0f;
    float _projectileSpeed = 0f;
    TeamType _teamNumber;
    RTSObject _enemy;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (_enemy == null)
        {
            DestroyProjectile();
            return;
        }

        Move();
    }

    public void SetupProjectile(float damage, float projectileSpeed, RTSObject enemy, TeamType teamNumber)
    {
        _damage = damage;
        _projectileSpeed = projectileSpeed;
        _enemy = enemy;
        _teamNumber = teamNumber;
    }

    void Move()
    {
        float moveFrame = _projectileSpeed * Time.fixedDeltaTime;
        Vector3 direction = (_enemy.transform.position - transform.position).normalized;
        Vector3 moveVector = direction * moveFrame;

        transform.Translate(moveVector);
    }

    void OnTriggerEnter(Collider other)
    {
        RTSObject otherRTS = other.GetComponent<RTSObject>();

        if (otherRTS != null && otherRTS.TeamNumber != _teamNumber)
        {
            otherRTS.GetHurt(_damage);
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        GameObject effect = Instantiate(ExplosionEffect, transform.position, Quaternion.identity, null);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}