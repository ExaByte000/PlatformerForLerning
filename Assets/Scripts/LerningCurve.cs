using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerningCurve : MonoBehaviour
{
    void Start()
    {
        Weapon huntingBow = new("Hunting Bow", 105);
        Weapon warBow = huntingBow;
        warBow.Name = "War Bow";

        huntingBow.PrintWeaponStats();
        warBow.PrintWeaponStats();
    }

}
