using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement)/*, typeof(Health)*/)]
public class PlayerInput : MonoBehaviour
{
	// Tip: You can probably use the new Input System instead of this...
	[SerializeField]
	string horizontal = "Horizontal";
	[SerializeField]
	string vertical = "Vertical";
	[SerializeField]
	string fireButton;

	//[SerializeField]
	//float maxHealth = 100;
	//Health health;
	Movement movement;
	PlayerCollision playerCollision;
	//public UnityAction onFire;
	//CameraManager cameraManager;

	private void Start()
	{
		movement = GetComponent<Movement>();
		playerCollision = GetComponent<PlayerCollision>();
		movement.SpeedModifier = 1;
		
	}

	// Update is called once per frame
	void Update()
	{
		Move();
		
		//Fire();
	}

	public void Move()
	{	
		float x = Input.GetAxis(horizontal);
		float z = Input.GetAxis(vertical);

		if(playerCollision.collisionDirection == PlayerCollision.CollisionDirection.Right && x > 0)
		{
			x = 0;
		}
		if(playerCollision.collisionDirection == PlayerCollision.CollisionDirection.Left && x < 0)
		{
			x = 0;
		}
		if(playerCollision.collisionDirection == PlayerCollision.CollisionDirection.Front && z > 0)
		{
			z = 0;
		}
		if(playerCollision.collisionDirection == PlayerCollision.CollisionDirection.Back && z < 0)
		{
			z = 0;
		}



		if(playerCollision.collisionDirection == PlayerCollision.CollisionDirection.FR && x > 0 && z > 0 
		|| playerCollision.collisionDirection == PlayerCollision.CollisionDirection.FR && x > 0 
		|| playerCollision.collisionDirection == PlayerCollision.CollisionDirection.FR && z > 0)
		{
			x = 0;
			z = 0;
		}
		if(playerCollision.collisionDirection == PlayerCollision.CollisionDirection.FL && x < 0 && z > 0
		||playerCollision.collisionDirection == PlayerCollision.CollisionDirection.FL && x < 0
		||playerCollision.collisionDirection == PlayerCollision.CollisionDirection.FL && z > 0)
		{
			x = 0;
			z = 0;
		}
		if(playerCollision.collisionDirection == PlayerCollision.CollisionDirection.BR && x > 0 && z < 0
		||playerCollision.collisionDirection == PlayerCollision.CollisionDirection.BR && x > 0
		||playerCollision.collisionDirection == PlayerCollision.CollisionDirection.BR && z < 0)
		{
			x = 0;
			z = 0;
		}
		if(playerCollision.collisionDirection == PlayerCollision.CollisionDirection.BL && x < 0 && z < 0
		||playerCollision.collisionDirection == PlayerCollision.CollisionDirection.BL && x < 0
		||playerCollision.collisionDirection == PlayerCollision.CollisionDirection.BL && z < 0)
		{
			x = 0;
			z = 0;

		} 
		movement.Move(new Vector3(x, 0, z));
	}
	/*private void Fire()
	{
		// Doesn't do anything currently, you can add a weapon class and then subscribe to "onFire"
		if(Input.GetButton(fireButton))
			onFire?.Invoke();
	}

	private void OnDisable()
	{
		// Tell the CameraManager that this object is gone and doesn't need to be tracked anymore
		//cameraManager.RemovePlayer(transform);
	}

	private void Die()
	{
		Debug.Log($"Player {gameObject.GetInstanceID()} has died");
		Destroy(gameObject);
	}*/
}
