using UnityEngine;
using System.Collections;

public class InteractPrompt : MonoBehaviour {

	#region GUI variables
	public GUISkin _guiSkin;
	private Color _transparantInner;
	private Color _originalInner;
	private Color _innerColor;
	
	private Color _transparantOuter;
	private Color _originalOuter;
	private Color _outlineColor;

	private int _width;
	private int _height;

	private float _timer = 0.0f;
	#endregion
	
	bool _showPrompt = false;

	public GameObject _player;
	private GameObject[] _interactiveObjectsList;

	// Use this for initialization
	private void Start () {

		//Make GUI colors
		_originalInner = new Color(_guiSkin.box.normal.textColor.r,_guiSkin.box.normal.textColor.g,_guiSkin.box.normal.textColor.b, 1);
		_transparantInner = new Color(_originalInner.r,_originalInner.g,_originalInner.b,0);

		//Make GUI colors
		_originalOuter = Color.black;
		_transparantOuter = new Color(0,0,0,0);

		//Find interactive objects
		_interactiveObjectsList = GameObject.FindGameObjectsWithTag("Interactive");
	
	}

	private void Update()
	{
		_showPrompt = false;

		foreach(GameObject obj in _interactiveObjectsList)
		{
			float distance = (obj.transform.position - _player.transform.position).magnitude;
			var interactiveComponent = obj.GetComponent<InteractiveObjBehaviour>();

			if(distance < interactiveComponent.ActivationDistance)	//Player close to obj
			{
				interactiveComponent.PlayerInReach = true;

				if(!interactiveComponent.isActivated)
					_showPrompt = true;
			}
			else
				interactiveComponent.PlayerInReach = false;
		}

		Fade();
	}

	private void Fade()
	{
		if(_showPrompt)
		{
			if(_timer < 1)
				_timer += Time.deltaTime;

			//_guiSkin.box.normal.textColor = Color.Lerp(_guiSkin.box.normal.textColor, _originalColor, 5 *Time.deltaTime);
			_innerColor = Color.Lerp(_transparantInner, _originalInner, _timer*2f);

			_outlineColor = Color.Lerp(_transparantOuter, _originalOuter, _timer*1.5f);
		}

		else
		{
			if(_timer > 0)
				_timer -= Time.deltaTime;

			//_guiSkin.box.normal.textColor = Color.Lerp(_guiSkin.box.normal.textColor, _transparant, 5 *Time.deltaTime);
			_innerColor = Color.Lerp(_transparantInner, _originalInner, _timer *1.5f);
			_outlineColor = Color.Lerp(_transparantOuter, _originalOuter, _timer*2f);
			
		}

	}

	private void OnGUI()
	{
		GUI.skin = _guiSkin;
		_width = Screen.width/5;
		_height = Screen.height/7;

		_guiSkin.box.normal.textColor = _outlineColor;
		GUI.Box(new Rect(Screen.width/2-_width/2 - 1,Screen.height-_height-1, _width, _height),"Press E");
		GUI.Box(new Rect(Screen.width/2-_width/2 - 1,Screen.height-_height+1, _width, _height),"Press E");
		GUI.Box(new Rect(Screen.width/2-_width/2 + 1,Screen.height-_height-1, _width, _height),"Press E");
		GUI.Box(new Rect(Screen.width/2-_width/2 + 1,Screen.height-_height+1, _width, _height),"Press E");
		
		_guiSkin.box.normal.textColor = _innerColor;
		GUI.Box(new Rect(Screen.width/2-_width/2,Screen.height-_height, _width, _height),"Press E");

	}
}
