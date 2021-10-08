using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour{   
[SerializeField] private Camera cam;  
    public float turnSpeed = 0.01f;
    public GameObject player;
    private Vector3 point;
 
    public float height = 2f;
    public float distance = 2f;
     
    private Vector3 offsetX;
    private Vector3 offsetY;
     
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        offsetX = new Vector3 (0, height, distance);
        offsetY = new Vector3 (0, 0, distance);
    }
//Mathf.Repeat(currentRotation.x, 360)     
    void LateUpdate()
    {
        Vector3 playerloc = player.transform.position; 
        //offsetX = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;
        //cam.transform.position = new Vector3(playerloc.x, playerloc.y,playerloc.z);
        cam.transform.position = new Vector3();
        cam.transform.Rotate(new Vector3(1,0,0),Input.GetAxis("Mouse Y") * turnSpeed);
        cam.transform.Rotate(new Vector3(0,1,0),Input.GetAxis("Mouse X") * turnSpeed, Space.World);
        //offsetY = Quaternion.AngleAxis (Input.GetAxis("Mouse Y") * turnSpeed, Vector3.right) * offsetY;
        //transform.position = player.position + offsetX + offsetY; 
        //transform.LookAt(player.position);
    }

}

