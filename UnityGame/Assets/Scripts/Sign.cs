using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sign : MonoBehaviour {

	public const int BASE_ID = 0;
	public const int ROCK_ID = 1;
	public const int PAPER_ID = 2;
	public const int SCISSORS_ID = 3;
	public const string BASE_NAME = "hidden";
	public const string ROCK_NAME = "rock";
	public const string PAPER_NAME = "paper";
	public const string SCISSORS_NAME = "scissors";

	private const float SPRITE_SCALE = 0.5f;

	private float m_speed;
	private int m_type;
	private bool m_scoring;
	private bool m_visible;
	private bool m_charging;

	public int player_id;

	private float path_location;
	private float path_percent_complete;
	private float path_length;

	public Dictionary<int, string> spriteNames;

	private Main main;

// HOUSEKEEPING
	void Awake() {
		spriteNames = new Dictionary<int, string>();
		spriteNames.Add(BASE_ID, BASE_NAME);
		spriteNames.Add(ROCK_ID, ROCK_NAME);
		spriteNames.Add(PAPER_ID, PAPER_NAME);
		spriteNames.Add(SCISSORS_ID, SCISSORS_NAME);

		isScoring = false;
		isVisible = false;

		// Set scale
		gameObject.transform.localScale = new Vector3(SPRITE_SCALE, SPRITE_SCALE, SPRITE_SCALE);

		GameObject maincamera = GameObject.Find("MainCamera");
		if ( maincamera )
		{
			main = maincamera.GetComponent < Main > ();
			
			if ( !main )
			{
				Debug.Log( "main camera not found" );
			}
		}
		else
		{
			Debug.Log( "main camera NOT FOUND ...." );
		}
	}

	// Use this for initialization
	void Start () {
		iTween.MoveTo (gameObject, iTween.Hash ("y", 0,
	                                        "x", 0,
	                                        "time", 7,
	                                        "islocal", true,
	                                        "easeType", "linear",
	                                        "name", "signMove" + player_id));
		// Rotate to north
		gameObject.transform.localEulerAngles = new Vector3(0,0,0);

		// Calculate path_length
		path_length = Vector3.Distance(gameObject.transform.position, gameObject.transform.parent.position);
	

	}
	
	// Update is called once per frame
	void Update () {
		path_location = Vector3.Distance(gameObject.transform.position, gameObject.transform.parent.position);
		//print (path_location);
		
		path_percent_complete = path_location / path_length * 100;
		//print (Mathf.Round (path_percent_complete));
		
		if (path_percent_complete < 25 && !isScoring) {
			// Drop in the center
			if(gameObject)
			{
				iTween.StopByName("signMove" + player_id);
				iTween.MoveTo(gameObject, iTween.Hash ("y", 0,
				                                       "x", 0,
				                                       "time", .5,
				                                       "islocal", true,
				                                       "easeType", "easeInBack",
				                                       "name", "signBounce" + player_id));
			}
			isScoring = true;
			isMeterCharging = false;

			main.Resolve(this);

		}
		if (path_percent_complete < 80 && !isScoring && !isMeterCharging) {
			isMeterCharging = true;
			isVisible = true;
			iTween.RotateAdd(gameObject, iTween.Hash("y",180) );
		}
	}

	// 
	void FixedUpdate () {

	}

	public void Initialize(int in_type, int in_player_id)
	{
		Debug.Log("Player Id "+in_player_id);
		player_id = in_player_id;
		type = in_type;
		isVisible = false;

	}

// OPERATIONS

	private void UpdateSprite(int in_type)
	{
		Debug.Log(in_type);
		Debug.Log(spriteNames[in_type]);
		//Does loading a rescource every time cause a problem?
		gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load(spriteNames[in_type], typeof(Sprite)) as Sprite;
	}

// GETTERS SETTERS
	public float speed
	{
		get {return m_speed;}
		set {m_speed = value;}
	}
	
	public int type
	{
		get {return m_type;}
		set {
			m_type = value;
			Debug.Log ("Is Visible: " + isVisible);
			// Change sprite here, based on visible property
			if(isVisible) UpdateSprite(m_type);
			else UpdateSprite(BASE_ID);
		}
	}
	
	public bool isScoring 
	{
		get {return m_scoring;}
		set {m_scoring = value;}
	}

	public bool isMeterCharging
	{
		get {return m_charging;}
		set {
			m_charging = value;
			main.MeterCharging(this);
		}
	}

	public bool isVisible 
	{
		get {return m_visible;}
		set {
			m_visible = value;
			// Change sprite to type if visible
			if(m_visible) UpdateSprite(m_type);
		}
	}
}
