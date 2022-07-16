using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TestDamage 
{
    [MenuItem("Test/Damage/Take 1")]
    public static void takeDamage1() {
        Player.playerInstance.pa.takeDamage(1);
    }

    [MenuItem("Test/Damage/Take 10")]
    public static void takeDamage10() {
        Player.playerInstance.pa.takeDamage(10);
    }
}
