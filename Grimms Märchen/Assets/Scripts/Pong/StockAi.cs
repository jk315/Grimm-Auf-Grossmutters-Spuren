using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockAi : MonoBehaviour {

	public Transform ball;

	[Range(0, 1)]
	public float schnelligkeit;

		void Start()
		{
			
		}

		void Update ()
		{
		Vector3 newPos = transform.position;
		newPos.y = Mathf.Lerp(transform.position.y, ball.position.y,schnelligkeit);
			if (transform.position.y < 5.3 && transform.position.y > -3.1) 
			{
			transform.position = newPos;
			}
		}
	}
