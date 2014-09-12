using UnityEngine;
using System.Collections;

public class IntroText : MonoBehaviour {
	
	public GUISkin _skin;
	public string _TextLookAround = "Use the right stick to look around";
	public string _MoveLookAround = "Use the left stick to move around";
	
	private bool _isGUIActive = true;
	private bool _hasStickMoved = false;
	private string _currentString;
	private int _introState=1;
	// Use this for initialization
	void Start () {
		_currentString = _TextLookAround;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		switch(_introState)
		{
		case 1:
			if(Input.GetAxis("Right Horizontal") != 0)
				_hasStickMoved = true;
	
		if(_hasStickMoved)
			_introState = 2;
			break;
		
		
		case 2:
			_currentString = _MoveLookAround;
			_hasStickMoved = false;
			
			if(Input.GetAxis("Vertical") != 0)
				_hasStickMoved = true;
			
			if(_hasStickMoved)
				_introState = 3;
			break;
			
		case 3:
			_isGUIActive = false;
			break;
		}
			
	}
	
	void OnGUI()
	{
		if(_isGUIActive)
		{
			GUI.skin = _skin;
			
			_skin.box.normal.textColor = Color.black;
			GUI.Box(new Rect(-1,-1,Screen.width, Screen.height),_currentString);
			GUI.Box(new Rect(1,-1,Screen.width, Screen.height),_currentString);
			GUI.Box(new Rect(-1,1,Screen.width, Screen.height),_currentString);
			GUI.Box(new Rect(1,1,Screen.width, Screen.height),_currentString);

			
			_skin.box.normal.textColor = Color.white;
			GUI.Box(new Rect(0,0,Screen.width, Screen.height),_currentString);
		}
	}
}
