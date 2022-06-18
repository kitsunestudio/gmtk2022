using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPerson", menuName = "Person")]
public class CreditEntry : ScriptableObject
{
    public string personNameFull;
    public CreditCategory category;
}
