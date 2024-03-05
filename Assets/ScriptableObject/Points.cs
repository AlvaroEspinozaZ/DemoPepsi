
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Points", menuName = "ScriptableObject/ValuesGlobal/int", order = 1)]
public class Points : ScriptableObject
{
    [SerializeField] public int ScoreData;
}
