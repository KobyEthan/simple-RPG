using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public CharacterStats characterStats;
    public int currentHealth;
    public int maxHealth;
    public PlayerLevel PlayerLevel { get; set; }

    void Awake()
    {
        PlayerLevel = GetComponent<PlayerLevel>();
        this.currentHealth = this.maxHealth;
        characterStats = new CharacterStats(0, 0);
    }

    public void AddHealth(int amount)
    {
        this.currentHealth += amount;
            if (currentHealth + amount >= maxHealth) 
                this.maxHealth = this.currentHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    public void TakeDamage(int amount)
    {
        this.currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    private void Die()
    {
        Debug.Log("Player dead");
        this.currentHealth = this.maxHealth;
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }
}
