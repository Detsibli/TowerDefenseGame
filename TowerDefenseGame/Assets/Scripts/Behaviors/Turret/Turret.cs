using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private EnemyHealth targetEnemy;
    private EnemyMovement enemy;

    [Header("General")]
    public float range;

    [Header("Bullets")]
    public float fireRate;
    private float fireTimer = 0f;
    public GameObject bulletPrefab;

    [Header("Laser")]
    public bool useLaser = false;

    public int damageOverTime = 8;
    public float slowPercent = .5f;

    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;

    [Header("Unity Setup Fields")]
    public string enemyTag = "Enemy";
    public Transform Pivot;
    public float turnSpeed;
    public Transform firePoint;
    public Vector3 TurretBuildOffset;
    public AudioSource shootSFX;

    void Awake()
    {
        transform.position = transform.position + TurretBuildOffset;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Invokes UpdateTarget at 0sec then repeats every half second
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        //Gets array of all GameObjects with tag Enemy
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            //Gets distance of enemy in relation to turret
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            //If enemy distance is less than shortestdistance then
            //Make that shortestdistance the enemydistance and make nearestEnemy that enemy
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        //If there is a nearestEnemy and the shortestDistance is less than
        //The turrets range, then set the target to the position of the nearest enemy
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<EnemyHealth>();
            enemy = nearestEnemy.GetComponent<EnemyMovement>();
        }
        //Else then there is no target.
        else
        {
            target = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Checks every update, if there is no target then do nothing.
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }

            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
            shootSFX.Play();
        }
        else
        {
            if (fireTimer <= 0)
            {
                Shoot();
                fireTimer = 1f / fireRate;
            }

            fireTimer -= Time.deltaTime;
        }
    }

    void LockOnTarget()
    {
        //Get direction, from turret position minus the target's position
        Vector3 dir = target.position - transform.position;

        //Convert that direction to quaternions
        Quaternion lookRotation = Quaternion.LookRotation(dir);

        //The line below rotates the object's pivot to face the enemy smoothly, turnSpeed can be changed.
        Vector3 rotation = Quaternion.Lerp(Pivot.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;

        //Rotate the pivot of the turrent in relation to a quaternion passed through a eulers to smooth it out.
        Pivot.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.takeDamage(damageOverTime * Time.deltaTime);
        enemy.Slow(slowPercent);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        // beam using line renderer from turret to enemy
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        // have effect start outside of enemy
        impactEffect.transform.position = target.position + dir.normalized * (target.localScale.x / 2);

        // effect should face turret
        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        shootSFX.Play();

        if (bullet != null)
        {
            bullet.Chase(target);
        }

        Debug.Log("You have shot!");
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
