using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float moveDistance = 2f;
    public bool moveUp = false;
    public bool moveSide = true;

    Vector3 startingPos;

    Transform trans;

	// Use this for initialization
	void Start () {
        trans = GetComponent<Transform>();
        startingPos = trans.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (moveUp)
        {
            trans.position = new Vector3(startingPos.x, startingPos.y + Mathf.PingPong(Time.time, moveDistance), startingPos.z);
        }
        if (moveSide)
        {
            trans.position = new Vector3(startingPos.x + Mathf.PingPong(Time.time, moveDistance), startingPos.y, startingPos.z);
        }
        if (moveSide && moveUp)
        {
            trans.position = new Vector3(startingPos.x + Mathf.PingPong(Time.time, moveDistance), startingPos.y + Mathf.PingPong(Time.time, moveDistance), startingPos.z);
        }
    }
}
