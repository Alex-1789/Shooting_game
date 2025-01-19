using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject enemyPrefab;       // Prefab przeciwnika
    public Transform[] spawnPoints;     // Tablica punktów spawnów

    public void SpawnEnemy()
    {
        // Wybierz losowy punkt spawnu
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform chosenSpawnPoint = spawnPoints[randomIndex];

        GameObject newEnemy = Instantiate(enemyPrefab, chosenSpawnPoint.position, Quaternion.identity);

        // Przypisz referencję do spawnera
        Enemy enemyScript = newEnemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.spawner = this; // Przekazanie referencji do spawnera
        }
    }
}
