using System;
using System.Collections.Generic;
using System.Linq;
using Project.Dev.Scripts.Setting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyGenerator : Singleton<EnemyGenerator>
{
    private readonly List<Enemy> EnemyList = new List<Enemy>();
    private readonly List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();

    private int _counter = 0;
    
    private EnemyGeneratorSetting.EnemyConfig[] _enemyConfigs = null;
    
    protected override void SingleAwake()
    {
        var spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();

        foreach (var spawnPoint in spawnPoints)
        {
            SpawnPoints.Add(spawnPoint);
        }
        
    }
    
    private void OnDisable()
    {
        foreach (var enemy in EnemyList)
        {
            enemy.Died -= Enemy_Died;
        }
    }

    private void Start()
    {
        _enemyConfigs = SceneContexts.Instance.EnemyGeneratorSetting.GetEnemyConfig();
        
        InstantiateEnemy();
        
        foreach (var enemy in EnemyList)
        {
            enemy.Died += Enemy_Died;
        }
    }

    private void InstantiateEnemy()
    {
        for (int i = 0; i < _enemyConfigs.Length; i++)
        {
            var resourcePrefab = Instantiate(_enemyConfigs[i].Enemy, transform);
            var dropResource = resourcePrefab.GetComponent<DropResource>();
            
            dropResource.SetDrop(_enemyConfigs[i].DropResourceConfigs);
            
            resourcePrefab.SetDrop(dropResource);
            resourcePrefab.transform.position = GetRandomPosition();
            resourcePrefab.gameObject.SetActive(true);
            
            EnemyList.Add(resourcePrefab);
        }
    }

    private Vector2 GetRandomPosition()
    {
        var randomIndex = Random.Range(0, SpawnPoints.Count);
        var randomPoint = SpawnPoints[randomIndex];

        SpawnPoints.RemoveAt(randomIndex);

        return randomPoint.transform.position;
    }
    
    private void Enemy_Died(Enemy enemy)
    {
        _counter++;

        if (EnemyList.Count == _counter)
        {
            SaveManager.Instance.Save();
            WindowSwitcher.Instance.Show<WinWindow>();
        }
    }
}
