using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] private State currentState;
    private TowerController _towerController;
    
    private void Start()
    {
        _towerController = GetComponent<TowerController>();
        //
        TransitionData();
    }

    private void Update()
    {
        RunCurrentState();
    }

    private void RunCurrentState()
    {
        State nextState = currentState?.RunState();
        
        if (nextState != null)
        {
            SwitchToNextState(nextState);    
        }
    }

    private void SwitchToNextState(State newState)
    {
        currentState = newState;
        TransitionData();
    }

    private void TransitionData()
    {
        currentState.TransitionData(_towerController,_towerController.tower);
    }
}
