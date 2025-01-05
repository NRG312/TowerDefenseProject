using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Inject] private ObjectPool _objectPool;
    private GameObject _spawnPos;
    //To quit task operation
    private bool _quiteTasks;
    
    
    //WaveController is enabling spawner
    public void EnableSpawner(WavesSO wave,int indexWave)
    {
        _spawnPos = GameObject.FindGameObjectWithTag("Respawn");
        SpawnWaveEnemy(wave,indexWave);
    }
    private async Task SpawnWaveEnemy(WavesSO wave, int index)
    {
        if (_quiteTasks == false)
        {
            Wave actualWave = wave.wave1[index];
            for (int i = 0; i < actualWave.enemyPrefabs.Length; i++)
            {
                _objectPool.SpawnEnemyFromPool(actualWave.enemyPrefabs[i],_spawnPos.transform.position,_spawnPos.transform.rotation);
                await Task.Delay(3000);
            }
        }
    }
    
    private void OnApplicationQuit()
    {
        _quiteTasks = true;
    }

}
