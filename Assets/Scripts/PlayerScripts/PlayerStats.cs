using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    [SerializeField] private int health = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private int speed = 1;

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
