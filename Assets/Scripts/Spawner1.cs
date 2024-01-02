using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject checkpointPrefab;

    public GameObject previousPlatform;
    public GameObject mario;
    [Range(0f, 25f)]
    public float xRange = 8;
    [Range(0f, 25f)]
    public float yRange = 5;

    // Counts how many platformed spawned until it reaches 5
    public int spawnInterval = 5;
    private int platformCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        float randomFloat = Random.Range(-1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (previousPlatform.transform.position.x - mario.transform.position.x < 10)
        {
            Vector3 newPosition = previousPlatform.transform.position + new Vector3(20 + Random.Range(-xRange, xRange), Random.Range(-yRange, yRange), 0);

            previousPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);

            // Counts how many platforms have spawned
            platformCounter++;

            // Check if it's time to spawn the checkpoint and sets counter to 0
            if (platformCounter % spawnInterval == 0)
            {
                SpawnCheckpoint();
            }
        }
    }

    void SpawnCheckpoint()
    {
        // Spawns Checkpoint
        Instantiate(checkpointPrefab, new Vector3(previousPlatform.transform.position.x, previousPlatform.transform.position.y + 2, 0), Quaternion.identity);
    }
}