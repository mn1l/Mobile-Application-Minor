using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int coinCount;

    // Values defined in this constructor will be default values
    // The game starts with when there's no data to load
    public GameData()
    {
        this.coinCount = 0;
    }
}
