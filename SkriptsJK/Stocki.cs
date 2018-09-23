using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stocki : MonoBehaviour {

	float speed = 5f;
	public bool isBump1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (isBump1) {
			if (Input.GetKey ("up") && transform.position.y < 5.3) {
				transform.Translate (Vector3.up * speed * Time.deltaTime);
			}

			if (Input.GetKey ("down") && transform.position.y > -3.1) {
				transform.Translate (Vector3.down * speed * Time.deltaTime);
			}
		} else {
			GameObject ballGameObject = GameObject.Find ("Ball");
		}
		/*else {
			if (Input.GetKey ("w") && transform.position.y < 5.3) {
				transform.Translate (Vector3.up * speed * Time.deltaTime);
			}

			if (Input.GetKey ("s")&& transform.position.y > -3.1) {
				transform.Translate (Vector3.down * speed * Time.deltaTime);
			}
		}*/
	}
}
