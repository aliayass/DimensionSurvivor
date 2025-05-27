using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Experience/Level")]
    public int playerLevel = 1;
    public int experience = 0;
    public int experienceToLevel = 100;
    public float experienceMultiplier = 1.2f;

    [Header("Health")]
    public float maxHealth = 100f;
    public float currentHealth;
    public Slider healthBar;
    public Text healthText;

    [Header("UI References")]
    public Slider experienceBar;
    public Text levelText;

    [Header("Game Over")]
    public GameOverManager gameOverManager;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar == null)
        {
            Debug.LogWarning("HealthBar referansı atanmamış!");
        }
        if (experienceBar == null)
        {
            Debug.LogWarning("ExperienceBar referansı atanmamış!");
        }
        UpdateHealthUI();
        UpdateExperienceUI();
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player died!");
            if (gameOverManager != null)
                gameOverManager.ShowGameOver();
        }
        UpdateHealthUI();
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
            if (healthBar.fillRect != null)
            {
                Image fillImage = healthBar.fillRect.GetComponent<Image>();
                if (fillImage != null)
                {
                    fillImage.color = Color.red;
                }
            }
        }
        if (healthText != null)
        {
            healthText.text = string.Format("{0}/{1}", Mathf.Ceil(currentHealth), maxHealth);
        }
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        CheckLevelUp();
        UpdateExperienceUI();
    }

    private void CheckLevelUp()
    {
        if (experience >= experienceToLevel)
        {
            playerLevel++;
            experience -= experienceToLevel;
            experienceToLevel = Mathf.RoundToInt(experienceToLevel * experienceMultiplier);
            maxHealth += 10f;
            currentHealth += 10f;
            UpdateHealthUI();
        }
    }

    private void UpdateExperienceUI()
    {
        if (experienceBar != null)
        {
            experienceBar.maxValue = experienceToLevel;
            experienceBar.value = experience;
            if (experienceBar.fillRect != null)
            {
                Image fillImage = experienceBar.fillRect.GetComponent<Image>();
                if (fillImage != null)
                {
                    fillImage.color = Color.blue;
                }
            }
        }
        if (levelText != null)
        {
            levelText.text = string.Format("Level {0}", playerLevel);
        }
    }

    private void LateUpdate()
    {
        // UI'ın her zaman kameraya dönük olmasını sağla
        if (healthBar != null)
        {
            healthBar.transform.rotation = Camera.main.transform.rotation;
        }
        if (experienceBar != null)
        {
            experienceBar.transform.rotation = Camera.main.transform.rotation;
        }
    }
} 