using UnityEngine;

public class Health : MonoBehaviour
{
    
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject deathEffect;
   
    bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;
        
        currentHealth -= damage;
        Debug.Log(gameObject.name + " Hasar aldi: " + damage + " | Kalan Can: " + currentHealth);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        PlayerAnimaton animController = GetComponent<PlayerAnimaton>();
        if (animController != null)
        {
            animController.TriggerDeathAnimation();
        }
        
        Invoke("DestroySelf", 2f);
    }
    public void DestroySelf()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    
        Destroy(gameObject);
        Debug.Log(gameObject.name + " tamamen yok edildi!");
    }
}