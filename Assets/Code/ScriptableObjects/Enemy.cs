using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy/Enemy")]

public class Enemy : ScriptableObject
{
    public int id;
    public string enemyName;
    public int maxHealth;
    public Sprite gameSprite;
    public float speed;
    public float attackDistance;
}
