using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gamemodes/Gamemode Template")]
public class Gamemode : ScriptableObject
{
    public string visibleName = "Example Gamemode";
    [TextArea] public string description = "Example Description";
    public GamemodeType type = GamemodeType.None;
    public float spawnSpeed = 1.5f;
}

public enum GamemodeType 
{
    None,
    Highscore,
    Easy,
    Hardcore,
}