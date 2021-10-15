using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject[] startRoom;
    
    public GameObject blockade;
    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;
    public float minRooms;
    public bool reset;

    int lengthRoomSample;
    int lengthRoomSampleDelay;
    [SerializeField]
    bool roomsGenerated;

    void Update(){
        Invoke("RoomChecker", 1f);
        if(reset == false && roomsGenerated == true){
            if(minRooms >= rooms.Count){
                reset = true;
                }
            if(waitTime <= 0 && spawnedBoss == false){
            Instantiate(boss, rooms[rooms.Count-1].transform.position, Quaternion.identity);
            spawnedBoss = true;
            } else if(waitTime >= 0) { waitTime -= Time.deltaTime;}
            } 
        else if(reset == true){
            for(int i = 0; 0 < rooms.Count;){
                Destroy(rooms[i].gameObject);
                rooms.Remove(rooms[i].gameObject);
            } 
            reset = false;
            roomsGenerated = false;
            }
        for(var i = rooms.Count - 1; i > -1; i--){
            if (rooms[i] == null)
            rooms.RemoveAt(i);
        }
    }

    void RoomChecker(){
        if(roomsGenerated == false){
        if(waitTime >= 1.5 && waitTime <= 3){
            lengthRoomSample = rooms.Count; 
            }
        if(waitTime <= 0){
            lengthRoomSampleDelay = lengthRoomSample;
            waitTime = 3;
            } else { waitTime -= Time.deltaTime;}
        if(lengthRoomSampleDelay == rooms.Count){
            roomsGenerated = true;
            lengthRoomSampleDelay = 0;
            }
        }
    }
}

