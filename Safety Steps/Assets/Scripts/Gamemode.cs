using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gamemodes/Gamemode Template")]
public class Gamemode : ScriptableObject
{
    public string visibleName = "Example Gamemode";
    [TextArea] public string description = "Example Description";
    public GamemodeType type = GamemodeType.None;
    public float startSpawnSpeed = 1.5f;
    public float endSpawnSpeed = 1f;
    public float spawnSpeedupRate = 1f;
}

public enum GamemodeType 
{
    None,
    Highscore,
    Easy,
    Hardcore,
}