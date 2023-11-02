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
        // Suurenda m�ngija raha enne objekti h�vitamist
        Events.SetGold(Events.RequestGold() + MoneyReward);
        Destroy(gameObject); // h�vita vaenlane
    }
}
