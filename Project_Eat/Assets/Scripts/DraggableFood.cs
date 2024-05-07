using UnityEngine;

// 食物的运动
public class DraggableFood : MonoBehaviour
{
    // 盲盒食物 



    // public GameObject ghostPrefab; // 虚影的预制体
    // public GameObject slot; // 槽位预制体游戏对象
    public GameObject ghost; // 虚影的实例
    public bool isDragging = false; // 是否正在拖拽
    public bool isLocked = false;// 是否锁定？-可否被拖动

    public bool isMoving = true;// 是否在随着传送带运动？

    public bool canDestroy = true;// 可否销毁

    public GameObject introTextBox; //食物信息界面对象
    public Canvas targetCanvas;

    public FoodProps foodProps;
    public string foodKey;


    private void Start()
    {
        targetCanvas.worldCamera = Camera.main;
        introTextBox.SetActive(false);

        if (GameObject.FindGameObjectWithTag("parent_belting") != null)
            gameObject.transform.parent = GameObject.FindGameObjectWithTag("parent_belting").transform;
    }

    void Update()
    {
        if (!isLocked)
        {
            if (isDragging)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (ghost != null)
                {
                    ghost.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0f);
                }
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
                    if (IsGhostinPlate())
                    {
                        MoveFoodintoSlot();

                    }

                    Destroy(ghost); // 无论是否重叠，松开鼠标都消除虚影
                }
            }


        }

        if (gameObject.tag == "ghost")
        {
            introTextBox.SetActive(IsMouseOverFood());
        }
        else
        {
            if (ghost == null)
            {
                introTextBox.SetActive(IsMouseOverFood());
            }
            else
            {
                introTextBox.SetActive(false);
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


        //if (colliders.Length > 0)
        //{
        //    if (colliders[^1].gameObject == gameObject)
        //    {
        //        return true;
        //    }
        //}

        return false;
    }


    void CreateGhost()
    {
        // 创建虚影
        ghost = Instantiate(gameObject, transform.position, Quaternion.identity);
        ghost.GetComponent<FoodProps>().thisFoodKey = foodKey;
        ghost.tag = "ghost";
        ghost.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f); // 设置透明度为0.5
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

        if (ghost == null || plate == null) return false;

        Collider2D ghostCollider = ghost.GetComponent<BoxCollider2D>();
        Collider2D plateCollider = plate.GetComponent<BoxCollider2D>();

        if (ghostCollider.IsTouching(plateCollider))
        {
            return true;
        }

        return false;
    }

    void MoveFoodintoSlot()
    {
        GameObject plate = GameObject.FindGameObjectWithTag("plate");
        GameObject firstEmptySlot = plate.GetComponent<PlateManager>().EmptySlot();

        if (firstEmptySlot != null)
        {
            // 检查餐盘是否有空槽位，若有，则将虚影的源食物移动到找到的第一个空的槽位中
            gameObject.transform.position = firstEmptySlot.transform.position;
            isMoving = false;// 解除运动状态，可被变换位置
            isLocked = true;// 锁定位置，不能被拖动
            canDestroy = false;// 不被传送带摧毁

            PlateManager plateManager = FindObjectOfType<PlateManager>();
            plateManager.foodKeyList.Add(foodKey);

            if (plateManager.food_1 == null)
            {
                plateManager.food_1 = gameObject;
            }
            else
            {
                plateManager.food_2 = gameObject;
            }
        }
    }


}
