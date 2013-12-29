using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public float _movementSpeed = 15;
	public float _rotSpeed = 10;
	
	private float _horizontalAxis;
	private float _verticalAxis;
	private Vector3 _stickDirection;
	
	private Animator _animator;
	
	// Use this for initialization
	void Start () {
		
		//Animator
		_animator = GetComponent<Animator>();
		
		if(_animator)
		{
			if(_animator.layerCount >= 2)
	        {
	        	_animator.SetLayerWeight(1, 1);
	        }
		}
		
		else
		{
			Debug.Log("No animator component");
		}
	}
	
	// FixedUpdate because rigidbody	
	void FixedUpdate()
	{	
		bool isGrounded = Physics.Raycast(transform.position, -Vector3.up, 1.0f);
		
		_horizontalAxis = Input.GetAxis("Horizontal");
		_verticalAxis = Input.GetAxis("Vertical");
		
		
		var lookDir = Camera.main.transform.forward;
		//lookDir.Normalize();
		lookDir.y = 0;
		Debug.DrawRay(this.transform.position + new Vector3(0,1,0), lookDir, Color.blue);
		
        _stickDirection = new Vector3(_horizontalAxis, 0, _verticalAxis);
		
		 Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, Vector3.Normalize(lookDir));

        // Convert joystick input in Worldspace coordinates
        Vector3 moveDirection = referentialShift * _stickDirection;
           
        //Debug.DrawRay(this.transform.position + new Vector3(0,1,0), stickDirection, Color.blue); 

		float speed = new Vector2(_horizontalAxis,_verticalAxis).sqrMagnitude;
		
		if(_animator)
		{
			_animator.SetFloat("Speed", speed);
		}
		
		if(speed != 0)
		{
			this.audio.volume = 1.0f;
			//Enables character to move
			rigidbody.isKinematic = false;
			RaycastHit hit = new RaycastHit();
			Physics.Raycast(transform.position,-transform.up,out hit,2);
			if(Vector3.Dot(Vector3.up,hit.normal)> 0.7)
			{
				//Move
				transform.position += moveDirection * Time.deltaTime * _movementSpeed;
			}
			//Rotate
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (moveDirection), _rotSpeed * Time.deltaTime);
		}
		
		else
		{
			this.audio.volume = 0;
			//Once character is grounded
			if(isGrounded)
				//Disables character to move (slide)
				rigidbody.isKinematic = true;
		}
	}
	
}
