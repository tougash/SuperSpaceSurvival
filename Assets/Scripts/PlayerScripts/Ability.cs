using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
        SPEED = 0,
        DAMAGE = 1,
        HEALTH = 2
}

public class Ability
{
        public string name;
        public string description;
        public bool isPassive;
        public AbilityType type;
        public virtual void effect(){}
        public virtual void effect(PlayerStats playerStats, PlayerController playerController){}
}
