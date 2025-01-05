using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerController : MonoBehaviour
{
    public TowerDataSO towerData;
    
    //Detecting Enemy
    [HideInInspector]public GameObject target;
    [HideInInspector]public GameObject tower;
    //timer
    private float _timeToShoot;
    [HideInInspector]public bool canShoot;
    private void Start()
    {
        //assign variables
        _timeToShoot = towerData.timeToShoot;
        //Finding tower in children objects
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "Tower")
            {
                tower = transform.GetChild(i).gameObject;
            }
        }
        //turn off blocking collider on mouse clicking
        Physics.queriesHitTriggers = false;
    }

    public void Tick()
    {
        if (canShoot == false)
        {
            _timeToShoot -= Time.deltaTime;
            if (_timeToShoot <= 0)
            {
                _timeToShoot = towerData.timeToShoot;
                canShoot = true;
            }
        }
    }

    //Detecting Enemy
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && target == null)
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && target == null)
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (target != null)
        {
            target = null;
        }
    }

    public void Shoot()
    {
        if (GetComponentInChildren<ParticleCollision>() != null)
        {
            ParticleSystem particle = GetComponentInChildren<ParticleSystem>();
            particle.Play();
            canShoot = false;
        }else if (GetComponentInChildren<BulletCreator>() != null)
        {
            BulletCreator bullet = GetComponentInChildren<BulletCreator>();
            bullet.ShootFunc();
            canShoot = false;
        }
    }
    public class Factory : PlaceholderFactory<TowerController>
    {
        
    }
}
