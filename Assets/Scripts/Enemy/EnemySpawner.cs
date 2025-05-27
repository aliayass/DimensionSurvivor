using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject batPrefab;
    public Transform player;
    public float spawnRadius = 10f;
    public float initialSpawnInterval = 3f;
    public float minSpawnInterval = 0.5f;
    public float spawnIntervalDecreaseRate = 0.05f; // Her dalgada aralık ne kadar azalsın
    public int initialBatsPerWave = 1; // Başlangıçta 1 yarasa
    public int batsIncreasePerWave = 1; // Her artışta 1 yarasa artış

    private float currentSpawnInterval;
    private int currentBatsPerWave;
    private float timer;
    private int waveCount = 0; // Dalga sayısını takip etmek için
    private PlayerStats playerStats;

    void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        currentBatsPerWave = initialBatsPerWave;
        timer = currentSpawnInterval;
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().transform;
        }
        playerStats = FindObjectOfType<PlayerStats>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnWave();
            waveCount++;
            
            // Her 5 dalgada bir yarasa sayısını artır
            if (waveCount % 5 == 0)
            {
                currentBatsPerWave += batsIncreasePerWave;
            }
            
            // Dalga başına spawn aralığını azalt
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalDecreaseRate);
            timer = currentSpawnInterval;
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < currentBatsPerWave; i++)
        {
            Vector2 spawnPos = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(batPrefab, spawnPos, Quaternion.identity);
        }
    }

    void OnDestroy()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        if (stats != null)
        {
            stats.AddExperience(5);
        }
    }
} 