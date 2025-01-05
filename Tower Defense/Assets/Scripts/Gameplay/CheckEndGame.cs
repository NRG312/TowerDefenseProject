using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CheckEndGame : MonoBehaviour
{
    [Inject] private SignalBus _signal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _signal.Fire<OnLoseGame>();
        }
    }
}
