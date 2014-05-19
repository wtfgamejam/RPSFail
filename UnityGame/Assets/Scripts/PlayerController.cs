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

	private bool m_phasersOnStun;
	private bool m_beingStunned;
	
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
			if(currentSign != null && meter.meter > 0){
				meter.StopMeter();
				meter.DecreaseMeter();
				currentSign.isMeterCharging = false;
				phasersOnStun = true;
			} else {
				phasersOnStun = false;
			}
		}

		if (Input.GetButtonUp ("Phaser" + id)) {
			phasersOnStun = false;
		}

		if(currentSign != null && currentSign.isScoring)
		{
			score = score + scoreStep;
		}

		if (currentSign != null && beingStunned && !currentSign.isScoring) {
			currentSign.stun_multiplier = 2f;
		} else if (currentSign != null) {
			currentSign.stun_multiplier = 1f;
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

	public void ShootPhaser (){
		Debug.Log ("Shooting Phaser");
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

// Getters and Setters
	public bool phasersOnStun
	{
		get {return m_phasersOnStun;}
		set {m_phasersOnStun = value;}
	}
	public bool beingStunned
	{
		get {return m_beingStunned;}
		set {m_beingStunned = value;}
	}	
}
