using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base script for all weapon controllers
/// </summary>
public class WeaponController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    protected float currentCooldown;
    protected float baseCooldown;

    protected PlayerStats playerStats;

    protected virtual void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        baseCooldown = weaponData.CooldownDuration;
        currentCooldown = weaponData.CooldownDuration;
        // Seviye başlatma
        if (playerStats != null)
        {
            OnPlayerLevelUp(playerStats.playerLevel);
        }
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }

    // Elmaslarla skill açma fonksiyonu
    public void UpgradeSkill(int cost)
    {
        // diamondCount özelliği yok, bu yüzden burası boş bırakıldı veya başka bir şekilde yönetilebilir.
    }

    protected void OnPlayerLevelUp(int level)
    {
        // Her seviye için cooldown'u %10 azalt, minimum 0.1f olsun
        weaponData.CooldownDuration = Mathf.Max(baseCooldown * Mathf.Pow(0.9f, level-1), 0.1f);
    }
}
