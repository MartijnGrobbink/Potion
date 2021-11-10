using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    public int destroy;
    //1 need bottom door
    //2 need top door
    //3 need left door
    //4 need right door
    //5 start


    private RoomTemplates templates;
    private int rand;
    public bool spawned = false;

    void Start(){
        //find room templates the place where you can find all the room presets
        //start the spawn with and 0.3 seconds of delay so the collisions can check what they collide with
        //if other is not found increase the delay
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.4f);
    }
    void Spawn(){
        if(spawned == false){
            if(openingDirection == 1){
                // Need to spawn a room with BOTTOM door
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
            } else if(openingDirection == 2){
                // Need to spawn a room with TOP door
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
            } else if(openingDirection == 3){
                // Need to spawn a room with LEFT door 
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);           
            } else if(openingDirection == 4){
                // Need to spawn a room with RIGHT door   
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
            }     
            //set spawned true so the code doesn't spawn more rooms
            spawned = true; 
        }
    }
    void Update(){
        //if the dungeon is generated and there are no reset going on delete this object
        //we do this as a clean up to prevent lagg as otherwise we have collision boxes and scripts running while it's not needed
        //the only object we are going to keep is the spawn for later generation
        if(templates.roomsGenerated == true && templates.reset != true){
            Destroy(gameObject);
        }
    }
    //this is to check if there is already a existing room on the place you want to spawn a room
    //if this is true delete this spawn object so there are no rooms spawned in already existing rooms
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("SpawnPoint") && spawned == false){
            if(other.GetComponent<RoomSpawner>().spawned == true){
            Destroy(gameObject);}
            //this checks if there are 2 spawnpoint that want to spawn a room in the same spot delete both spawnpoints and blockade that spot
            if(other.GetComponent<RoomSpawner>().spawned == false){
                Instantiate(templates.blockade, transform.position, templates.blockade.transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}