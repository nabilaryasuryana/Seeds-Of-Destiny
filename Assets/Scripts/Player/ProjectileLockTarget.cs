using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLockTarget : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;
    [SerializeField] private int projectileHealth = 1; // Variabel nyawa

    private Transform playerTransform;
    private Vector3 startPosition;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }

        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Peluru terkena objek: " + other.gameObject.name);

        if (other.CompareTag("PlayerSword"))
        {
            // Jika peluru terkena objek dengan tag "PlayerSword"
            Debug.Log("Peluru terkena PlayerSword!");
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
            return;
        }

        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();

        if (!other.isTrigger && (enemyHealth || indestructible || player))
        {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                if (player && isEnemyProjectile)
                {
                    player.TakeDamage(1, transform);
                }

                projectileHealth--;
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);

                if (projectileHealth <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else if (!other.isTrigger && indestructible)
            {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void DetectFireDistance()
    {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
        }
    }
}
