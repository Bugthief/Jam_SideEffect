using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class played : MonoBehaviour
{
    public void isPlayed()
    {
        gameObject.GetComponent<Animator>().SetBool("played", true);
    }
}
