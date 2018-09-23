using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ColliderPunkt : MonoBehaviour {

    private MazeCreation m;
    public int punkte;
    public bool generateNew = false;
    // Use this for initialization
    void Start () {
        m = new MazeCreation();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("BALL Punktestand " + punkte);
        if (punkte == 100)
        {
            Debug.Log("SPIEL GEWONNEN");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("COLLISION " + collision.gameObject.name);
        if (collision.gameObject.name == "Cube")
        {
            punkte += 10;
            Destroy(collision.gameObject);
            generateNew = true;
            if (punkte == 100) {
                
                Debug.Log("SPIEL GEWONNEN");
                SceneManager.LoadScene("DEINE SCENE");
            }
        }

    }
}
