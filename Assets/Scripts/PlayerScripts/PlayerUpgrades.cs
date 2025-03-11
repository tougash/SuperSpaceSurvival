using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{

    public int playerCurrentLevel = 1;
    public int nextLevel = 10;
    public int currentExp = 0;

    void onLevelUp()
    {
        playerCurrentLevel++;
        currentExp = currentExp-nextLevel;
        nextLevel*=2;
        Time.timeScale = 0f;
        // Select Ability
    }

    void checkLevelUp()
    {
        if(currentExp >= nextLevel)
        {
            onLevelUp();
        }
    }
}
