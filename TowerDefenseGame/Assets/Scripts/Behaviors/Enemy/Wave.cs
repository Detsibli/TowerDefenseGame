using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public WaveEnemies[] enemies;

    [System.Serializable]
    public class WaveEnemies
    {
        public GameObject enemy;
        public int amount;
        public float rate;
    }
}
