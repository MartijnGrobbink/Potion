using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// See "EnemyInput" for more information
/// </summary>
[RequireComponent(typeof(BoxCollider), typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
	// Rigidbody2D should already be in use, hence the "new" Rigidbody2D rigidbody2D
	new Rigidbody rigidbody;
	BoxCollider boxCollider;
	// SerializeField, to keep class encapsulated: https://unity3d.college/2016/02/15/editing-unity-variables-encapsulation-serializefield/
	[SerializeField]
	float speed;
	float speedModifier;

	Vector3 direction;
	Vector3 lastDirection;
	Vector3 wallPosition;
	Vector3 playerPosition;
	Vector3 wallLocation;

	private Vector2 currentRotation;
	public float sensitivity = 10f;

	[SerializeField]
	LayerMask movementBlockMask;
	Transform myTransform;

	public LayerMask m_LayerMask;

	public UnityAction onStopMove;
	public UnityAction onMove;

	public float SpeedModifier
	{
		get { return speedModifier; }
		set { speedModifier = value; }
	}

	public Vector3 Direction
	{
		get { return direction; }
		private set { direction = value; }
	}

	private void Start()
	{ // Make sure we never need GetComponents after Start
		rigidbody = GetComponent<Rigidbody>();
		boxCollider = GetComponent<BoxCollider>();
		myTransform = transform;
	}

	private void FixedUpdate()
	{
		ApplyMovement();
	}

	/*public void SetMovementModifiers(BrainModifiers modifiers)
	{
		speedModifier += modifiers.speedModifier;
		speedModifier += Random.Range(speedModifier * 0.9F, speedModifier * 1.1F);
		speed += modifiers.speed;
	}
*/
void OnTriggerEnter(Collider other){
if (other.CompareTag("Wall"))
		{
			playerPosition = myTransform.transform.position;
			wallPosition = other.transform.position;
			wallLocation = wallPosition - playerPosition;
			Debug.Log(wallLocation);
			if(wallPosition.x >= playerPosition.x){
				Debug.Log("test");
			}
		}
}
	private void ApplyMovement()
	{
		if (this.direction == Vector3.zero)
		{
			//GetComponent<Rigidbody>().velocity = Direction;
			onStopMove?.Invoke();
		} else {

		Vector3 direction = this.direction * (speed * SpeedModifier);
		//Rigidbody.velocity = direction;
		onMove?.Invoke();
		}
	}

	public void Move(Vector3 direction)
	{
		transform.position += direction * speed;
		//currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
		//currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
		//transform.rotation = Quaternion.Euler(0,currentRotation.x,0);
	}

	public bool IsMoving()
	{
		return direction != Vector3.zero;
	}
/*
	public Vector3 GetLastDirection(bool getLargestAxisMagnitude = true)
	{
		// Get the direction
		if (!getLargestAxisMagnitude)
			return lastDirection;

		Vector3 movementVector = lastDirection;
		if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y))
			movementVector.y = 0;
		else
			movementVector.x = 0;

		// This is for the animator, to return the last position the player was facing
		return movementVector;
	} */
}