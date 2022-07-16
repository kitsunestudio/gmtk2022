using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/All Waves")]
public class AllEnemyWaves : ScriptableObject
{
    public List<EnemyWave> waves;
}
