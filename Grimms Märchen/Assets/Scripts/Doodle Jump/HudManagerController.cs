using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManagerController : MonoBehaviour {

    public Text scoreLabel;

	// Use this for initialization
	void Start () {
        RefreshScore();
    }

    public void RefreshScore()
    {
        scoreLabel.text = "Score:" + GameManager.instance.score;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        RefreshScore();
	}
}
