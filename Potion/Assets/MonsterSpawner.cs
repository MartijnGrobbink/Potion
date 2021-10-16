using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
private RoomData data;
void Start(){
    data = GetComponent<RoomData>();
    data.monsters.Add(this.gameObject);  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
