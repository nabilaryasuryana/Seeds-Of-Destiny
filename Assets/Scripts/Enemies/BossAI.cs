using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField] private float attackRange = 5f; // Jarak di mana bos akan menyerang
    [SerializeField] private float chaseRange = 10f; // Jarak di mana bos akan mulai mengejar
    [SerializeField] private float stopChaseRange = 3f; // Jarak di mana bos berhenti mengejar
    [SerializeField] private List<MonoBehaviour> attackTypes; // Daftar tipe serangan yang tersedia
    [SerializeField] private float attackCooldown = 2f; // Cooldown serangan
    [SerializeField] private bool stopMovingWhileAttacking = false; // Apakah bos berhenti bergerak saat menyerang

    private bool isAttacking = false; // Status apakah sedang menyerang
    private bool isAttackOnCooldown = false; // Status apakah serangan sedang cooldown
    private MonoBehaviour currentAttackType;

    private enum State
    {
        Idle,
        Chasing,
        Attacking
    }

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Idle;
        ChooseRandomAttackType(); // Set serangan awal
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

        // Bos mulai mengejar jika berada dalam chaseRange tetapi di luar stopChaseRange
        if (distanceToPlayer < chaseRange && distanceToPlayer > stopChaseRange)
        {
            state = State.Chasing;
        }
        // Jika terlalu dekat tapi masih di dalam attackRange, langsung pindah ke state Attacking
        else if (distanceToPlayer <= attackRange && !isAttackOnCooldown)
        {
            state = State.Attacking;
        }
    }

    private void Chasing()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        // Pindah ke Idle jika terlalu dekat dengan player
        if (distanceToPlayer <= stopChaseRange)
        {
            state = State.Idle;
            enemyPathfinding.StopMoving();
        }
        else
        {
            // Update target secara langsung ke posisi player
            enemyPathfinding.MoveTo(PlayerController.Instance.transform.position); // Bos terus mengejar player secara langsung
        }

        // Pindah ke attacking jika berada dalam attackRange dan tidak sedang cooldown
        if (distanceToPlayer <= attackRange && !isAttackOnCooldown)
        {
            state = State.Attacking;
        }
        else if (distanceToPlayer > chaseRange) // Kembali ke Idle jika player keluar dari chaseRange
        {
            state = State.Idle;
        }
    }

    private void Attacking()
    {
        if (isAttacking) return; // Cegah serangan berulang saat serangan sedang berlangsung

        float distanceToPlayer = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        // Jika keluar dari jangkauan serang, kembali mengejar atau ke Idle
        if (distanceToPlayer > attackRange)
        {
            state = State.Chasing;
            return;
        }

        // Memulai serangan
        isAttacking = true;

        // Stop movement if needed
        if (stopMovingWhileAttacking)
        {
            enemyPathfinding.StopMoving();
        }

        // Execute the current attack type
        (currentAttackType as IEnemy).Attack();

        // Mulai cooldown setelah serangan selesai
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine()
    {
        // Tandai bahwa serangan sedang cooldown
        isAttackOnCooldown = true;

        // Tunggu hingga serangan selesai dan cooldown habis
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false; // Serangan selesai
        isAttackOnCooldown = false; // Cooldown selesai

        // Kembali ke state Chasing saat cooldown, jika player masih dalam jangkauan chase
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
            int randomIndex = Random.Range(0, attackTypes.Count);
            currentAttackType = attackTypes[randomIndex];
        }
    }
}
