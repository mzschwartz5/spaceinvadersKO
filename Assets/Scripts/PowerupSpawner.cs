using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] GameObject powerupPrefab;
    [SerializeField] float spawnPeriod;
    private float lastSpawnTime = 0;
    private BoxCollider spawnRegion;
    void Start()
    {
        spawnRegion = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawnTime > spawnPeriod)
        {
            lastSpawnTime = Time.time;
            Vector3 spawnPos = new Vector3(
                Random.Range(spawnRegion.bounds.min.x, spawnRegion.bounds.max.x),
                transform.position.y,
                Random.Range(spawnRegion.bounds.min.z, spawnRegion.bounds.max.z)
            );

            Instantiate(powerupPrefab, spawnPos, Quaternion.identity);
        }
    }
}
