using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Wave Entry")]
public class EnemyWaveEntry : ScriptableObject
{
    public Enemy myEnemy;
    public int amount;
}
