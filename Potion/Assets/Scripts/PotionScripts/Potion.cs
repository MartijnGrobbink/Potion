using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    Rigidbody potionRigid;

    public GameObject PotionSprite;
    public GameObject AreaOfEffect;

    public float damage;
    public float duration;

    bool broken;
    // Start is called before the first frame update
    void Start()
    {
        potionRigid = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(broken == true)
        {
            PotionSprite.SetActive(false);
            AreaOfEffect.SetActive(true);

            potionRigid.constraints = RigidbodyConstraints.FreezePosition;
            potionRigid.constraints = RigidbodyConstraints.FreezeRotation;
            duration -= Time.deltaTime;
        }
        if(duration <= 0)
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "AOE")
        {
            broken = true;
        }  
    }
    //void OnTriggerStay(Collision other)
    //{
        //if(other.gameObject.tag)
    //}
}
