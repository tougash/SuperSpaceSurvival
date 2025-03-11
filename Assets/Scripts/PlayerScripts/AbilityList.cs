using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetFoot:Ability
{
    public FleetFoot()
    {
        name = "Fleet Foot";
        description = "Plus 3 to speed stat.";
        isPassive = true;
        type = AbilityType.SPEED;
    }

    public void effect(PlayerStats player)
    {
        player.setSpeedMod(player.getSpeedMod() + 3);
    }
}

public class StrongBody:Ability
{
    public StrongBody()
    {
        name = "Strong Body";
        description = "Plus 3 to health stat.";
        isPassive = true;
        type = AbilityType.HEALTH;
    }

    public void effect(PlayerStats player)
    {
        player.setHealthMod(player.getHealthMod() + 3);
    }
}

public class AdvancedWeapons:Ability
{
    public AdvancedWeapons()
    {
        name = "Advanced Weaponry";
        description = "Plus 3 to damage stat.";
        isPassive = true;
        type = AbilityType.DAMAGE;
    }

    public void effect(PlayerStats player)
    {
        player.setDamageMod(player.getDamageMod() + 3);
    }
}
