using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float speed = 6f;       //Geschwindigkeit
    public float maxSpeed = 4f;
    public float jumpSpeed = 8f;    //Sprunghöhe
    Rigidbody myRigidbody;          //phys. Regelung auf das Model/Spieler

    bool isOnFloor;
    bool pressedJump = false;       //Variable, dass es kein Mehrfachspringen hintereinander gibt



    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();    //Initialisierung v. Rigidbody
    }

    // weniger Aufrufe per Frame
    void FixedUpdate()
    {
        RunHandler();
        JumpHandler();
    }


    // Spieler bewegen
    void RunHandler()
    {
        float moveVertical = Input.GetAxis("Vertical");     //Eingabe der Tastatur (hoch/runter oder s/w)
        float moveHorizontal = Input.GetAxis("Horizontal"); //Eingabe der Tastatur (links/rechts oder a/d)

        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);   // 0f = Sprunghöhe; soll ausgeschlossen werden

        if (myRigidbody.velocity.magnitude > maxSpeed)
        {
            // Model kann nicht schneller wie die angegebene maxSpeed
            myRigidbody.velocity = myRigidbody.velocity.normalized * maxSpeed;
        }
        else
        {
            myRigidbody.AddForce(movement * speed); // laufe dahin...
        }
        //myRigidbody.MovePosition(transform.position + (movement * speed));
    }


    // Spieler springt
    void JumpHandler()
    {
        float jumpAxis = Input.GetAxis("Jump");     //Sprungachse Y
        if (jumpAxis > 0f)
        { //Springen erst wenn Sprungachse über 0
            if (isOnFloor && !pressedJump)
            { // Springen, wenn Spieler Boden unter sich hat 
                //print("Jumping");
                //myRigidbody.AddForce(jumpVector);    //Kraft
                isOnFloor = false;
                pressedJump = true;
                Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);    //Richtung
                myRigidbody.velocity = myRigidbody.velocity + jumpVector;
            }
        }
        else
        {
            pressedJump = false; //wieder zum Ausgangspunkt
        }
    }

    //Initialisierung/Abfrage  mit dem #tag "Floor"
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            isOnFloor = true;
        }

        else if (col.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("Game Over");
        }


    }

    private void OnTriggerEnter(Collider col)
    {
        //Punktesystem = Coin
        if (col.gameObject.tag == "Coin")
        {
            GameManager.instance.AddScore(1);
            Destroy(col.gameObject);
        }
        
        else if (col.gameObject.tag == "Goal")
        {
            SceneManager.LoadScene("Ende");
            //GameManager.instance.NextLevel();
        }
        
    }
}

