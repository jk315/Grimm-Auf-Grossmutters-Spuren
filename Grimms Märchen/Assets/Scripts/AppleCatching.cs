using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class AppleCatching : MonoBehaviour
{

    // Use this for initialization

    // Use this for initialization
    void Start()
    {
      
    }


   void OnMouseDown()
    {
        Debug.Log(" GECKLICKED");
        SceneManager.LoadScene("CatchApples");
    }
}
