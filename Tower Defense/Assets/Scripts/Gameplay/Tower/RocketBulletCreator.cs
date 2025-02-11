using System;
using UnityEngine;

public class RocketBulletCreator : MonoBehaviour
{
    public TowerController towerController;
    [SerializeField] private GameObject prefabBullet;
    private GameObject _bullet;

    public void ShootFunc()
    {
        _bullet = Instantiate(prefabBullet, transform.position, transform.rotation);
        _bullet.GetComponent<BulletPrefab>().ReplaceData(towerController);
    }
}