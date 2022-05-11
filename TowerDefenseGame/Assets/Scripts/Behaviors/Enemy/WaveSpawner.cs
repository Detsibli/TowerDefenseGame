using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAlive = 0;
    public int moneyPerWave = 0;
    bool moneyGiven;

    public Wave[] waves;

    public Transform spawnPoint;

    public float waveInterval = 5f;
    private float waveTimer;

    public TextMeshProUGUI waveCountdownText;
    public static int waveNum;

    public GameManager gameManager;

    void Start()
    {
        EnemiesAlive = 0;
        waveNum = 0;
        moneyGiven = true;
        waveTimer = waveInterval;
    }

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            moneyGiven = false;
            return;
        }

        if (!moneyGiven)
        {
            if(waveNum < 4)
                PlayerStats.Money += moneyPerWave;

            if (waveNum <= 2)
                moneyPerWave += 125;

            moneyGiven = true;
        }

        if (waveTimer <= 0)
        {
            StartCoroutine(SpawnWave());
            waveTimer = waveInterval;
        }

        waveTimer -= Time.deltaTime;
        waveTimer = Mathf.Clamp(waveTimer, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", waveTimer);

        if (waveNum == waves.Length)
        {
            gameManager.WinLevel();
            this.enabled = false;
        }
    }

    IEnumerator SpawnWave()
    {
        Wave wave = waves[waveNum];

        for (int i = 0; i < wave.enemies.Length; i++)
        {
            EnemiesAlive += wave.enemies[i].amount;
            for (int x = 0; x < wave.enemies[i].amount; x++)
            {
                SpawnEnemy(wave.enemies[i].enemy);
                yield return new WaitForSeconds(wave.enemies[i].rate);
            }
        }

        waveNum++;
        PlayerStats.Rounds++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
