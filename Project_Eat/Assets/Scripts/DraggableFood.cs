using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFood : MonoBehaviour
{
    // Transform parentAfterDrag;
    
    private bool isDragging = false;
    private Vector3 offset;

    void Update()
    {
        if (isDragging)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //     Debug.Log("begin");
    //     parentAfterDrag = transform.parent;
    //     transform.SetParent(transform.root);
    //     transform.SetAsLastSibling();
    // }

    // public void OnDrag(PointerEventData eventData)
    // {
    //     Debug.Log("ing");
    //     transform.position = Input.mousePosition;
    // }

    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     Debug.Log("end");
    //     transform.SetParent(parentAfterDrag);
    // }

}
