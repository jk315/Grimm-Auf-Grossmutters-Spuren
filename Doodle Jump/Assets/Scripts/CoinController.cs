using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    public float rotationspeed = 150f;
	
	// Münze drehen lassen
	void Update () {
        float angel = rotationspeed * Time.deltaTime;
        transform.Rotate(Vector3.up * angel, Space.World); // nach oben
    }
}
