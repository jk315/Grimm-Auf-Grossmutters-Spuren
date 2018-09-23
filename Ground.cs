using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {


	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "gApple" || col.gameObject.name == "rApple" || col.gameObject.name == "rottenApple"){
			Destroy(col.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
