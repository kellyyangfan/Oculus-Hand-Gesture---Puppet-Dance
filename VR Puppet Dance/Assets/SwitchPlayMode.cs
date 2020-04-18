using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayMode : MonoBehaviour
{
    public GameObject GestureDetector;
    public GameObject DebugText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision c)
    {
        //GestureDetector.GetComponent<GestureDetection>().playMode = true;
        if (c.gameObject.tag == "LeftHand" || c.gameObject.tag == "RightHand")
        {
            //setting playMode to true
            GestureDetector.GetComponent<GestureDetection>().playMode = true;
        }

    }
}
