using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgrades : MonoBehaviour
{

    public int playerCurrentLevel = 1;
    public int nextLevel = 10;
    public int currentExp = 0;

    public Image currentExpBar;
    public static bool selectingUpgrade = false;
    void Update()
    {
         if (!selectingUpgrade) 
        {
            checkLevelUp();
        }
    }

    void onLevelUp()
    {   
        playerCurrentLevel++;
        currentExp = currentExp-nextLevel;
        nextLevel*=2;
        // Select Ability
        selectingUpgrade = true;
        currentExpBar.fillAmount = 1f;
        UpgradeManager.instance.setMenu();
    }

    void checkLevelUp()
    {   
        if(currentExpBar != null)
        {
            currentExpBar.fillAmount = (float)currentExp / (float)nextLevel;
        }
        if(currentExp >= nextLevel)
        {
            onLevelUp();
        }
    }

     // Call this when the upgrade menu is closed
    public static void ResumeGame()
    {
        selectingUpgrade = false;
    }
}
