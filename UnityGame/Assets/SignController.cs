using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignController : MonoBehaviour {

	public const int BASE_ID = 0;
	public const int ROCK_ID = 1;
	public const int PAPER_ID = 2;
	public const int SCISSORS_ID = 3;
	public const string BASE_NAME = "base";
	public const string ROCK_NAME = "rock";
	public const string PAPER_NAME = "paper";
	public const string SCISSORS_NAME = "scissors";

	private float m_speed;
	private int m_type;
	private bool m_scoring;
	private bool m_visible;

	public Dictionary<int, string> spriteNames;

// HOUSEKEEPING
	void Awake() {
		spriteNames = new Dictionary<int, string>();
		spriteNames.Add(BASE_ID, BASE_NAME);
		spriteNames.Add(ROCK_ID, ROCK_NAME);
		spriteNames.Add(PAPER_ID, PAPER_NAME);
		spriteNames.Add(SCISSORS_ID, SCISSORS_NAME);

		isScoring = false;
		isVisible = false;
	}

	// Use this for initialization
	void Start () {
		Initialize(ROCK_ID);
		iTween.MoveTo (gameObject, iTween.Hash ("y", 0,
		                                        "x", 0,
		                                        "time", 10,
		                                        "islocal", true,
		                                        "easeType", "linear"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Initialize(int in_type)
	{
		type = in_type;
		isVisible = true;
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
			// Change sprite here, based on visible property
			if(isVisible) UpdateSprite(m_type);
		}
	}
	
	public bool isScoring 
	{
		get {return m_scoring;}
		set {m_scoring = value;}
	}
	
	public bool isVisible 
	{
		get {return m_visible;}
		set {
			m_visible = value;
			// Change sprite to type if visible
			UpdateSprite(m_type);
		}
	}
}
