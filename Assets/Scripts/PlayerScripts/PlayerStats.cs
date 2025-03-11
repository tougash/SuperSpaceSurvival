using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    private int health,speed,damage = 1;

    public int getHealthMod()
    {
        return health;
    }

    public int getDamageMod()
    {
        return damage;
    }

    public int getSpeedMod()
    {
        return speed;
    }

    public void setHealthMod(int newVal)
    {
        health = newVal;
    }

    public void setDamageMod(int newVal)
    {
        damage = newVal;
    }

    public void setSpeedMod(int newVal)
    {
        speed = newVal;
    }

}
