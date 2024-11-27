using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HitEnemy : MonoBehaviour
{
    public TowerController towerController;
    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(towerController.towerData.damage);
        }
    }
}