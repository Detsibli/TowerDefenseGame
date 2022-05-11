using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    public int moneyGiven;
    public GameObject deathEffect;
    private float currentHealth;
    public bool armored = false;
    private int waveNum;

    public Image healthBar;

    private bool isDead = false;

    void Start()
    {
        if(!armored)
            maxHealth += (WaveSpawner.waveNum * 5);
            
        currentHealth = maxHealth;
    }

    public void takeDamage (float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;

        if (currentHealth <= 0)
        {
            if (!isDead)
            {
                onDeath();
            }
        }
    }

    void onDeath()
    {
        PlayerStats.Money += moneyGiven;

        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        if (armored)
        {
            RemoveArmor();
        }

        Destroy(gameObject);

        WaveSpawner.EnemiesAlive--;
        isDead = true;
    }

    void RemoveArmor()
    {
        int children = transform.childCount;

        while(children > 1)
        {
            Transform child = transform.GetChild(0);
            child.parent = null;
            child.tag = "Enemy";
            WaveSpawner.EnemiesAlive++;

            EnemyMovement childMovement = child.GetComponent<EnemyMovement>();
            childMovement.enabled = true;
            childMovement.canvas.SetActive(true);
            childMovement.SetWaypointIndex(transform.GetComponent<EnemyMovement>().GetWaypointIndex());

            children--;
        }
    }
}
