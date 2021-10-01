using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBlockade : MonoBehaviour
{
    public GameObject Walls;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawner>().spawned == true){
            Destroy(gameObject);
        } else{
            Walls.SetActive(true);
        }
    }
}