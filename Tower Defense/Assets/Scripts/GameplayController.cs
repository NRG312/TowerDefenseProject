using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplayController : MonoBehaviour
{
    //musze znalezc metode w hitenemy zeby nie recznie nie wrzucac towercontroller/musze tez przemyslec czy nie stworzyc drugiego interfejsu na iDamageable
    
    //wiezyczki i przeciwnicy beda mieli optymalizacje tick
    //stworzenie managera poprzez zenjecta
    //najpierw trzeba zrobic tworzenie wiezyczek/statey do nich ale podstawowe po zrobieniu przeciwnikow zrobie juz cale staety
    //przeciwnicy ktorzy beda na object poolu i tworzone beda na zenjectu
    [Inject] private ScoreController _scoreController;
    public float Money { get; private set; }

    public void StartGame()
    {
        //After start send info to score controller to turn on timer then scorecontroller is sendind info to spawner objectpooler to activate everything
    }

    public void EndGame()
    {
        
    }
    
    public void AddMoney(float money)
    {
        Money += money;
        _scoreController.ManageMoneyUI(Money);
    }

    public void RemoveMoney(float money)
    {
        Money -= money;
        _scoreController.ManageMoneyUI(Money);
    }
    
}
