using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class EnemyController : MonoBehaviour,IScoreBehaviour,IDamageable
{
    private HealthSystem _healthSystem;
    [Inject] private GameplayController _gameplayController;
    public float gainMoney;
    private void Start()
    {
        _healthSystem = GetComponentInChildren<HealthSystem>();
    }

    public void Score(float amount)
    {
        _gameplayController.AddMoney(amount);
    }

    public void TakeDamage(float dmg)
    {
        _healthSystem.TakeDamage(dmg);
    }
    //tutaj bedzie stworzenie zniszczony prefab
    public void EnemyDead()
    {
        gameObject.SetActive(false);
    }
}