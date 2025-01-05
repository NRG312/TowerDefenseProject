using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class WaveController : MonoBehaviour
{
    [Inject] private EnemySpawner _enemySpawner;
    [Inject] private ObjectsTickController _objects;
    [Inject] private ObjectPool _objectPool;
    [Inject] private SignalBus _signal;
    [Header("TimerWaves")]
    [SerializeField] private TMP_Text timerGetReady;
    [SerializeField] private TMP_Text numberWave;
    
    //Awaiting timer
    private float _awaitTimer = 5f;
    private bool _startPreparing = true;
    //Waves variable
    private float _timer = 5f;
    private bool _newWave = false;
    private WavesSO _wave;
    private int _indexWave = 0;
    //Checking end of wave
    private bool _checkingAmountEnemies;

    private void StartNewWave()
    {
        _enemySpawner.EnableSpawner(_wave,_indexWave);
        _indexWave++;
        _startPreparing = true;//
        _newWave = false;//
        _checkingAmountEnemies = true;
    }
    //
    public void PrepareFirstWave(WavesSO wavesSo)
    {
        //Prepare objects in objectPool
        _objectPool.RespawnObjects();
        //
        _newWave = true;
        _wave = wavesSo;
        //
        var waveNumber = _indexWave + 1;
        numberWave.text = "Wave: " + waveNumber;
    }
    //
    private void PreparedNextWaves()
    {
        //Check is next Wave available
        if (CheckNextAvailableWave(_wave,_indexWave))
        {
            _signal.Fire<OnWinGame>();
        }
        
        //
        _newWave = true;
        //
        var waveNumber = _indexWave + 1;
        numberWave.text = "Wave: " + waveNumber;
    }
    //
    private bool CheckNextAvailableWave(WavesSO wave,int indexWave)
    {
        if (indexWave >= wave.wave1.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //
    private void Update()
    {
        if (_newWave)
        {
            //awaiting for timer
            if (_startPreparing) PreparingTimers();
            if (_startPreparing) return;
            //timer wave
            timerGetReady.gameObject.SetActive(true);
            _timer -= Time.deltaTime;
            timerGetReady.text = "Prepare for the wave! : " +_timer.ToString("F0");
            if (_timer <= 0)
            {
                timerGetReady.gameObject.SetActive(false);
                _timer = 5f;
                StartNewWave();
                _newWave = false;
            }
        }
        //Checking amount enemies to end the wave
        if (_checkingAmountEnemies)
        {
            if (_objects.activeEnemiesOnScene().Count == 0)
            {
                PreparedNextWaves();
                _checkingAmountEnemies = false;
            }
        }
    }

    private void PreparingTimers()
    {
        _awaitTimer -= Time.deltaTime;
        if (_awaitTimer <= 0)
        {
            _awaitTimer = 5f;
            _startPreparing = false;
        }
    }
}