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
	public const int COMBAT_ZONE = 80;
	public const int SCORING_ZONE = 25;
	
	private const float SPRITE_SCALE = 0.5f;

	private float m_speed;
	private int m_type;
	private bool m_scoring;
	private bool m_resolving;
	private bool m_visible;
	private bool m_charging;

	public int player_id;

	private float path_location;
	private float path_percent_complete;
	private float path_length;

	public Dictionary<int, string> spriteNames;

	private Main main;

	public Transform origin;
	public Transform destination;

	public float dist;
	public float move_speed;
	public float stun_multiplier;
	
// HOUSEKEEPING
	void Awake() {
		spriteNames = new Dictionary<int, string>();
		spriteNames.Add(BASE_ID, BASE_NAME);
		spriteNames.Add(ROCK_ID, ROCK_NAME);
		spriteNames.Add(PAPER_ID, PAPER_NAME);
		spriteNames.Add(SCISSORS_ID, SCISSORS_NAME);

		isScoring = false;
		isResolving = false;
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
	
		move_speed = 2f;
		stun_multiplier = 1f;

//		// Rotate to north
		gameObject.transform.localEulerAngles = new Vector3(0,0,0);

		// Calculate path_length
		//path_length = Vector3.Distance(gameObject.transform.position, gameObject.transform.parent.position);
	
		origin = gameObject.transform;
		destination = gameObject.transform.parent;

		path_length = Vector3.Distance (origin.position, destination.position);
	}
	
	// Update is called once per frame
	void Update () {
		path_location = Vector3.Distance(gameObject.transform.position, gameObject.transform.parent.position);
     	path_percent_complete = path_location / path_length * 100;

		if (path_percent_complete < SCORING_ZONE && !isResolving) {
			// Drop in the center
			if(gameObject)
			{
				iTween.StopByName("signMove" + player_id);
				iTween.MoveTo(gameObject, iTween.Hash ("y", 0,
				                                       "x", 0,
				                                       "time", .5,
				                                       "islocal", true,
				                                       "easeType", "easeInBack",
				                                       "name", "signBounce" + player_id,
				                                       "oncomplete", "EnterMiddleComplete",
				                                       "oncompletetarget", gameObject));
			}
			isResolving = true;
			isMeterCharging = false;

			main.Resolve(this);

		}
		if (path_percent_complete < COMBAT_ZONE && !isResolving && !isMeterCharging) {
			isMeterCharging = true;
			iTween.RotateAdd(gameObject, iTween.Hash("y",180, 
			                                         "easeType", "easeInBack",
			                                         "oncomplete","RotateComplete",
			                                         "speed", 500,
			                                         "onompletetarget", gameObject) );
		}

		if (path_percent_complete < COMBAT_ZONE && path_percent_complete < SCORING_ZONE) {
			
		}

		// Move the sprite along the path.
		dist = Vector3.Distance (origin.position, destination.position);

		// Stun multiplier


		if (dist > 0) {
			Vector3 pointA = origin.position;
			Vector3 pointB = destination.position;
			
			Vector3 pointAlongLine = ((Time.deltaTime * move_speed) / stun_multiplier)  * Vector3.Normalize(pointB - pointA) + pointA;
			gameObject.transform.position = pointAlongLine;
		}
	}

	public void EnterMiddleComplete () {
		Debug.Log ("Made it to the middle");
		isScoring = true;
	}

	public void RotateComplete () {
		isVisible = true;
	}

	public void OnDestroy() {
	
	}

	public void DestroySequenceStart() {	
		gameObject.particleSystem.Play ();
		iTween.ScaleBy(gameObject,
		            iTween.Hash("amount", new Vector3(-0.5f,0,0),
		            "time", .5f,
                    "oncomplete","DestroySequenceComplete",
                    "onompletetarget", gameObject) );
	}

	private void DestroySequenceComplete() {
		iTween.Stop (gameObject);
		DestroyObject (gameObject);
	}

	public void Initialize(int in_type, int in_player_id)
	{
//		Debug.Log("Player Id "+in_player_id);
		player_id = in_player_id;
		if (player_id == 1) gameObject.GetComponent<SpriteRenderer>().color = new Color(.963F, 0.428F, 0.428F, 1F);
		if (player_id == 2) gameObject.GetComponent<SpriteRenderer>().color = new Color(0.3F, 0.950F, 0.711F, 1F);
		if (player_id == 3) gameObject.GetComponent<SpriteRenderer>().color = new Color(0.591F, 0.861F, 0.961F, 1F);
		type = in_type;
		isVisible = false;

	}

// OPERATIONS

	private void UpdateSprite(int in_type)
	{
//		Debug.Log(in_type);
//		Debug.Log(spriteNames[in_type]);
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
//			Debug.Log ("Is Visible: " + isVisible);
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

	public bool isResolving 
	{
		get {return m_resolving;}
		set {m_resolving = value;}
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
