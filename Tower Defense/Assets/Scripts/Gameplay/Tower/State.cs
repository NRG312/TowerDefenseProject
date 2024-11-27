using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State: MonoBehaviour
{
    public abstract State RunState();
    public abstract void TransitionData(TowerController towerController,GameObject tower);
}
