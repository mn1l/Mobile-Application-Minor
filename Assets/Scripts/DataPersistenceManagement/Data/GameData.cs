using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coinCount;
    public Dictionary<string, bool> levelsUnlocked;

    public GameData()
    {
        this.coinCount = 0;
        levelsUnlocked = new Dictionary<string, bool>();
    }
}
