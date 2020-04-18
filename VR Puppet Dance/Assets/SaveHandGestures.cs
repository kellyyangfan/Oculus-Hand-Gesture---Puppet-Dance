using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHandGestures : MonoBehaviour
{
    //public GameObject RightHand;
    //public GameObject LeftHand;
    public GameObject GestureDetector;
    public GameObject DebugText;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("here");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c)
    {
        Debug.Log("collide");

        //GestureDetector.GetComponent<GestureDetection>().Save();

        if (c.gameObject.tag == "LeftHand") {
            DebugText.GetComponent<TextMesh>().text = "Left Hand Press Button!\nSaving Right Hand Gesture...";
            
            //setting Right Hand Gesture
            GestureDetector.GetComponent<GestureDetection>().Save();
        }
        if (c.gameObject.tag == "RightHand")
        {
            DebugText.GetComponent<TextMesh>().text = "Press the button with Left Hand \nto save Right Hand gesture ";
        }
    }
}
