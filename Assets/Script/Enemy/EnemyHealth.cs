using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxhealth;

    private float currenthealth;
    Ragdoll Ragdoll;
    public float delay = 4f;



    // Start is called before the first frame update
    void Start()
    {
        Ragdoll = GetComponent<Ragdoll>();
        currenthealth = maxhealth;

    

    }

   
    public void  TakeDamage(int amount, Vector3 direction)
    {
        currenthealth -=amount;
        if (currenthealth <= 0 ) 
        {
            Die();
            Invoke("DestroyObject", delay);
        }
    }

    public void Die()
    {
        Ragdoll.ActivateRagdoll(); 

    }

    void DestroyObject()
    {
        Destroy(gameObject); // ทำลาย GameObject ปัจจุบัน
    }

}
