using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float startSpeed = 10f;
    private float speed;
    public int damageToBase;
    public bool armored = false;
    public GameObject canvas;

    private Transform target;
    private int waypointIndex = 0;

    void Start()
    {
        target = Waypoints.waypoints[waypointIndex];
        speed = startSpeed;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) <= 0.001f)
        {
            waypointIndex++;

            if(waypointIndex >= Waypoints.waypoints.Length)
            {
                Destroy(gameObject);
                WaveSpawner.EnemiesAlive--;
                GameObject.FindWithTag("Manager").GetComponent<BaseHealth>().takeDamage(damageToBase);
            }
            else
            {
                target = Waypoints.waypoints[waypointIndex];

                if (armored)
                    transform.Rotate(0, 90, 0);
            }
        }

        speed = startSpeed;
    }

    public void Slow (float amount)
    {
        speed = speed * amount;
    }

    public void SetWaypointIndex(int index)
    {
        waypointIndex = index;
    }

    public int GetWaypointIndex()
    {
        return waypointIndex;
    }
}
