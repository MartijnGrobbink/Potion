using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionAOE : MonoBehaviour
{
    public GameObject potion;

    public float invTime;
    float timer;
    int damage;

    void Start()
    {
        timer = invTime;
    }
    void Update()
    {
        if(timer >= 0) timer -= Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy") 
        {   
            if(timer <= 0) DealDamage();
            other.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
        }
    }

    private void DealDamage()
    {
        damage = potion.gameObject.GetComponent<Potion>().damage;
        Invoke(nameof(DamageReset),0.1f);
    }

    private void DamageReset()
    {
        damage = 0; 
        timer = invTime;
    }
}
