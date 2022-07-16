using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Wave")]

public class EnemyWave : ScriptableObject
{
    public int id;
    public int waveNumber;
    public List<EnemyWaveEntry> waveEntries;
}
