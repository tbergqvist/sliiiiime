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

    public GameObject speedDrop;
    public GameObject healthDrop;
    public GameObject bombDrop;

    private void Start()
    {
        speedDropTime = Time.time + repeatTime;
        healthDropTime = Time.time + repeatTime * 2;
        bombDropTime = Time.time + repeatTime;
    }

    private void Update()
    {
        if (Time.time > speedDropTime)
        {
            var d = Instantiate(speedDrop, slime.transform.position + new Vector3(Random.Range(1f, 5f), Random.Range(1f, 5f), 0), Quaternion.identity);
            Destroy(d, 30);
            speedDropTime = Time.time + repeatTime;
        }
        if (Time.time > healthDropTime)
        {
            var d = Instantiate(healthDrop, slime.transform.position + new Vector3(Random.Range(1f, 5f), Random.Range(1f, 5f), 0), Quaternion.identity);
            Destroy(d, 30);
            healthDropTime = Time.time + repeatTime * 2;
        }
        if (Time.time > bombDropTime)
        {
            var d = Instantiate(bombDrop, slime.transform.position + new Vector3(Random.Range(1f, 5f), Random.Range(1f, 5f), 0), Quaternion.identity);
            Destroy(d, 30);
            bombDropTime = Time.time + repeatTime * Random.Range(1, 3);
        }
    }



}
