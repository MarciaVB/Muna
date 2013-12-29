using UnityEngine;
using System.Collections;

public class InteractPrompt : MonoBehaviour {

	#region GUI variables
	public GUISkin _guiSkin;
	private Color _transparant;
	private Color _originalColor;
	private int _width;
	private int _height;
	#endregion
	
	bool _showPrompt = false;

	public GameObject _player;
	private GameObject[] _interactiveObjectsList;

	// Use this for initialization
	private void Start () {

		//Make GUI colors
		_originalColor = new Color(_guiSkin.box.normal.textColor.r,_guiSkin.box.normal.textColor.g,_guiSkin.box.normal.textColor.b, 1);
		_transparant = new Color(_originalColor.r,_originalColor.g,_originalColor.b,0);
		_guiSkin.box.normal.textColor = _transparant;

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
			_guiSkin.box.normal.textColor = Color.Lerp(_guiSkin.box.normal.textColor, _originalColor, 5 *Time.deltaTime);
		}

		else
		{
			_guiSkin.box.normal.textColor = Color.Lerp(_guiSkin.box.normal.textColor, _transparant, 5 *Time.deltaTime);
			
		}

	}

	private void OnGUI()
	{
		GUI.skin = _guiSkin;
		_width = Screen.width/5;
		_height = Screen.height/7;
		GUI.Box(new Rect(Screen.width/2-_width/2,Screen.height-_height, _width, _height),"Press E");
	}
}
