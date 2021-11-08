using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Movement movement;
    public PlayerInput playerInput;

    public List<GameObject> walls;
    public float xPercentage;
    public float zPercentage;
    public List<CollisionDirection> collisionDirList;

    private Vector3 wallPosition;
	private Vector3 playerPosition;
	private Vector3 playerScale;
	private Vector3 wallScale;

	private Vector3 wallRawLocation;
	private Vector3 wallLocation;

    public enum CollisionDirection { None, Front, Back, Left, Right, FL, FR, BL, BR}
    public CollisionDirection collisionDirection = CollisionDirection.None;

    void Start(){
        movement = this.GetComponent<Movement>();
    }
    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Wall"))
        {
            walls.Add(other.gameObject);

            playerPosition = movement.myTransform.transform.position;
            playerScale = movement.myTransform.transform.localScale;

            wallPosition = other.transform.position;
            wallScale = other.transform.localScale;

            wallRawLocation = wallPosition - playerPosition;

            if(wallRawLocation.x <= 0){
                wallLocation.x = wallRawLocation.x * -2 - playerScale.x;
                } else {
                    wallLocation.x = wallRawLocation.x * 2 - playerScale.x;
                    }

            if(wallRawLocation.z <= 0){
                wallLocation.z = wallRawLocation.z * -2 - playerScale.z;
                } else {
                    wallLocation.z = wallRawLocation.z * 2 - playerScale.z;
                    }

            xPercentage = (wallLocation.x / wallScale.x) * 100;
            zPercentage = (wallLocation.z / wallScale.z) * 100;

                if(zPercentage > xPercentage && wallRawLocation.z > 0)
                {
                    collisionDirection = CollisionDirection.Front;
                }

                if(zPercentage > xPercentage && wallRawLocation.z < 0)
                {
                    collisionDirection = CollisionDirection.Back;
                }

                if(zPercentage < xPercentage && wallRawLocation.x > 0)
                {
                    collisionDirection = CollisionDirection.Right;
                }

                if(zPercentage < xPercentage && wallRawLocation.x < 0)
                {
                    collisionDirection = CollisionDirection.Left;
                }
            collisionDirList.Add(collisionDirection);

            if(walls.Count == 2)
            {
                if(collisionDirList[0] == CollisionDirection.Front && collisionDirList[1] == CollisionDirection.Right || collisionDirList[1] == CollisionDirection.Front && collisionDirList[0] == CollisionDirection.Right)
                {
                    collisionDirection = CollisionDirection.FR;
                }  
                if(collisionDirList[0] == CollisionDirection.Front && collisionDirList[1] == CollisionDirection.Left || collisionDirList[1] == CollisionDirection.Front && collisionDirList[0] == CollisionDirection.Right)
                {
                    collisionDirection = CollisionDirection.FL;
                }
                if(collisionDirList[0] == CollisionDirection.Back && collisionDirList[1] == CollisionDirection.Right || collisionDirList[1] == CollisionDirection.Front && collisionDirList[0] == CollisionDirection.Right)
                {
                    collisionDirection = CollisionDirection.BR;
                }
                if(collisionDirList[0] == CollisionDirection.Back && collisionDirList[1] == CollisionDirection.Left || collisionDirList[1] == CollisionDirection.Front && collisionDirList[0] == CollisionDirection.Right)
                {
                    collisionDirection = CollisionDirection.BL;
                }
            }
		}
	}
    
    void OnTriggerExit(Collider other){
        if(other.CompareTag("Wall"))
        {
            int index = walls.IndexOf(other.gameObject);
            walls.Remove(other.gameObject);
            collisionDirList.Remove(collisionDirList[index]);
            if(walls.Count == 0)
            {
                collisionDirection = CollisionDirection.None;
            }
        }
    } 
}
