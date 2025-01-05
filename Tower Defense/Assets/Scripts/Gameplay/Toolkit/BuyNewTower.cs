using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class BuyNewTower : MonoBehaviour
{
    [Inject] private GameplayController _gameplayController;
    [Inject] private TowerController.Factory[] _towerController;
    private GameObject _target;
    private BaseController _baseController;

    [SerializeField] private TMP_Text machineGunPriceTxt;
    [SerializeField] private TMP_Text rocketTowerPriceTxt;
    [SerializeField] private TMP_Text laserTowerPriceTxt;
    [Space(20f)] 
    [SerializeField] private TowerDataSO machineGun;
    [SerializeField] private TowerDataSO rocketTower;
    [SerializeField] private TowerDataSO laserTower;

    [Space(20f)] 
    [SerializeField] private GameObject backGroundTools;
    //
    private void Update()
    {
        if (backGroundTools.activeInHierarchy)
        {
            if (machineGun.price > _gameplayController.Money)
            {
                machineGunPriceTxt.text = "<color=red>" + "$" + machineGun.price + "</color>";
            }
            else if(machineGun.price <= _gameplayController.Money)
            {
                machineGunPriceTxt.text = "<color=green>" + "$" + machineGun.price + "</color>";
            }
            //
            if (rocketTower.price > _gameplayController.Money)
            {
                rocketTowerPriceTxt.text = "<color=red>" + "$" + rocketTower.price + "</color>";
            }
            else if(rocketTower.price <= _gameplayController.Money)
            {
                rocketTowerPriceTxt.text = "<color=green>" + "$" + rocketTower.price + "</color>";
            }
            //
            if (laserTower.price > _gameplayController.Money)
            {
                laserTowerPriceTxt.text = "<color=red>" + "$" + laserTower.price + "</color>";
            }
            else if(laserTower.price <= _gameplayController.Money)
            {
                laserTowerPriceTxt.text = "<color=green>" + "$" + laserTower.price + "</color>";
            }
        }
    }
    //
    public void NewTarget(GameObject target)
    {
        _target = target;
        _baseController = target.GetComponent<BaseController>();
    }

    public void BuildNewTower(TowerDataSO tower)
    {
        //connect with gameplaycontroller to check the amount of money
        if (_gameplayController.Money >= tower.price && _baseController.CheckEmptySlot())
        {
            TowerController newTower = _towerController[tower.index].Create();
            //Position
            newTower.transform.SetParent(_target.transform);
            newTower.gameObject.transform.position = _target.transform.position;
            //Rotation
            newTower.transform.rotation = _target.transform.rotation;
            //
            _baseController.UseSlot();
            
            //Invoke Event in objectsTickController
            ObjectsTickController.onCreateTower.Invoke(newTower);
            //
            _gameplayController.RemoveMoney(tower.price);
            //
            backGroundTools.SetActive(false);
        }
    }
}
