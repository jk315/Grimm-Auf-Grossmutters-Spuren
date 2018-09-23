using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rigids : MonoBehaviour {
    
    // Use this for initialization
    void Start () {

        Camera m = Camera.main;
         m.transform.Translate(0.37f, 5, -4);
        m.transform.eulerAngles = new Vector3(50.997f, 0, 0);
       
        Rigidbody rg = m.GetComponent<Rigidbody>();
        rg.useGravity = false;
        rg.isKinematic = true;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
