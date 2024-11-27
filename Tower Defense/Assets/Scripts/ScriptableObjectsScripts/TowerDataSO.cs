using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower",menuName = "ScriptableObject/NewTower")]
public class TowerDataSO : ScriptableObject
{
    public GameObject towerPrefab;
    public TowerDataSO towerUpgraded;
    public float timeToShoot;
    public float damage;
    public float price;
    public int index;
}
