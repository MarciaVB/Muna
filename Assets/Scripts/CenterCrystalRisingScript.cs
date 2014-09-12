using UnityEngine;
using System.Collections;

public class CenterCrystalRisingScript : MonoBehaviour {

	public bool _cheatEnabled = true;
	private Vector3 _finalPos;
	private Vector3 _startPos;
	public GameObject[] _requiredCrystals;
	public float _height;
	private bool _activated = false;
	
	private float _moveTime = 0.0f;
	private float _moveTime2 = 0.0f;
	public Camera _menhirCam;
	private Vector3 _camStartPos;

	public GameObject _player;

	// Use this for initialization
	void Start () {
	
		_finalPos = this.transform.position;

		_startPos = _finalPos - new Vector3(0,_height,0);
		this.transform.position = _startPos;
		_menhirCam.enabled = false;
		_camStartPos = _menhirCam.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if(!_activated)
		{
			foreach (GameObject crystal in _requiredCrystals)
			{
				_activated = crystal.GetComponent<InteractiveObjBehaviour>().isActivated;
			}
		}

		else
		{
			if(_moveTime < 1)
			{
				_menhirCam.enabled = true;
				_moveTime += Time.deltaTime * 0.25f;
			}

			else
			{
				_moveTime2 += Time.deltaTime;

				_menhirCam.transform.position = Vector3.Lerp(_camStartPos, Camera.main.transform.position, _moveTime2);
				_menhirCam.transform.LookAt(Vector3.Lerp(this.transform.position, _player.transform.position, _moveTime2));

				if(_moveTime2 >= 1)
				{
					_menhirCam.enabled = false;
					Camera.main.enabled = true;
				}
			}

			this.transform.position = Vector3.Lerp(_startPos, _finalPos, _moveTime);
		}
	
		if(Input.GetKeyUp(KeyCode.C) && _cheatEnabled)
		{
			foreach (GameObject crystal in _requiredCrystals)
			{
				crystal.GetComponent<InteractiveObjBehaviour>().isActivated= true;
			}
		}
	}
}
