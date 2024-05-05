using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// 食物的运动
public class DraggableFood : MonoBehaviour
{

    public GameObject ghostPrefab; // 虚影的预制体
    // public GameObject slot; // 槽位预制体游戏对象
    private GameObject ghost; // 虚影的实例
    private bool isDragging = false; // 是否正在拖拽
    private bool isLocked = false;// 是否锁定？-可否被拖动

    public bool isMoving = true;// 是否在随着传送带运动？

    public bool canDestroy = true;// 可否销毁

    public GameObject introTextBox; //食物信息界面对象
    public Canvas targetCanvas;

    public FoodProps foodProps;
    public string foodKey;

    private void Start()
    {
        targetCanvas.worldCamera = Camera.main;
    }

    void Update()
    {
        if(!isLocked)
        {
            if (isDragging)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                ghost.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
            }

            // 按下鼠标左键时
            if (Input.GetMouseButtonDown(0))
            {
                if (IsMouseOverFood())
                {
                    isDragging = true;
                    CreateGhost();
                }
            }

            // 抬起鼠标左键时
            if (Input.GetMouseButtonUp(0))
            {
                
                if (isDragging)
                {
                    isDragging = false;
                    
                    // 如果虚影在【槽位】->【餐盘】区域, 检测空的槽位，若有，则将源食物移动到第一个找到的空槽位中，并锁定
                    if(IsGhostinPlate())
                    {   
                        MoveFoodintoSlot();
                        
                    }
                    
                    Destroy(ghost); // 无论是否重叠，松开鼠标都消除虚影
                }
            }
        }

        introTextBox.SetActive(IsMouseOverFood());

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

    
    void CreateGhost()
    {
        // 创建虚影

        ghost = Instantiate(gameObject, transform.position, Quaternion.identity);
        ghost.GetComponent<FoodProps>().thisFoodKey = foodKey;
        ghost.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // 设置透明度为0.5
    }

    // bool IsGhostinSlots()
    // {
    //     // 判断食物虚影是否在【槽位】区域内

    //     GameObject[] slots = GameObject.FindGameObjectsWithTag("slot");

    //     foreach (GameObject slot in slots)
    //     {
    //         Debug.Log("_checked_");
    //         Collider2D ghostCollider = ghost.GetComponent<BoxCollider2D>();
    //         Collider2D slotCollider = slot.GetComponent<BoxCollider2D>();

    //         //Debug.Log("print:" + ghostCollider.IsTouching(slotCollider));

    //         if (ghostCollider.IsTouching(slotCollider))
    //         {
    //             Debug.Log("_moved_");
    //             isMoving = false;// 解除运动状态，可被移动
    //             gameObject.transform.position = slot.transform.position;

    //             return true;
    //         }
                
    //     }
    //     return false;
    // }

    bool IsGhostinPlate()
    {
        // 判断食物虚影是否在【餐盘】区域内

        GameObject plate = GameObject.FindGameObjectWithTag("plate");

        Collider2D ghostCollider = ghost.GetComponent<BoxCollider2D>();
        Collider2D plateCollider = plate.GetComponent<BoxCollider2D>();

        if(ghostCollider.IsTouching(plateCollider))
        {
            return true;
        }
        
        return false;
    }

    void MoveFoodintoSlot()
    {
        GameObject plate = GameObject.FindGameObjectWithTag("plate");
        GameObject firstEmptySlot = plate.GetComponent<PlateManager>().EmptySlot();

        if(firstEmptySlot != null)
        {
            // 检查餐盘是否有空槽位，若有，则将虚影的源食物移动到找到的第一个空的槽位中
            gameObject.transform.position = firstEmptySlot.transform.position;
            isMoving = false;// 解除运动状态，可被变换位置
            isLocked = true;// 锁定位置，不能被拖动
            canDestroy = false;// 不被传送带摧毁
            
            PlateManager plateManager = FindObjectOfType<PlateManager>();
            plateManager.foodKeyList.Add(foodKey);
        }  
    }
}
