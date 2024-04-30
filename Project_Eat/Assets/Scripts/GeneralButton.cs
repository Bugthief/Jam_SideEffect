using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralButton : MonoBehaviour
{
    public void GeneralOnClick(GameObject[] objToClose, GameObject[] objToOpen)
    {
        if (objToClose != null)
        {
            foreach (GameObject obj in objToClose)
            {
                obj.SetActive(false);
            }
        }

        if (objToOpen != null)
        {
            foreach (GameObject obj in objToOpen)
            {
                obj.SetActive(true);
            }
        }
    }
}
