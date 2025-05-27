using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController
{
    private Camera mainCamera;
    private float viewportMargin = 0.1f; // Görüş alanının %90'ı için kenar boşluğu

    protected override void Start()
    {
        base.Start();
        // Başlangıç cooldown süresini 2 saniye olarak ayarla
        weaponData.CooldownDuration = 2f;
        baseCooldown = 2f;
        currentCooldown = 2f;
        mainCamera = Camera.main;
    }

    protected override void Attack()
    {
        base.Attack();
        
        // En yakın düşmanı bul
        GameObject nearestEnemy = FindNearestEnemy();
        Vector2 direction;

        if (nearestEnemy != null)
        {
            // En yakın düşmana doğru yönlendir
            direction = (nearestEnemy.transform.position - transform.position).normalized;
        }
        else
        {
            // Düşman yoksa sağa doğru at (veya istediğin bir varsayılan yön)
            direction = Vector2.right;
        }

        // Kılıcı oluştur ve yönlendir
        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position;
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(direction);
    }

    private GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearest = null;
        float minDistance = float.MaxValue;

        // Kamera görüş alanının sınırlarını hesapla
        Vector2 viewportMin = new Vector2(viewportMargin, viewportMargin);
        Vector2 viewportMax = new Vector2(1 - viewportMargin, 1 - viewportMargin);

        foreach (GameObject enemy in enemies)
        {
            // Düşmanın ekrandaki konumunu viewport koordinatlarına çevir
            Vector3 viewportPoint = mainCamera.WorldToViewportPoint(enemy.transform.position);
            
            // Düşman görüş alanı içinde mi kontrol et
            if (viewportPoint.x >= viewportMin.x && viewportPoint.x <= viewportMax.x &&
                viewportPoint.y >= viewportMin.y && viewportPoint.y <= viewportMax.y &&
                viewportPoint.z > 0) // Düşman kameranın önünde mi
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = enemy;
                }
            }
        }

        return nearest;
    }
}
