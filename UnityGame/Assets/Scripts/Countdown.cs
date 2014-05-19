using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	float speed = 1;

	public SpriteRenderer three;
	public SpriteRenderer two;
	public SpriteRenderer one;
	public SpriteRenderer go;

	void Awake () {
		three.enabled = false;
		two.enabled = false;
		one.enabled = false;
		go.enabled = false;

		three.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
		two.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
		one.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
		go.gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
		
	}

	// Use this for initialization
	void Start () {
		three.enabled = true;
		iTween.ScaleTo(three.gameObject, iTween.Hash(
			"scale", new Vector3(1f, 1f, 1f),
			"time", speed,
			"onComplete", "PlayTwo",
			"onCompleteTarget", gameObject,
			"easeType", "easeOutBounce"
			));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void PlayTwo()
	{
		three.enabled = false;
		two.enabled = true;
		iTween.ScaleTo(two.gameObject, iTween.Hash(
			"scale", new Vector3(1f, 1f, 1f),
			"time", speed,
			"onComplete", "PlayOne",
			"onCompleteTarget", gameObject,
			"easeType", "easeOutBounce"
			));
	}

	void PlayOne()
	{
		two.enabled = false;
		one.enabled = true;
		iTween.ScaleTo(one.gameObject, iTween.Hash(
			"scale", new Vector3(1f, 1f, 1f),
			"time", speed,
			"onComplete", "PlayGo",
			"onCompleteTarget", gameObject,
			"easeType", "easeOutBounce"
			));
	}

	void PlayGo()
	{
		one.enabled = false;
		go.enabled = true;
		iTween.ScaleTo(go.gameObject, iTween.Hash(
			"scale", new Vector3(1f, 1f, 1f),
			"time", speed,
			"easeType", "easeOutBounce",
			"onComplete", "StartGame",
			"onCompleteTarget", gameObject
			));
	}

	void StartGame()
	{
		Application.LoadLevel (2);
	}
}
