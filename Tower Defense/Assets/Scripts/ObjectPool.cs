using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int size;
    }
    
    [Inject] private EnemyController.Factory[] _enemy;
    public List<Pool> listEnemy;
    [HideInInspector] public List<GameObject> listObjects;
    [SerializeField] private GameObject container;
    private int _enemyCount;
    
    public void RespawnObjects()
    {
        foreach (var v in listEnemy)
        {
            for (int i = 0; i < v.size; i++)
            {
                EnemyController enemy = _enemy[_enemyCount].Create();
                enemy.transform.SetParent(container.transform);
                enemy.gameObject.SetActive(false);
                listObjects.Add(enemy.gameObject);
            }
            _enemyCount++;
        }
    }

    public void SpawnEnemyFromPool(GameObject prefab, Vector3 pos,Quaternion rot)
    {
        for (int i = 0; i < listObjects.Count; i++)
        {
            if (prefab.name+"(Clone)" == listObjects[i].name && !listObjects[i].activeInHierarchy)
            {
                GameObject objectToSpawn = listObjects[i];
                objectToSpawn.SetActive(true);
                objectToSpawn.transform.rotation = rot;
                objectToSpawn.transform.position = pos;
                //Reset functions in enemy
                objectToSpawn.GetComponent<EnemyController>().ResetEnemy();
                //Send Enemy to list objectsTickController
                ObjectsTickController.onCreateEnemy.Invoke(objectToSpawn.GetComponent<EnemyController>());
                
                return;
            }
        }
    }
}
