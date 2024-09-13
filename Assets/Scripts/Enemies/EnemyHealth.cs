using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;
    private BossDropRate bossDropRate;

    private void Awake() {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
        bossDropRate = GetComponent<BossDropRate>();
    }

    private void Start() {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine() {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        DetectDeath();
    }

    public void DetectDeath() {
        if (currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            GetComponent<PickUpSpawner>().DropItems();
            if (bossDropRate != null)
            {
                bossDropRate.DropItems();
            }
            Destroy(gameObject);
        }
    }

    // Metode baru untuk mendeteksi kematian tanpa mendrop item
    public void DetectDeathWithoutDrop() {
        Destroy(gameObject);
    }

    public int StartingHealth()
    {
        return startingHealth;
    }

    public int CurrentHealth()
    {
        return currentHealth;
    }

    public void SetHealth(int health)
    {
        currentHealth = health;
    }
}
