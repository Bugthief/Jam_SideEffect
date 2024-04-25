using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralButton : MonoBehaviour
{
    public void GeneralOnClick(GameObject[] objToClose, GameObject[] objToOpen)
    {
        foreach (GameObject obj in objToClose) obj.SetActive(false);

        foreach (GameObject obj in objToOpen) obj.SetActive(true);

    }
}
