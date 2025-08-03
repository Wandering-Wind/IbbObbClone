using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityInverter : MonoBehaviour
{
    bool onGround = false;
    // Update is called once per frame
    void Update()
    {
        if(onGround == true)
        {
            //basically inverts gravity. 
            Physics2D.gravity = -Physics2D.gravity;
        }
    }
}
