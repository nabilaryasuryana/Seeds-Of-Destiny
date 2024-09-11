using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterWind : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private float startingDistance = 0.1f;

    private bool isShooting = false;

    private void OnValidate() {
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if (restTime < 0.1f) { restTime = 0.1f; }
        if (startingDistance < 0.1f) { startingDistance = 0.1f; }
        if (bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }
    }

    public void Attack() {
        if (!isShooting) {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        for (int i = 0; i < burstCount; i++)
        {
            Vector2 targetDirection = (PlayerController.Instance.transform.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            Vector2 pos = FindBulletSpawnPos(targetAngle);
            GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            newBullet.transform.right = newBullet.transform.position - transform.position;

            if (newBullet.TryGetComponent(out Projectile projectile))
            {
                projectile.UpdateMoveSpeed(bulletMoveSpeed);
            }

            yield return new WaitForSeconds(timeBetweenBursts);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private Vector2 FindBulletSpawnPos(float currentAngle) {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}