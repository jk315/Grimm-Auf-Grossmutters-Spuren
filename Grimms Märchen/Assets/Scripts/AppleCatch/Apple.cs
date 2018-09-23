using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {

public GameObject[] apple;
public Transform spawnSpot;
//int randomValue;
int randomSpot;
int randomSpotZ;
int randomBool;
int randomColor;
float lastTime;

	void randomSpawn(){
		//randomValue = Random.Range(0, apple.Length);
		randomSpot = Random.Range(-4, 4);
		randomSpotZ = Random.Range(-4, 4);
		randomColor = Random.Range(0, 10);
		spawnSpot.position = new Vector3(randomSpot, 10, randomSpotZ);

		if (randomColor <=5){
			GameObject green = Instantiate(apple[0], spawnSpot.position, spawnSpot.rotation);
			green.name = "gApple";

		}
		else if (randomColor == 6 || randomColor == 7){
			GameObject red = Instantiate(apple[1], spawnSpot.position, spawnSpot.rotation);
			red.name = "rApple";

		}
		else{
			GameObject rotten = Instantiate(apple[2], spawnSpot.position, spawnSpot.rotation);
			rotten.name = "rottenApple";

		}

	}




	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		 if ((Time.time - lastTime) > 0.5){        
			lastTime = Time.time;
			randomBool = Random.Range(0, 3);

			if(randomBool != 0){
				randomSpawn();
			}
		 }	

	}
}
