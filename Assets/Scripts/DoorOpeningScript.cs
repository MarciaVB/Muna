using UnityEngine;
using System.Collections;

public class DoorOpeningScript : MonoBehaviour {
	
	public GameObject _requiredCrystal;
	private InteractiveObjBehaviour _interactiveCrystal;
	bool opened = false;

	// Use this for initialization
	void Start () {

		_interactiveCrystal = _requiredCrystal.GetComponent<InteractiveObjBehaviour>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if(_interactiveCrystal.isActivated && !opened)
		{
			Debug.Log("Open Sesame");
			animation["Open"].wrapMode = WrapMode.Once;
			animation.Play("Open");
			opened=true;
			this.collider.enabled = false;
		}
	
	}
}
