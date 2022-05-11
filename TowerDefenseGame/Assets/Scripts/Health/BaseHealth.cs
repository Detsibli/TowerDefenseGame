using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Image healthBar;
    public GameManager gameManager;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = (float) currentHealth / (float) maxHealth;

        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            gameManager.EndGame();
        }
    }
}
