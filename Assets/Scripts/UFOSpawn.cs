using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawn : MonoBehaviour
{
    public float spawnDelay;
    public GameObject ufoPrefab;
    private float lastSpawnTime;

    private Vector3 leftSpawnPoint;
    private Vector3 rightSpawnPoint;

    void Start()
    {
        lastSpawnTime = Time.time;
        leftSpawnPoint = transform.Find("LeftSpawn").position;
        rightSpawnPoint = transform.Find("RightSpawn").position;
    }

    void Update()
    {
        if (Time.time - lastSpawnTime > spawnDelay)
        {
            lastSpawnTime = Time.time;
            SpawnUFO();
        }
    }

    void SpawnUFO()
    {
        bool leftSpawn = Random.value < 0.5f;
        Vector3 spawnPoint =  leftSpawn ? leftSpawnPoint : rightSpawnPoint;
        GameObject ufo = Instantiate(ufoPrefab, spawnPoint, Quaternion.identity);
        UFOBehavior ufoBehavior = ufo.GetComponent<UFOBehavior>();
        ufoBehavior.direction = leftSpawn ? Vector3.right : Vector3.left;
    }
}
