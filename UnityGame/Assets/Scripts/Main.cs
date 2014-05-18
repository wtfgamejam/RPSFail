﻿using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {
	
	private Sign currentSign;

	public PlayerController player1;
	public PlayerController player2;
	public PlayerController player3;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Resolve(Sign in_sign)
	{
		if(currentSign == null)
		{
			currentSign = in_sign;
			return;
		}

		int test = ((currentSign.type + 1) % 3);
		Debug.Log("test? "+test);
		// is win?
		if( in_sign.type == test )
		{
			Debug.Log ("WINNER!");

			iTween.Stop(currentSign.gameObject);
			Destroy(currentSign.gameObject);
			currentSign = in_sign;
		}
		else
		{
			Debug.Log ("LOSE!");
			iTween.Stop(in_sign.gameObject);
			Destroy(in_sign.gameObject);
			if(in_sign.type == currentSign.type)
			{
				iTween.Stop(currentSign.gameObject);
				Destroy(currentSign.gameObject);
				Debug.Log ("well actually a tie!");
			}
		}
	}

}
