using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public const int scoreStep = 1;

	public int score = 0;
	public int id;
	public Sign currentSign;

	public ParticleSystem exploder;
	
	public MeterController meter;
	public Sign sign;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Rock" + id)) {
//			Debug.Log("Pressed Rock");
			SendSign(Sign.ROCK_ID);
		}

		if (Input.GetButtonDown ("Paper" + id)) {
//			Debug.Log("Pressed Paper");
			SendSign(Sign.PAPER_ID);
		}

		if (Input.GetButtonDown ("Scissors" + id)) {
//			Debug.Log("Pressed Scissors");
			SendSign(Sign.SCISSORS_ID);
		}
		
		if (Input.GetButton ("Phaser" + id)) {
			if(currentSign != null){
				meter.StopMeter();
				meter.DecreaseMeter();
				currentSign.isMeterCharging = false;
			}
		}

		if(currentSign != null && currentSign.isScoring)
		{
			score = score + scoreStep;
		}
	}

	void OnGUI() {
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

		GUI.Label(new Rect(pos.x, Screen.height - pos.y, 140, 20), ""+score);
	}

	public void StartMeter () {
		meter.StartMeter();
	}
	
	public void StopMeter () {
		meter.StopMeter();
	}

	public void SendSign( int signType )
	{
		if (currentSign == null) { 
			Sign signClone = (Sign) Instantiate(sign, transform.position, transform.rotation);
			signClone.transform.parent = gameObject.transform.parent;
			signClone.Initialize(signType, id);
			currentSign = signClone;
		}
	}


}
