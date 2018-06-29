using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Basket : MonoBehaviour {

int score;
public Text Scoretext;

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "gApple"){
			score += 1;
			Destroy(col.gameObject);
		}
		else if (col.gameObject.name == "rApple"){
			score += 3;
			Destroy(col.gameObject);
		} 		
		else if (col.gameObject.name == "rottenApple"){
			score -= 5;
			Destroy(col.gameObject);
		}
		//updateScore();
		checkFinish();

	}

	void updateScore(){
		Scoretext.text = "Score: " + score;
	}

	void checkFinish (){
		if (score == 1){
			Debug.Log("Du hast gewonnen");
		}else if (score == -1){
			Debug.Log("Computer sagt nein");
			//SceneManager.LoadScene(this.scene);
		}

	}


	// Use this for initialization
	void Start () {

		score = 0;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Debug.Log("A pressed");
	    if (this.transform.position.x != -4){
	   	 var offset = new Vector3(-1f, 0f, 0f);
           	 this.transform.position += offset;
	     }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
	    if (this.transform.position.x != 4){
	   	 var offset = new Vector3(1f, 0f, 0f);
           	 this.transform.position += offset;
	     }
        }



		
	}
}
