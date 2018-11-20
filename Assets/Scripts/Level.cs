using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Recipe/Level", order = 1)]
public class Level : ScriptableObject
{
    public List<GameObject> LevelOrdersList;
}
