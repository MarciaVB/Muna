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
	public Camera _menhirCam;


	// Use this for initialization
	void Start () {
	
		_finalPos = this.transform.position;

		_startPos = _finalPos - new Vector3(0,_height,0);
		this.transform.position = _startPos;
		_menhirCam.enabled = false;
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
			_menhirCam.enabled = true;
			_moveTime += Time.deltaTime * 0.25f;
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
