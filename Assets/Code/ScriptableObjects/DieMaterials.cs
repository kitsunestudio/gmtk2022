using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Die", menuName = "Die Material")]

public class DieMaterials : ScriptableObject
{
    public List<Material> materials;
}
