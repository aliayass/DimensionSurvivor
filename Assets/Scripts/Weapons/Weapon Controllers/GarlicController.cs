using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    private float baseDuration = 2f; // Temel süre 2 saniye
    private float currentDuration;
    private GameObject currentGarlic;
    public float garlicDamage = 2f; // Her temas için verilecek hasar
    public float damageInterval = 0.5f; // Yarı saniyede bir hasar

    protected override void Start()
    {
        base.Start();
        currentDuration = baseDuration;
        // Cooldown süresini 10 saniye olarak ayarla
        weaponData.CooldownDuration = 10f;
        baseCooldown = 10f;
        currentCooldown = 10f;
    }

    protected override void Attack()
    {
        base.Attack();
        
        // Eğer mevcut bir garlic varsa yok et
        if (currentGarlic != null)
        {
            Destroy(currentGarlic);
        }

        // Yeni garlic oluştur
        currentGarlic = Instantiate(weaponData.Prefab);
        currentGarlic.transform.position = transform.position;
        currentGarlic.transform.parent = transform;

        // GarlicDamageDealer component ekle
        if (currentGarlic.GetComponent<GarlicDamageDealer>() == null)
        {
            var dealer = currentGarlic.AddComponent<GarlicDamageDealer>();
            dealer.damage = garlicDamage;
            dealer.interval = damageInterval;
        }

        // Belirli süre sonra garlic'i yok et
        StartCoroutine(DestroyGarlicAfterDelay());
    }

    private IEnumerator DestroyGarlicAfterDelay()
    {
        yield return new WaitForSeconds(currentDuration);
        if (currentGarlic != null)
        {
            Destroy(currentGarlic);
            currentGarlic = null;
        }
    }

    // Artık override değil, sadece OnEnable
    protected void OnEnable()
    {
        if (playerStats != null)
        {
            // Her seviyede süreyi %10 artır
            currentDuration = baseDuration * (1 + (playerStats.playerLevel - 1) * 0.1f);
        }
    }
}

// Garlic'ın sürekli hasar vermesi için ek component
public class GarlicDamageDealer : MonoBehaviour
{
    public float damage = 2f;
    public float interval = 0.5f;
    private float lastDamageTime = 0f;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (Time.time - lastDamageTime > interval)
            {
                EnemyStats enemy = other.GetComponent<EnemyStats>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                lastDamageTime = Time.time;
            }
        }
    }
}
