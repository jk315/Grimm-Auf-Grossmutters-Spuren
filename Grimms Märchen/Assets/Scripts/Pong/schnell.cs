using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class schnell : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale += 0.001f; //sorgt dafür, dass die Zeit schneller verfliegt, und somit der Ball immer schneller wird
	}
}
