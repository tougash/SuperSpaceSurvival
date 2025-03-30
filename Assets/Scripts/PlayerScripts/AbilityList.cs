using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
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

    public override void effect(PlayerStats stats, PlayerController player)
    {
        stats.setSpeedMod(stats.getSpeedMod() + 3);
        player.updateCurrentSpeed();
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

    public override void effect(PlayerStats stats, PlayerController player)
    {
        stats.setHealthMod(stats.getHealthMod() + 3);
        player.health.updateMaxHealth();
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

    public override void effect(PlayerStats stats, PlayerController player)
    {
        stats.setDamageMod(stats.getDamageMod() + 3);
    }
}

public class Ghost:Ability
{
    public Ghost()
    {
        name = "Ghost";
        description = "For 10 seconds, gain the ability to walk through objects and enemies";
        isPassive = false;
        type = AbilityType.SPEED;
    }


    public override void effect(PlayerStats stats, PlayerController player)
    {
        player.Intagible();
    }

}

public class GrenadeLob:Ability
{
    public GrenadeLob()
    {
        name = "Grenade";
        description = "Throw a grenade that will detonate after 3 seconds, dealing a large amount of damage.";
        isPassive = false;
        type = AbilityType.DAMAGE;
    }

    public override void effect(PlayerStats stats, PlayerController player)
    {
        player.ThrowGrenade();
    }

}