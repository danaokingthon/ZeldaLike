using UnityEngine;
using UnityEngine.AI;
public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}

public class Goblin : Enemy
{
    public NavMeshAgent navMeshAgent;
    public Transform player;

    public EnemyState currentState = EnemyState.Idle;

    [SerializeField] private float damage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                break;

            case EnemyState.Attacking:
                break;
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        currentState = EnemyState.Chasing;

        if (Time.time >= nextAttackTime)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                Debug.Log("Goblin golpeó al jugador");

                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentState = EnemyState.Idle;
            Debug.Log("Goblin dejó de perseguir al jugador.");
        }
    }
}