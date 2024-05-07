using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizedCursor : MonoBehaviour
{
    public Texture2D cursorArrow;
    public Texture2D toPick;
    public Texture2D drag;

    public bool isFood = false;// 该脚本附加到食物上吗？

    void Start()
    {
        
        // Cursor.visible = false;
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    void OnMouseEnter()
    {
        if (isFood)
        {
            if (!gameObject.GetComponent<DraggableFood>().isLocked)
            {
                Cursor.SetCursor(toPick, Vector2.zero, CursorMode.ForceSoftware);
            }
        }

    }

    void OnMouseDrag()
    {
        if (isFood)
        {
            if (!gameObject.GetComponent<DraggableFood>().isLocked)
            {
                Cursor.SetCursor(drag, Vector2.zero, CursorMode.ForceSoftware);
            }
        }
        
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

}
