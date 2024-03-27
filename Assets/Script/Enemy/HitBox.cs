using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public EnemyHealth health;


    public void OnRaycastHit(GunSystem gunSystem,Vector3 direction)
    {
        health.TakeDamage(gunSystem.damage, direction);
    }
}
