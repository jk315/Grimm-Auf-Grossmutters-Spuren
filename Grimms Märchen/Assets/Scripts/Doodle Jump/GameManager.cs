using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public int score = 0;
    public int highscore = 0;

    public int level = 1;
    public int maxLevel = 2;

    //GameManager bleibt erhalten
	void Awake () {
		if (instance == null)
        {
            instance = this;
        } else if (instance != null)
        {
            Destroy(gameObject);
        }
        //Daten bleiben beim Szenenwechsel erhalten
        DontDestroyOnLoad(gameObject);
	}

    // alter Highscore des Benutzers wird geladen
    private void Start(){
        highscore = PlayerPrefs.GetInt("hightscore");
    }

	
	// Punktesystem einfügen
	public void AddScore (int newScoreValue) {
        score += newScoreValue;
        if (score > highscore) {
            highscore = score;
            // dauerhafte Speicherung der Spielerdaten
            PlayerPrefs.SetInt("hightscore", highscore);
        }
	}

    //Level wechseln (1-5/ danach wieder 1)
    public void NextLevel() {
        if (level < maxLevel)
        {
            level++;
        } else
        {
            level = 1;
        }
        SceneManager.LoadScene("Level" + level);
    }

    //Spielstart/Reset
    public void Reset()
    {
        score = 0;
        level = 1;
        SceneManager.LoadScene("Level" + level);
    }

}
