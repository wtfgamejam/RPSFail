using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	
	private Sign currentSign;

	public PlayerController player1;
	public PlayerController player2;
	public PlayerController player3;

	private Dictionary<int, PlayerController> players;

	void Awake() {
		players = new Dictionary<int, PlayerController>();
		players.Add(1, player1);
		players.Add(2, player2);
		players.Add(3, player3);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		player1.beingStunned = false;
		player2.beingStunned = false;
		player3.beingStunned = false;

		if (player1.phasersOnStun) {
			player2.beingStunned = true;
			player3.beingStunned = true;
		}
		if (player2.phasersOnStun) {
			player1.beingStunned = true;
			player3.beingStunned = true;
		}
		if (player3.phasersOnStun) {
			player1.beingStunned = true;
			player2.beingStunned = true;
		}
	}

	public void Resolve(Sign in_sign)
	{
		if(currentSign == null)
		{
			currentSign = in_sign;
			return;
		}

		int test = ((currentSign.type + 1) % 3);
		// Scissors resolves to 0 after mod, change it to 3
		if(test == 0) test = 3;

		// if the current sign + 1 is equal to in_sign
		if( in_sign.type == test )
		{
			Debug.Log ("WINNER!");

			//iTween.Stop(currentSign.gameObject);
			//Destroy(currentSign.gameObject);
			currentSign.DestroySequenceStart();
			currentSign = in_sign;
		}
		else
		{
			Debug.Log ("LOSE!");
			//iTween.Stop(in_sign.gameObject);
			//Destroy(in_sign.gameObject);
			if(in_sign.type == currentSign.type)
			{
				//iTween.Stop(currentSign.gameObject);
				//Destroy(currentSign.gameObject);
				currentSign.DestroySequenceStart();
				Debug.Log ("TIE!");
				iTween.ShakePosition(gameObject,iTween.Hash("amount",new Vector3(.25f,.25f,.25f),
				                                            "time", .5f,
				                                            "islocal",true));	

			}
			in_sign.DestroySequenceStart();
		}
	}

	public void MeterCharging(Sign sign) {
		PlayerController player = players [sign.player_id];
		// Debug.Log ("isMeterCharging: " + sign.isMeterCharging);
		if (sign.isMeterCharging) {
			player.StartMeter ();
		} else {
			player.StopMeter ();
		}
	}

}
