using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public GameObject diamondPrefab; // Elmas prefabı

    //Current stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
        //Assign the vaiables
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;

        // Bat düşmanına otomatik olarak bir CircleCollider2D bileşeni ekle
        if (GetComponent<CircleCollider2D>() == null)
        {
            CircleCollider2D cc = gameObject.AddComponent<CircleCollider2D>();
            cc.isTrigger = true;
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            // XP ekle
            PlayerStats stats = FindObjectOfType<PlayerStats>();
            if (stats != null)
            {
                stats.AddExperience(10); // Başlangıçta daha hızlı XP artışı için 10 XP ver
            }
            DropDiamond(); // Elmas düşür
            Kill();
        }
    }

    void DropDiamond()
    {
        if (diamondPrefab == null)
        {
            // Eğer diamondPrefab null ise, otomatik olarak bir elmas prefabı oluştur
            GameObject diamond = new GameObject("Diamond");
            diamond.tag = "Diamond";
            SpriteRenderer sr = diamond.AddComponent<SpriteRenderer>();
            // Basit bir elmas sprite'ı oluştur
            Texture2D texture = new Texture2D(32, 32);
            Color[] colors = new Color[32 * 32];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.cyan;
            }
            texture.SetPixels(colors);
            texture.Apply();
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 32, 32), new Vector2(0.5f, 0.5f));
            sr.sprite = sprite;
            CircleCollider2D cc = diamond.AddComponent<CircleCollider2D>();
            cc.isTrigger = true;
            diamondPrefab = diamond;
        }
        Instantiate(diamondPrefab, transform.position, Quaternion.identity);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    // Düşman oyuncuya çarptığında can azalt
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.TakeDamage(4f); // Her bir yarasa 4 can azaltsın
            }
        }
    }
}
