using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {
	
	#region Members
	
	public Vector3 _offset;					//Camera offset
	public int _maxY = 50;
	public int _minY = 0;
	
	private Transform _playerTransform;		//Player position
	private Vector3 _targetPos;				//Target Camera position
	
	private Vector3 _lookDir;				//Look Direction
	
	
	//Makes camera follows target smoothly
	//Lower value -> slower || Higher value -> faster
	public float _smooth = 2;
	
	#endregion
	
	void Start ()
	{
		//Get player transform
		_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
		_targetPos = this.transform.position;
	}
	
	
	//Late update comes after update.
	//Good for camera code so it calculates with the new values.
	private void LateUpdate ()
	{
		//lookDir = characterOffset - transform.position;
		_lookDir = _playerTransform.position - _targetPos;
		_lookDir.Normalize();
		_lookDir.y = 0;
		
		//Update targetPos
		_targetPos = 	_playerTransform.transform.position			//Get new playerPosition
						- _lookDir * _offset.z						//- Z-offset (backwards)
						+ _playerTransform.up * _offset.y	//+ Y-offset (up)
						+ _playerTransform.right * _offset.x;		//+ X-offset (right/left)
		
		
		//Debug.DrawLine(_playerTransform.position, _targetPos, Color.magenta);

		
		//Rotate left
		if(Input.GetAxis("Right Horizontal")> 0.5)
		{
			_targetPos = RotateAroundPoint(_targetPos,_playerTransform.position, Quaternion.Euler(0,90 * Time.deltaTime, 0));
		}
		
		//Rotate right
		if(Input.GetAxis("Right Horizontal")< -0.5)
		{
			_targetPos = RotateAroundPoint(_targetPos,_playerTransform.position, Quaternion.Euler(0,-90 * Time.deltaTime, 0));
		}
		
		//Go up
		if(Input.GetAxis("Right Vertical") < -0.5)
		{
			if(_offset.y < _maxY)
				_offset.y += 10 * Time.deltaTime;
			else _offset.y = _maxY;
		}
		
		//Go down
		if(Input.GetAxis("Right Vertical") > 0.5)
		{
			if(_offset.y > _minY)
				_offset.y -= 10 * Time.deltaTime;
			else _offset.y = _minY;
		}
		
		Debug.DrawLine(_playerTransform.position,_targetPos,Color.red);
		
		CompensateForWalls(_playerTransform.position, ref _targetPos);
		
		//Make camera move to targetPos
		transform.position = Vector3.Lerp(transform.position, _targetPos, _smooth * Time.deltaTime);
				
		//LookAt player
		transform.LookAt(_playerTransform);
	}
	
	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        // Compensate for walls between camera
        RaycastHit wallHit = new RaycastHit();                
        if (Physics.Linecast(fromObject, toTarget, out wallHit)) 
        {
			var wallPoint =  new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);
			var direction = wallPoint - fromObject;
			direction.Normalize();
			toTarget = wallPoint - direction;

        }
    }
	
	Vector3 RotateAroundPoint(Vector3 point, Vector3 pivotPoint, Quaternion angle)
	{
    	return angle * ( point - pivotPoint) + pivotPoint;
	}
}
