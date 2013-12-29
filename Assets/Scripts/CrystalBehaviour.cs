using UnityEngine;
using System.Collections;

[RequireComponent (typeof (InteractiveObjBehaviour))]

public class CrystalBehaviour : MonoBehaviour {

	#region Light variables
	private Light _childLight;

	public float _maxIntensity;
	public float _minIntensity;
	private float _midIntensity;

	private bool _isLightDimming = false;

	public float _pulsingSpeed;

	public Color _finalColor;
	#endregion

	private InteractiveObjBehaviour _interactiveComponent;


	private void Start () {

		//Get InteractiveObjBehaviour Component
		_interactiveComponent = this.GetComponent<InteractiveObjBehaviour>();

		//Light
		InitialiseLight();
	}
	
	// Update is called once per frame
	private void Update () {

		//If crystal is not yet activated
		if(!_interactiveComponent.isActivated)
		{
			if(_interactiveComponent.PlayerInReach)	//Player close to crystal
			{
				LightPulse();
			}

			else 	//Player NOT close to crystal
			{
				RestoreLight();
			}
		}
		
		//if activated
		else
		{
			SwitchToActivatedLight();
			this.audio.volume = Mathf.Lerp(this.audio.volume,0,Time.deltaTime);
			if(audio.volume == 0) audio.enabled = false;
		}
		
	
	}

	#region LightMethods
	private void InitialiseLight()
	{
		//Get Light
		_childLight = this.GetComponentInChildren<Light>();
		if(_childLight == null)
			Debug.Log("WARNING: Light not found");
		//Set light intensity
		_childLight.intensity = _midIntensity;
		
		//Calculate midIntensity
		_midIntensity = _maxIntensity + _minIntensity / 2;

	}
	
	private void LightPulse()
	{
		//Check whether light should dim or become brighter
		if(_childLight.intensity <= _minIntensity)
			_isLightDimming = false;
		
		else if(_childLight.intensity >= _maxIntensity)
			_isLightDimming = true;

		//Dim
		if(_isLightDimming)
		{
			_childLight.intensity = _childLight.intensity - _pulsingSpeed * Time.deltaTime;
		}

		//Become brighter
		else
		{
			_childLight.intensity = _childLight.intensity + _pulsingSpeed * Time.deltaTime;
		}
	}

	private void RestoreLight()
	{
		if (_childLight.intensity < _midIntensity)
		{
			_childLight.intensity += _pulsingSpeed * Time.deltaTime;
			if(_childLight.intensity > _midIntensity) _childLight.intensity =_midIntensity;
		}
		
		else if(_childLight.intensity > _midIntensity)
		{
			_childLight.intensity -= _pulsingSpeed * Time.deltaTime;	
			if(_childLight.intensity < _midIntensity) _childLight.intensity =_midIntensity;
		}
	}

	private void SwitchToActivatedLight()
	{
		//Change intensity
		_childLight.intensity = Mathf.Lerp(_childLight.intensity, _maxIntensity, _pulsingSpeed * Time.deltaTime);

		//Change color
		_childLight.color = Color.Lerp(_childLight.color,_finalColor,_pulsingSpeed * Time.deltaTime);
	}
	#endregion

}
