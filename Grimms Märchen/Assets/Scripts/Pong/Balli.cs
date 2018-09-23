using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Balli : MonoBehaviour {
	public float force = 500f;
	public int i = 0;
	//public float speed= 5f;


	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().AddForce(Vector3.right * force);

		//float sx = Random.Range (0,2) == 0? -1 : 0;
		//float sy = Random.Range (0,2) == 0? -1 : 0;

		//GetComponent<Rigidbody>().velocity = new Vector3 (speed * sx * sy, 0f);
	}
	
	// Update is called once per frame
	void Update () {
			Application.Quit ();
		//force = force+1;
		//GetComponent<Rigidbody>().AddForce(Vector3.right*force, ForceMode.VelocityChange);

	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.tag == "Wand") {
			SceneManager.LoadScene ("Pong");
		}
		i++;
	}
}
	

		/*if (other.gameObject.tag == "Stock") {

			force = force + 10;
			GetComponent<Rigidbody>().AddForce(Vector3.right*force, ForceMode.VelocityChange);
		}*/


