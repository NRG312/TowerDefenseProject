using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Waves",menuName = "ScriptableObject/Waves")]
public class WavesSO : ScriptableObject
{
    public Wave[] wave1;
}

[Serializable]
public class Wave
{
    public GameObject[] enemyPrefabs;
}