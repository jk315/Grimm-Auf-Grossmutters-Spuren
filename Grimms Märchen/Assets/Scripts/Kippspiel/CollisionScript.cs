using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionScript : MonoBehaviour {
    private bool aufBoden = false;
    private int aufgekommen = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("COLLISION " + collision.gameObject.name);
        if (collision.gameObject.name == "Ball" && aufgekommen == 0)
        {
            aufgekommen = 1;
            aufBoden = true;
            Debug.Log("Kugel jetzt auf boden " + aufBoden);
        }
        
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        if (aufBoden)
        {
            Debug.Log("COLLISION " + collisionInfo.gameObject.name);
            Application.LoadLevel(Application.loadedLevel);
        }
        
    }

}
