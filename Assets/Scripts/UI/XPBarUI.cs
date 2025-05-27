using UnityEngine;
using UnityEngine.UI;

public class XPBarUI : MonoBehaviour
{
    public Slider xpSlider;
    public Text levelText;

    private PlayerStats playerStats;

    void Start()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            UpdateXPBar(playerStats.experience, playerStats.experienceToLevel, playerStats.playerLevel);
        }
    }

    void Update()
    {
        if (playerStats != null)
        {
            UpdateXPBar(playerStats.experience, playerStats.experienceToLevel, playerStats.playerLevel);
        }
    }

    void UpdateXPBar(int currentXP, int maxXP, int level)
    {
        if (xpSlider != null && maxXP > 0)
            xpSlider.value = (float)currentXP / maxXP;
        if (levelText != null)
            levelText.text = "Level: " + level;
    }
} 