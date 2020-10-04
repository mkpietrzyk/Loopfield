using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject[] blocksPrefabs;
    [SerializeField] private GameObject choosenBlock;
    public GameManager gameManager;
    public float spawnTimer;
    void Start()
    {
        spawnTimer = gameManager.timer;
        StartCoroutine(nameof(SpawnObject));
    }
    
    void Update()
    {
        spawnTimer = gameManager.timer;
    }

    IEnumerator SpawnObject()
    {
        while (spawnTimer > 0)
        {
            var randomSpawner = spawners[Random.Range(0, spawners.Length)];
            var randomBlockIndex = Random.Range(0, 20);
            var randomSpawnSpeed = Random.Range(0f,1f);
            var randomWaitTime = Random.Range(0f, 1.5f);
            if (randomBlockIndex == 5)
            {
                choosenBlock = blocksPrefabs[1];
            } else if (randomBlockIndex == 12)
            {
                choosenBlock = blocksPrefabs[2];
            }
            else
            {
                choosenBlock = blocksPrefabs[0];
            }

            Vector2 spawnDirection = player.transform.position - randomSpawner.transform.position;

            if (gameManager.gameStarted && !gameManager.gamePaused && !gameManager.gameEnd)
            {
                GameObject block = Instantiate(choosenBlock, randomSpawner.transform.position,
                    Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

                Rigidbody2D blockRigidBody = block.GetComponent<Rigidbody2D>();
            
                blockRigidBody.AddForce(spawnDirection * randomSpawnSpeed, ForceMode2D.Impulse);   
            }

            yield return new WaitForSeconds(randomWaitTime);
        }
    }
}
