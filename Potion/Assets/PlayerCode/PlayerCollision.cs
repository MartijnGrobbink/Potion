using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Movement movement;
    public PlayerInput playerInput;

    private Vector3 wallPosition;
	private Vector3 playerPosition;
	private Vector3 playerScale;
	private Vector3 wallScale;

	private Vector3 wallRawLocation;
	private Vector3 wallLocation;
	private Vector3 negWallLocation;

	private float negativeXPercentage;
	private float negativeZPercentage;
	private float xPercentage;
	private float zPercentage;

    private int collisionCount;
    public enum CollisionDirection { None, Front, Back, Left, Right }
    public CollisionDirection collisionDirection = CollisionDirection.None;
    void Start(){
        movement = this.GetComponent<Movement>();
    }
    void OnTriggerEnter(Collider other){
    if(other.CompareTag("Wall"))
    {
        collisionCount++;

        playerPosition = movement.myTransform.transform.position;
        playerScale = movement.myTransform.transform.localScale;
        wallPosition = other.transform.position;
        wallScale = other.transform.localScale;

        wallRawLocation = wallPosition - playerPosition;
        wallLocation = wallRawLocation * 2 - playerScale;
        negWallLocation = wallRawLocation * -2 - playerScale; 

        if(wallRawLocation.x <= 0){
            negativeXPercentage = (negWallLocation.x / wallScale.x) * 100;
            xPercentage = 0;
            } else {
                xPercentage = (wallLocation.x / wallScale.x) * 100;
                negativeXPercentage = 0;
                }
        if(wallRawLocation.z <= 0){
            negativeZPercentage = (negWallLocation.z / wallScale.z) * 100;
            zPercentage = 0;
            } else {
                zPercentage = (wallLocation.z / wallScale.z) * 100;
                negativeZPercentage = 0;
                }
			
        if(negativeXPercentage == 0){
            if(xPercentage > zPercentage && xPercentage > negativeZPercentage){
                collisionDirection = CollisionDirection.Right;
                } else if(zPercentage > negativeZPercentage){
                    collisionDirection = CollisionDirection.Front;
                    } else {
                        collisionDirection = CollisionDirection.Back;
                        }
        } else {
            if(negativeXPercentage > zPercentage && negativeXPercentage > negativeZPercentage){
                collisionDirection = CollisionDirection.Left;
            } else if(zPercentage > negativeZPercentage){
                collisionDirection = CollisionDirection.Front;
                } else {
                    collisionDirection = CollisionDirection.Back;
                }	
			}
		}
	}
    void OnTriggerExit(Collider other){
        if(other.CompareTag("Wall"))
        {
            collisionCount--;
            if(collisionCount == 0)
            {
                collisionDirection = CollisionDirection.None;
            }
        }
        //playerInput.Move(collisionDirection);
    }
}
