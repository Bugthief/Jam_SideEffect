using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableFood : MonoBehaviour
{
    public GameObject ghostPrefab; // 虚影的预制体
    // public GameObject slot; // 槽位预制体游戏对象
    private GameObject ghost; // 虚影的实例
    private bool isDragging = false; // 是否正在拖拽
    private bool isLocked = false;// 是否锁定？-可否被拖动

    void Update()
    {
        if(!isLocked)
        {
            if (isDragging)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ghost.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (IsMouseOverFood())
                {
                    isDragging = true;
                    CreateGhost();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (isDragging)
                {
                    isDragging = false;

                    if(IsGhostinSlots())
                    {
                        // gameObject.transform.position = slot.transform.position;
                        isLocked = true;
                    }
                    
                    Destroy(ghost); // 无论是否重叠，松开鼠标都消除虚影
                }
            }
        }
        
    }

    // 检测鼠标是否在食物上
    bool IsMouseOverFood()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] colliders = Physics2D.OverlapPointAll(new Vector2(mousePosition.x, mousePosition.y));
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == gameObject)
            {
                return true;
            }
        }
        return false;
    }


    bool IsGhostinSlots()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("slot");
        foreach (GameObject slot in slots)
        {
            Collider2D ghostCollider = ghost.GetComponent<BoxCollider2D>();
            Collider2D slotCollider = slot.GetComponent<BoxCollider2D>();
            if (ghostCollider.IsTouching(slotCollider))
            {
                gameObject.transform.position = slot.transform.position;

                return true;
            }
                
        }
        return false;
    }

    // 创建虚影
    void CreateGhost()
    {
        ghost = Instantiate(gameObject, transform.position, Quaternion.identity);
        ghost.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // 设置透明度为0.5
    }

}

// bool IsGhostOverSlots()
    // {
    //     Collider2D ghostCollider = ghost.GetComponent<Collider2D>();
    //     if(ghostCollider.IsTouching(slot.GetComponent<Collider2D>()))
    //     {
    //         Debug.Log("Yes!");
    //         return true;
    //     }
    //     else
    //     {
    //         Debug.Log("No...");
    //         return false;
    //     }
        
        
    // }