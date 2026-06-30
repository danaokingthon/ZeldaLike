using System.Collections;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float damage = 10f;
    public Collider damageCollider;
    AnimationCharacter animationCharacter;
    void Start()
    {
        animationCharacter = GetComponent<AnimationCharacter>();
    }

    public float attackCooldown = 1f;
    
    public void PerformAttack()
    {
        if (animationCharacter.isAttacking) return; // Evitar ataques consecutivos 
        animationCharacter.isAttacking = true;
        animationCharacter.animator.SetBool("Attack", animationCharacter.isAttacking);
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);
        foreach (Collider hit in hits)        {
            if (hit.gameObject != gameObject) // Evitar dañarse a sí mismo
            {
                IDamageable damageable = hit.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector3 direction = (hit.transform.position - transform.position).normalized;

                    if (Vector3.Dot(transform.forward, direction) > 0.3f)
                    {
                        damageable.TakeDamage(damage);
                        Debug.Log($"{hit.gameObject.name} ha recibido {damage} de daño.");
                    }
                    damageable.TakeDamage(damage);
                    Debug.Log($"{hit.gameObject.name} ha recibido {damage} de daño.");
                }
            }
        }      
        StartCoroutine(AttackCooldown());  
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        EndAttack();
        yield break;
    }
    public void EndAttack()
    {
        animationCharacter.isAttacking = false;
        animationCharacter.animator.SetBool("Attack", animationCharacter.isAttacking);
    }   
}
