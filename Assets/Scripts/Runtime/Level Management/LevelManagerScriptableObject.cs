using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Level
{
    public int level;
    public string name;

    public Level(int level, string name)
    {
        this.level = level;
        this.name = name;
    }
}

[CreateAssetMenu(fileName = "LevelManagerScriptableObject", menuName = "Level Manager/Create Level SO", order = 101)]

public class LevelManagerScriptableObject : ScriptableObject
{
    public List<Level> levels;
}
