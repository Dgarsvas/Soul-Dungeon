using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelManagerScriptableObject", menuName = "Level Manager/Create Level SO", order = 101)]

public class LevelManagerScriptableObject : ScriptableObject
{
    public List<string> levels;
}