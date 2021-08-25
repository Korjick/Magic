using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : Singleton<GameManager>
{
    public List<EnemyData> enemyData;
    public List<GameObject> spawnPoints;
    public Enemy enemy;
    public GameObject player;

    private int _level;
    private Random _random;
    
    private void Awake()
    {
        _level = 1;
        _random = new Random();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (enemyData.Count > 0)
            {
                var weight = 0;
                while (weight < _level)
                {
                    var tmpData = enemyData[_random.Next(enemyData.Count)];
                    if (weight + tmpData.weight <= _level)
                    {
                        var tmpEnemy = Instantiate(enemy.gameObject, 
                            spawnPoints[_random.Next(spawnPoints.Count)].transform.position, Quaternion.identity);
                        var tmpEnemyScript = tmpEnemy.GetComponent<Enemy>();
                        tmpEnemyScript.data = tmpData;
                        tmpEnemyScript.Player = player;
                        weight += tmpData.weight;
                    }
                }
            }

            yield return new WaitForSeconds(5f);
        }
    }
}
