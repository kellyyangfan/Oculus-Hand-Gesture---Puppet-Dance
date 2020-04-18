using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfAnim : MonoBehaviour
{
    public GameObject GestureDetector;
    Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Gesture1()
    {
        Anim.SetTrigger("Belly");
    }
    public void Gesture2()
    {
        Anim.SetTrigger("Chicken");
    }
    public void Gesture3()
    {
        Anim.SetTrigger("Idle");
    }

}
