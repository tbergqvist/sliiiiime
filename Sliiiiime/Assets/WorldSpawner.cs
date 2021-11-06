using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    public GameObject slime;

    float speedDropTime;
    float healthDropTime;
    float repeatTime = 15;
    float bombDropTime;
    float enemySpawnTime;

    public GameObject speedDrop;
    public GameObject healthDrop;
    public GameObject bombDrop;
    public GameObject Enemy;

    private float enemySpawnRate = 25;
    private void Start()
    {
        speedDropTime = Time.time + repeatTime;
        healthDropTime = Time.time + repeatTime * 2;
        bombDropTime = Time.time + repeatTime;
        enemySpawnTime = Time.time + enemySpawnRate;
    }

    private void Update()
    {
        Vector3 spawnPos = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        if (Time.time > speedDropTime)
        {
            var d = Instantiate(speedDrop, spawnPos, Quaternion.identity);
            Destroy(d, 30);
            speedDropTime = Time.time + repeatTime + Random.Range(0, 15);
        }
        if (Time.time > healthDropTime)
        {
            var d = Instantiate(healthDrop, spawnPos, Quaternion.identity);
            Destroy(d, 30);
            healthDropTime = Time.time + repeatTime + Random.Range(0, 15);
        }
        if(Time.time > bombDropTime)
        {
            var d = Instantiate(bombDrop, spawnPos, Quaternion.identity);
            Destroy(d, 30);
            bombDropTime = Time.time + repeatTime - Random.Range(5, 15); ;
        }
    }
}
