using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class StateWaiting : State
{
    [SerializeField] private StateAttack stateAttack;
    private TowerController _towerController;
    private GameObject _tower;
    //WatchingAnimation
    private bool _isTurning;
    private float _timerToTurn = 7f;
    private Vector3 rotDestination;
    public override State RunState()
    {
        RunTower();

        if (_towerController.target != null)
        {
            DOTween.Clear();
            return stateAttack;
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
        if (_isTurning)
        {
            _tower.transform.DOLocalRotate(new Vector3(0,rotDestination.y,0),5,RotateMode.Fast);

            //Timer to change new destination
            _timerToTurn -= Time.deltaTime;
            if (_timerToTurn <= 0)
            {
                _timerToTurn = 7f;
                _isTurning = false;
            }
        }
        else
        {
            float randomY = Random.Range(-50, 50);
            rotDestination = new Vector3(0, randomY, 0);
            _isTurning = true;
        }
    }
}
