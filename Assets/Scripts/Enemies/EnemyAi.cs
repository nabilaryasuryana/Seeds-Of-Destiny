using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType; // Harus mengimplementasikan IEnemy
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    [Header("Enemy Settings")]
    [SerializeField] private int enemyNumber; // Nomor untuk ID
    [SerializeField] private string enemyTypeName; // Tipe musuh
    [SerializeField] private string sceneName; // Nama scene

    private string enemyID; // ID unik untuk musuh
    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Attacking
    }

    private Vector2 roamPosition;
    private float timeRoaming = 0f;

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private EnemyHealth enemyHealth;
    private string enemyDataFile;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        enemyHealth = GetComponent<EnemyHealth>();
        state = State.Roaming;

        // Buat ID berdasarkan informasi yang ada
        GenerateUniqueID();
        sceneName = SceneManager.GetActiveScene().name;
        enemyDataFile = Application.persistentDataPath + "/enemy_" + enemyID + ".json";
        LoadEnemyData(); // Load data jika ada
    }

    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        enemyPathfinding.MoveTo(roamPosition);

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if (timeRoaming > roamChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }

        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy)?.Attack(); // Pastikan enemyType mengimplementasikan IEnemy

            if (stopMovingWhileAttacking)
            {
                enemyPathfinding.StopMoving();
            }
            else
            {
                enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    public void ReceiveDamage(int damage)
    {
        enemyHealth?.TakeDamage(damage);
    }

    // Generate unique ID based on number, type, and scene
    private void GenerateUniqueID()
    {
        enemyID = $"{enemyNumber}_{enemyTypeName}_{sceneName}";
    }

    // Menyimpan data musuh
    // Menyimpan data musuh
    public void SaveEnemyData()
    {
        EnemyData enemyData = new EnemyData
        {
            id = enemyID,
            position = transform.position,
            health = enemyHealth.CurrentHealth(),
            isDead = enemyHealth.CurrentHealth() <= 0 // Set isDead langsung berdasarkan kondisi
        };

        string json = JsonUtility.ToJson(enemyData);
        File.WriteAllText(enemyDataFile, json);
        Debug.Log("Enemy Data Saved to JSON");
    }

    // Memuat data musuh
    private void LoadEnemyData()
    {
        if (File.Exists(enemyDataFile))
        {
            string json = File.ReadAllText(enemyDataFile);
            EnemyData enemyData = JsonUtility.FromJson<EnemyData>(json);

            // Pastikan data yang dimuat sesuai dengan ID
            if (enemyData.id == enemyID)
            {
                transform.position = enemyData.position;

                if (!enemyData.isDead)
                {
                    enemyHealth.SetHealth(enemyData.health);
                }
                else
                {
                    enemyHealth.DetectDeathWithoutDrop(); // Jika musuh mati, panggil metode kematian
                }

                Debug.Log("Enemy Data Loaded from JSON");
            }
            else
            {
                Debug.LogWarning("ID mismatch for enemy data, using default values");
            }
        }
        else
        {
            Debug.LogWarning("No enemy data file found, using default values");
        }
    }


    private void OnDestroy()
    {
        SaveEnemyData(); // Save data when the enemy is destroyed
    }
}
