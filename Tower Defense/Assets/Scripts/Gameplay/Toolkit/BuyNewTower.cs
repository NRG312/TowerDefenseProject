using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BuyNewTower : MonoBehaviour
{
    [Inject] private GameplayController _gameplayController;
    [Inject] private TowerController.Factory[] _towerController;
    private GameObject _target;
    private BaseController _baseController;
    //
    private void RefreshData()
    {
        //przy wlaczaniu panelu tutaj bedzie odswiezal dane do paneli czy kupowania czy upgradeu
    }
    //
    public void NewTarget(GameObject target)
    {
        _target = target;
        _baseController = target.GetComponent<BaseController>();
        RefreshData();
    }

    public void BuildNewTower(TowerDataSO tower)
    {
        //connect with gameplaycontroller to check the amount of money
        if (/*_gameplayController.Money >= tower.price*/ _baseController.CheckEmptySlot())
        {
            TowerController newTower = _towerController[tower.index].Create();
            //Position
            newTower.transform.SetParent(_target.transform);
            newTower.gameObject.transform.position = _target.transform.position;
            //Rotation
            newTower.transform.rotation = _target.transform.rotation;
            //
            _baseController.UseSlot();
        }
    }
}
