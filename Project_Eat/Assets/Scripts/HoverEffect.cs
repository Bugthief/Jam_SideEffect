using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffect : MonoBehaviour
{
    public GameObject[] objToShow;

    private void Update()
    {
        if (IsPointerOverUI(this.gameObject))
        {
            foreach (GameObject obj in objToShow) obj.SetActive(true);
        }else{
            foreach (GameObject obj in objToShow) obj.SetActive(false); // 新加，悬浮时显示，此外不显示
        }
    }


    public static bool IsPointerOverUI(GameObject targetUI)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        int targetSortingOrder = GetSortingOrder(targetUI);

        foreach (var result in raycastResults)
        {
            if (result.gameObject == targetUI)
            {
                return true;
            }

            // Check if the current UI element is a TMP_Text component, and if it is, skip it.
            if (result.gameObject.GetComponent<TMP_Text>() != null)
            {
                continue;
            }

            int sortingOrder = GetSortingOrder(result.gameObject);
            if (sortingOrder >= targetSortingOrder)
            {
                //string obscuringUIName = result.gameObject.name;
                //Debug.Log("UI component obscuring the target: " + obscuringUIName);
                break;
            }
        }

        return false;
    }

    private static int GetSortingOrder(GameObject uiObject)
    {
        Canvas canvas = uiObject.GetComponentInParent<Canvas>();
        return canvas ? canvas.sortingOrder : int.MinValue;
    }
}
