using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayController : MonoBehaviour
{
    [Inject] private WaveController _waveController;
    [Inject] private UIController _uiController;
    [Inject] private MoneyController _moneyController;
    [Inject] private SignalBus _signal;
    public float Money { get; private set; }
    [SerializeField] private WavesSO wavesSo;

    private void OnEnable()
    {
        _signal.Subscribe<OnWinGame>(x => EndGame(true));
        _signal.Subscribe<OnLoseGame>(x => EndGame(false));
    }

    private void OnDisable()
    {
        _signal.TryUnsubscribe<OnWinGame>(x => EndGame(true));
        _signal.TryUnsubscribe<OnLoseGame>(x => EndGame(false));
    }

    #region TestStartGame

    private void Start()
    {
        StartCoroutine(WaitForRefreshScripts());
    }

    public void StartGame()
    {
        _waveController.PrepareFirstWave(wavesSo);
        AddMoney(150);
    }

    private IEnumerator WaitForRefreshScripts()
    {
        yield return new WaitForSeconds(1);
        StartGame();//test
    }

    #endregion
    private void EndGame(bool w)
    {
        Time.timeScale = 0;
        _uiController.EnableEndGamePanel(w);
    }
    
    public void AddMoney(float money)
    {
        Money += money;
        _moneyController.ManageMoneyUI(Money);
    }

    public void RemoveMoney(float money)
    {
        Money -= money;
        _moneyController.ManageMoneyUI(Money);
    }
}
