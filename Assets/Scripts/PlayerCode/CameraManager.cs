using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{   
  
[SerializeField] private Camera cam;  
    public float turnSpeed = 1f;
    public Transform player;
    private Vector3 point;
 
    public float height = 2f;
    public float distance = 2f;
     
    private Vector3 offsetX;
    private Vector3 offsetY;
    Vector3 offset;
/*     
    void Start () {
       // player = GameObject.FindGameObjectWithTag("Player");
        offsetX = new Vector3 (0, height, distance);
        offsetY = new Vector3 (0, 0, distance);
        offset = new Vector3 (0,0,0);
    }
//Mathf.Repeat(currentRotation.x, 360)  
 
    void LateUpdate()
    {
        Vector3 playerloc = player.transform.position; 
        offsetX = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
        cam.transform.position = new Vector3(playerloc.x, playerloc.y,playerloc.z);
        offsetY = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
        offset =  offsetY + offsetX; 
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }
*/
///*
     void Start () {
         offset = new Vector3(player.position.x, player.position.y + 8.0f, player.position.z + 7.0f);
     }
 
     void LateUpdate()
     {
         offset = Quaternion.AngleAxis (Input.GetAxisRaw("QandE") * turnSpeed, Vector3.up) * offset;
         transform.position = player.position + offset; 
         transform.LookAt(player.position);
     }
//*/
}

