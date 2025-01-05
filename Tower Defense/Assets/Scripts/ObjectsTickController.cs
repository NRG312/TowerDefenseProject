using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectsTickController : MonoBehaviour
{
    [SerializeField] private List<EnemyController> enemyControllers;
    [SerializeField] private List<TowerController> towerControllers;

    public List<EnemyController> activeEnemiesOnScene()
    {
        return enemyControllers;
    }
    
    
    public static UnityAction<EnemyController> onCreateEnemy;
    public static UnityAction<TowerController> onCreateTower;
    public static UnityAction<EnemyController> onDeathEnemy;
    public static UnityAction<TowerController> onChangeTower;

    private void OnEnable()
    {
        onCreateTower += AddTowerToList;
        onCreateEnemy += AddEnemyToList;
        onDeathEnemy += DeleteEnemyFromList;
        onChangeTower += DeleteTowerFromList;
    }

    private void OnDisable()
    {
        onCreateTower -= AddTowerToList;
        onCreateEnemy -= AddEnemyToList;
        onDeathEnemy -= DeleteEnemyFromList;
        onChangeTower -= DeleteTowerFromList;
    }

    private void Update()
    {
        if (enemyControllers.Count > 0)
        {
            for (int i = 0; i < enemyControllers.Count; i++)
            {
                enemyControllers[i].Tick();
            }
        }

        if (towerControllers.Count > 0)
        {
            for (int i = 0; i < towerControllers.Count; i++)
            {
                towerControllers[i].Tick();
            }
        }
    }

    private void AddEnemyToList(EnemyController enemy)
    {
        enemyControllers.Add(enemy);
    }

    private void AddTowerToList(TowerController tower)
    {
        towerControllers.Add(tower);
    }

    private void DeleteEnemyFromList(EnemyController enemy)
    {
        enemyControllers.Remove(enemy);
    }

    private void DeleteTowerFromList(TowerController tower)
    {
        towerControllers.Remove(tower);
    }
}