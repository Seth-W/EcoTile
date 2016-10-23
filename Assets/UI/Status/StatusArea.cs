﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusArea : MonoBehaviour {

	public Text energyText;
	public Text pollutionText;

	private float _energy;
	private float _pollution;

	public void Awake ()
	{
		_energy = 0;
		_pollution = 0;
	}

	public void SetEnergy ( float argValue )
	{
		_energy = argValue;
		energyText.text = _energy.ToString();
	}

	public void IncrementEnergy ()
	{
		_energy++;
		energyText.text = _energy.ToString();
	}

	public void DecrementEnergy ()
	{
		_energy--;
		energyText.text =_energy.ToString();
	}


	public void SetPollution ( float argValue )
	{
		_pollution = argValue;
		pollutionText.text = _pollution.ToString();
	}

	public void IncrementPollution ()
	{
		_pollution++;
		pollutionText.text = _pollution.ToString();
	}

	public void DecrementPollution ()
	{
		_pollution--;
		pollutionText.text = _pollution.ToString();
	}
}
