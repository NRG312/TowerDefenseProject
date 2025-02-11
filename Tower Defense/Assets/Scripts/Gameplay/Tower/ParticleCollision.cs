using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleCollision : MonoBehaviour
{
    public TowerController towerController;
    [SerializeField] private GameObject sfxCollision;
    private List<ParticleCollisionEvent> _particleCollisionEvent;
    private ParticleSystem _particle;

    private void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _particleCollisionEvent = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(towerController.towerData.damage);
            int num = _particle.GetCollisionEvents(other, _particleCollisionEvent);
            foreach (var v in _particleCollisionEvent)
            {
                Vector3 pos = v.intersection;
                Instantiate(sfxCollision,pos,Quaternion.identity);
            }
        }
    }
}