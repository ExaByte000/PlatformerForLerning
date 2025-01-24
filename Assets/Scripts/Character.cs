using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public string name;
    public int exp = 0;

    public Character()
    {
        name = "Not assigned";
    }

    public Character(string name)
    {
        this.name = name;
    }

    public virtual void PrintStatsInfo()
    {
        Debug.LogFormat("Hero: {0} - {1} EXP", name, exp);
    }
}

public struct Weapon
{
   public string Name;
   public int Damage;

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }

    public readonly void PrintWeaponStats() 
    {
        Debug.LogFormat("Weapon: {0} - {1} DMG", Name, Damage);
    }
}
