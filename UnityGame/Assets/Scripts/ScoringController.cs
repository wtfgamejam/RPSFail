using UnityEngine;
using System.Collections;

public class ScoringController : MonoBehaviour {
	
	private int m_score;
	
	// Use this for initialization
	void Start () {
		renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, score));
	}

	public void IncreaseScore () {
		if (score <= 100) {
			score += 2;
			if ( score > 0 ){
				renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, score)); 
			}
		}
	}
	
	public void DecreaseScore () {
		if (score > 0) {
			score -= 1;
			renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(100, 0, score)); 
		}
	}
	
	public int score
	{
		get {return m_score;}
		set {m_score = value;}
	}
}
