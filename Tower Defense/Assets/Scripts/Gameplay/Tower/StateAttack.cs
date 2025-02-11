using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StateAttack : State
{
    [SerializeField] private StateWaiting stateWaiting;
    private TowerController _towerController;
    private GameObject _tower;
    
    //Skonczylem na robieniu dzwiekow i animacji dla przeciwnikow, musze ogarnac jak ustawiac dzwiek musze zobaczyc slidery bo wszystkie audio sourcey moga miec volume na 0.5 i bede tylko operowal sliderem ttylko nie wiem jak zejsc na minus decybele
    //przjerzec wszystkie dzwieki i dodac do wszystkich pojazdach dodac muzyke w grze i w menu i to bedzie tyle
    public override State RunState()
    {
        RunTower();
        if (_towerController.target == null)
        {
            return stateWaiting;
        }
        else
        {
            return this;
        }
    }

    public override void TransitionData(TowerController towerController,GameObject tower)
    {
        _towerController = towerController;
        _tower = tower;
    }
    
    private void RunTower()
    {
        if (_towerController.target != null)
        {
            _tower.transform.DOLookAt(_towerController.target.transform.position,1);
            if (Physics.Raycast(_tower.transform.position,_tower.transform.forward,30))
            {
                var angle = Vector3.Angle((_towerController.target.transform.position - _tower.transform.position),
                    _tower.transform.forward);
                if (angle < 8f && _towerController.canShoot)
                {
                    _towerController.Shoot();
                }
            }
        }
        //if target is destroyed change to null
        if (_towerController.target != null)
        {
            if (!_towerController.target.gameObject.activeInHierarchy)
            {
                _towerController.target = null;
            }
        }
    }
}
