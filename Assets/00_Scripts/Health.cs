using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} HP: {currentHealth}");
        if (currentHealth <= 0f)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");

        if (CompareTag("Player"))
        {
            StartCoroutine(RestartGame());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
