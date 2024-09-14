using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float attackRange = 5f; // Jarak di mana bos akan menyerang
    [SerializeField] private float chaseRange = 10f; // Jarak di mana bos akan mulai mengejar
    [SerializeField] private float stopChaseRange = 3f; // Jarak di mana bos berhenti mengejar
    [SerializeField] private List<MonoBehaviour> attackTypes; // Daftar tipe serangan yang tersedia
    [SerializeField] private float attackCooldown = 2f; // Cooldown serangan
    [SerializeField] private bool stopMovingWhileAttacking = false; // Apakah bos berhenti bergerak saat menyerang

    [Header("Boss Settings")]
    [SerializeField] private int bossNumber; // Nomor untuk ID
    [SerializeField] private string bossTypeName; // Tipe bos
    [SerializeField] private string sceneName; // Nama scene

    private string bossID; // ID unik untuk bos
    private bool isAttacking = false; // Status apakah sedang menyerang
    private bool isAttackOnCooldown = false; // Status apakah serangan sedang cooldown
    private MonoBehaviour currentAttackType;
    private EnemyHealth enemyHealth; // Menambahkan referensi EnemyHealth
    private bool isDead = false; // Status apakah bos sudah mati

    private enum State
    {
        Idle,
        Chasing,
        Attacking
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;
    private string bossDataFile;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        enemyHealth = GetComponent<EnemyHealth>();
        state = State.Idle;

        // Buat ID berdasarkan informasi yang ada
        GenerateUniqueID();
        sceneName = SceneManager.GetActiveScene().name;
        bossDataFile = Application.persistentDataPath + "/boss_" + bossID + ".json";
        LoadBossData();

        ChooseRandomAttackType(); // Set serangan awal
    }

    private void Update()
    {
        if (!isDead) // Jangan update jika bos sudah mati
        {
            MovementStateControl();
        }
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Idle:
                Idle();
                break;

            case State.Chasing:
                Chasing();
                break;

            case State.Attacking:
                Attacking();
                break;
        }
    }

    private void Idle()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distanceToPlayer < chaseRange && distanceToPlayer > stopChaseRange)
        {
            state = State.Chasing;
        }
        else if (distanceToPlayer <= attackRange && !isAttackOnCooldown)
        {
            state = State.Attacking;
        }
    }

    private void Chasing()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distanceToPlayer <= stopChaseRange)
        {
            state = State.Idle;
            enemyPathfinding.StopMoving();
        }
        else
        {
            enemyPathfinding.MoveTo(PlayerController.Instance.transform.position);
        }

        if (distanceToPlayer <= attackRange && !isAttackOnCooldown)
        {
            state = State.Attacking;
        }
        else if (distanceToPlayer > chaseRange)
        {
            state = State.Idle;
        }
    }

    private void Attacking()
    {
        if (isAttacking || isAttackOnCooldown) return;

        float distanceToPlayer = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        if (distanceToPlayer > attackRange)
        {
            state = State.Chasing;
            return;
        }

        isAttacking = true;

        if (stopMovingWhileAttacking)
        {
            enemyPathfinding.StopMoving();
        }

        (currentAttackType as IEnemy)?.Attack();

        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isAttackOnCooldown = true;

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
        isAttackOnCooldown = false;

        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) <= chaseRange)
        {
            state = State.Chasing;
        }

        ChooseRandomAttackType(); // Pilih serangan berikutnya
    }

    private void ChooseRandomAttackType()
    {
        if (attackTypes.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, attackTypes.Count);
            currentAttackType = attackTypes[randomIndex];
        }
    }

    // Generate unique ID based on number, type, and scene
    private void GenerateUniqueID()
    {
        bossID = $"{bossNumber}_{bossTypeName}_{sceneName}";
    }

    // Menyimpan data bos
    public void SaveBossData()
    {
        BossData bossData = new BossData
        {
            id = bossID,
            isDead = enemyHealth.CurrentHealth() <= 0
        };

        string json = JsonUtility.ToJson(bossData);
        File.WriteAllText(bossDataFile, json);
        Debug.Log("Boss Data Saved to JSON");
    }

    // Memuat data bos
    private void LoadBossData()
    {
        if (File.Exists(bossDataFile))
        {
            string json = File.ReadAllText(bossDataFile);
            BossData bossData = JsonUtility.FromJson<BossData>(json);

            if (bossData.id == bossID)
            {
                isDead = bossData.isDead;
                if (isDead)
                {
                    enemyHealth.DetectDeathWithoutDrop(); // Jika bos mati, panggil metode kematian
                }
                Debug.Log("Boss Data Loaded from JSON");
            }
            else
            {
                Debug.LogWarning("ID mismatch for boss data, using default values");
            }
        }
        else
        {
            Debug.LogWarning("No boss data file found, using default values");
        }
    }

    private void OnDestroy()
    {
        SaveBossData(); // Save data when the boss is destroyed
    }
}

[System.Serializable]
public class BossData
{
    public string id;
    public bool isDead;
}
