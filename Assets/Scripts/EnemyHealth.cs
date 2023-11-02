using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    public int MoneyReward = 5;
    private float currentHealth;

    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // Suurenda mängija raha enne objekti hävitamist
        Events.SetGold(Events.RequestGold() + MoneyReward);
        Destroy(gameObject); // hävita vaenlane
    }
}
