using UnityEngine;
using System.Collections;

public class InteractiveObjBehaviour : MonoBehaviour {
	
	private bool _isActivated = false;
	private bool _playerInReach = false;
	public float _activationDistance = 4.0f;
	public AudioClip _activationSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(_playerInReach && Input.GetButtonUp("Ebutton"))
		{
			_isActivated = true;
		}
	}

	public bool isActivated {
		get	{return _isActivated;}
		set {_isActivated = value;}
	}

	public float ActivationDistance {
		get	{return _activationDistance;}
		set {_activationDistance = value;}
	}

	
	public bool PlayerInReach {
		get	{return _playerInReach;}
		set {_playerInReach = value;}
	}
}
