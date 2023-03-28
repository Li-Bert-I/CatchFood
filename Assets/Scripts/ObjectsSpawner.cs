using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    /*
    !! Changes gravity for game !!
    Spawns food and not food with some rate.
    */
    public GameObject food;
    public GameObject notFood;
    private float spawnRate = 0.4f;
    private float lastSpawnTime = 0.0f;
    public Vector3 spawnZoneStart;
    public Vector3 spawnZoneEnd;

    void Start()
    {
        Physics.gravity = new Vector3(0, -1.5f, 0);
    }

    void Update()
    {
        if (Time.time > lastSpawnTime + spawnRate && Random.value > 0.5)
        {
            Debug.Log(lastSpawnTime.ToString() + " " + (lastSpawnTime + spawnRate).ToString() + " " + Time.time.ToString());
            lastSpawnTime = Time.time;
            Vector3 position = spawnZoneStart + (spawnZoneEnd - spawnZoneStart) * Random.value;
            if (Random.value > 0.5f)
            {
                Instantiate(food, position, Random.rotation);
            }
            else
            {
                Instantiate(notFood, position, Random.rotation);
            }

        }
    }

    void Spawn()
    {

    }
}
