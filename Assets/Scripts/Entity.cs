using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int lives;
    public void GetDamage()
    {
        Debug.Log("Нанесен урон!");
        lives--;
        if (lives < 0) Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
