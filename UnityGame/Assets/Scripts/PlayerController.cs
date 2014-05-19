using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public const int ROUNDS_TO_WIN = 5;
	public const int WIN_VALAUE = 100;
	public const int SCORE_STEP = 50;
	public int score = 0;

	public int id;
	public Sign currentSign;
	public ParticleSystem exploder;
	public MeterController meter;
	public Sign sign;

	public int roundsWon;

	private bool m_phasersOnStun;
	private bool m_beingStunned;

	private bool scoreRunning;
	private bool hasStartedScoring;
	

	AudioSource phaser_attack;
	AudioSource phaser_sustain;
	AudioSource phaser_release;

	// Use this for initialization
	void Start () {
		scoreRunning = false;
		hasStartedScoring = false;

		roundsWon = PlayerPrefs.GetInt("Round"+id, 0);
		AudioSource[] audios = GetComponents<AudioSource>();
		phaser_attack = audios[0];
		phaser_sustain = audios[1];
		phaser_release = audios[2];
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Rock" + id)) {
			SendSign(Sign.ROCK_ID);
		}

		if (Input.GetButtonDown ("Paper" + id)) {

			SendSign(Sign.PAPER_ID);
		}

		if (Input.GetButtonDown ("Scissors" + id)) {
			SendSign(Sign.SCISSORS_ID);
		}
		if (Input.GetButtonDown ("Phaser" + id)) {
			phaser_attack.Play();
		}

		if (Input.GetButton ("Phaser" + id)) {
			if(currentSign != null) {
				meter.StopMeter();
				meter.DecreaseMeter();
				currentSign.isMeterCharging = false;
			}

			if (meter.meter > 1) {
				phasersOnStun = true;
				phaser_sustain.Play();
			} else {
				phasersOnStun = false;
				phaser_sustain.Stop();
				phaser_attack.Stop();
			}
		}

		if (Input.GetButtonUp ("Phaser" + id)) {
			if (phasersOnStun){
				phasersOnStun = false;
				phaser_release.Play();
			} else {
				phaser_sustain.Stop();
				phaser_attack.Stop();
				phaser_release.Stop();
			}
		}

		if (currentSign != null && beingStunned && !currentSign.isResolving) {
			currentSign.stun_multiplier = 2f;
		} else if (currentSign != null) {
			currentSign.stun_multiplier = 1f;
		}

		if(currentSign != null && currentSign.isScoring && !hasStartedScoring)
		{
			hasStartedScoring = true;
			scoreRunning = true;
			Debug.Log("what score"+score);
			InvokeRepeating("ScoreTick", 0f, 1.0f);
		}

		if(currentSign == null)
		{
			hasStartedScoring = false;
			CancelInvoke("ScoreTick");
		}
	}

	void ScoreTick()
	{
		if ( score < WIN_VALAUE)
		{
			score = score + SCORE_STEP;
		}
		else
		{
			CancelInvoke("ScoreTick");
			EvaluateWinCondition();
		}
	}

	void EvaluateWinCondition()
	{
		roundsWon++;
		PlayerPrefs.SetInt("Round"+id, roundsWon);
		Debug.Log("Rounds won " + roundsWon);
		PlayerPrefs.SetInt("CurrentWinner", id);

		if(roundsWon == ROUNDS_TO_WIN)
		{
			//end game
		}
		else
		{
			//end round
			Application.LoadLevel (3);
		}
	}

	void OnGUI() {
		Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);

		GUI.Label(new Rect(pos.x, Screen.height - pos.y, 140, 20), ""+score);
		GUI.Label(new Rect(pos.x + 10, Screen.height + 10 - pos.y, 140, 20), "Rounds Won:"+roundsWon);
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
