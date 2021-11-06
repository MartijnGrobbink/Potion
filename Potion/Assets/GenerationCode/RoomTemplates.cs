using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    //setting up rooms and blockades for the spawner
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms;
    public GameObject startRoom;
    public GameObject blockade;
    //ths list where all the rooms are stored in
    public List<GameObject> rooms;
    //setting up a clock and the boss and a bool the see if the boss is spawned
    public float waitTime;
    private bool spawnedBoss;
    public GameObject boss;
    //setting up the requirements and an bool reset
    public float minRooms;
    public bool reset;
    //setting up test variable
    int lengthRoomSample;
    //setting up a bool to see if the generation is done
    public bool roomsGenerated;

    void Update(){
        Invoke("RoomChecker", 1f);
        //check if no resets are happening and if the rooms are generated
        if(reset == false && roomsGenerated == true){
            //check if the rooms are meeting the minimum required amount of rooms if not then start an reset
            if(minRooms >= rooms.Count){
                reset = true;
                }
            //if there is no boss and the wait time is 0 find the latest generated room that is not a hallway
            //we go through the list backwards so we start at the latest generated room and go back to find a room that is not an hallway
            if(waitTime <= 0 && spawnedBoss == false){
                for(int i = rooms.Count - 1; i > 0; i--){
                    //if the room is found spawn the boss and say that the boss is spawned
                    //also stop the loop as we have found the boss room and we do not need to find more rooms that are not hallways
                    if(!rooms[i].CompareTag("Hallway")){
                        Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        spawnedBoss = true;
                        break;
                }      
            }
        //remove time for the wait time if the waittime is not 0
            } else if(waitTime >= 0) { waitTime -= Time.deltaTime;}
        }
        //if there is an reset then delete the object and remove the object from the list
        else if(reset == true){
            for(int i = 0; 0 < rooms.Count;){
                Destroy(rooms[i].gameObject);
                rooms.Remove(rooms[i].gameObject);
            }
            //then say if the reset is done set the reset to false and generate to false
            reset = false;
            roomsGenerated = false;
            //after reset create new spawnpoint
            //that start new generation
            if(GameObject.FindGameObjectsWithTag("SpawnPoint").Length == 0){
                Instantiate(startRoom, transform.position, Quaternion.identity);
            }
        }
        //if there is some how removed items are in the list of room then remove those list items
        for(var i = rooms.Count - 1; i > -1; i--){
            if (rooms[i] == null)
            rooms.RemoveAt(i);
        }
    }
    //this will check if the dungeon is still being generated
    void RoomChecker(){
        //if the rooms are not already generated we want to check if there is still generation happening
        if(roomsGenerated == false){
        //get a room sample
        if(waitTime == 1){
            lengthRoomSample = rooms.Count;
        }
        //if the room sample after some time is the same as the current amount of rooms
        //that means that there are no new rooms and say that the dungeon is done with it's generation
        //reset the roomsample
        if(waitTime <= 0){
            if(lengthRoomSample == rooms.Count){
            roomsGenerated = true;
            lengthRoomSample = 0;
            }
            waitTime = 1;
            //remove time for the wait time if the waittime is not 0
            } else { waitTime -= Time.deltaTime;}
        }
    }
}