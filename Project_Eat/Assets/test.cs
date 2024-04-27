using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("slot"))
        {
            Debug.Log("1");
        }

        
    }
}
