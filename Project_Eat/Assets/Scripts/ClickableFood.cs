using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableFood : MonoBehaviour
{
    public GameObject ghostPrefab; // 虚影的预制体
    private GameObject ghost; // 虚影的实例
    public bool isGhostCreated = false;// 是否已创建虚影


    private bool isDragging = false; // 是否正在拖拽？
    private bool isLocked = false;// 是否锁定？-可否被拖动

    public bool isMoving = true;// 是否在随着传送带运动？

    public bool canDestroy = true;// 可否销毁？

    void Update()
    {
        if(!isLocked)
        {
            // 按下鼠标左键时
            if (Input.GetMouseButtonDown(0))
            {
                // 如果点击的是食物，且食物未在选中状态
                if (!isDragging && IsMouseOverFood())
                {
                    isDragging = true;
                    CreateGhost();
                    print("222");
                }

                // 如果虚影在【槽位】->【餐盘】区域, 检测空的槽位，若有，则将源食物移动到第一个找到的空槽位中，并锁定
                if(ghost != null && IsGhostinPlate())
                {   
                    print("444");
                    moveFoodintoSlot();
                    DestroyGhost();// 消除虚影
                }
            }

            
            // 按下鼠标右键时
            if (Input.GetMouseButtonDown(1))
            {
                if(isDragging)
                {
                    DestroyGhost(); // 消除虚影
                    
                }
                
            }

            // 如果已经在拖动食物状态，虚影跟随鼠标
            if (ghost != null && isDragging)
            {
                GhostFollowMouse();
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

    
    void CreateGhost()
    {
        // 创建虚影
        if(!isGhostCreated)
        {
            ghost = Instantiate(gameObject, transform.position, Quaternion.identity);
            ghost.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // 设置透明度为0.5
            ghost.GetComponent<ClickableFood>().isGhostCreated = true;

            // 添加阻止鼠标事件的脚本
            ghost.AddComponent<BlockMouseEvents>();
        }
        
    }

    public void DestroyGhost()
    {
        // 销毁虚影
        if(isGhostCreated)
        {
            isDragging = false;
            isGhostCreated = false;
            Destroy(ghost); // 消除虚影
        }
        
    }

    void GhostFollowMouse()
    {
        // 虚影跟随鼠标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ghost.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);

    }

    bool IsGhostinPlate()
    {
        // 判断食物虚影是否在【餐盘】区域内

        GameObject plate = GameObject.FindGameObjectWithTag("plate");

        print("000");
        Collider2D ghostCollider = ghost.GetComponent<BoxCollider2D>();
        print("001");
        Collider2D plateCollider = plate.GetComponent<BoxCollider2D>();

        if(ghostCollider.IsTouching(plateCollider))
        {
            return true;
        }
        
        return false;
    }

    void moveFoodintoSlot()
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
            
        }
        
        
    }
}
