using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Waves",menuName = "ScriptableObject/Waves")]
public class WavesSO : ScriptableObject
{
    public Wave[] Waves;
}

[Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
}