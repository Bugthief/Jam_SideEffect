using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateManager : MonoBehaviour
{
    public GameObject slot_1;
    public GameObject slot_2;
    public bool slotEmpty_1; // 槽位状态，用于记录每个槽位是否为空闲
    public bool slotEmpty_2;

    public List<string> foodKeyList;
    public FoodManager foodManager;

    private void Start()
    {
        // 初始化槽位状态数组，默认所有槽位都为空闲
        slotEmpty_1 = true;
        slotEmpty_2 = true;
        foodKeyList = new List<string>();
    }

    private void Update()
    {
        if (!slotEmpty_1 && !slotEmpty_2 && !foodManager.isEating)
        {
            foodManager.EatFoodList(foodKeyList);
        }
    }


    // 检查空的槽位，并返回对应的对象
    public GameObject EmptySlot()
    {
        if (slotEmpty_1 && slotEmpty_2)
        {
            slotEmpty_1 = false;
            print("进入1号槽");
            return slot_1;
        }

        if (!slotEmpty_1 && slotEmpty_2)
        {
            slotEmpty_2 = false;
            print("进入2号槽");
            return slot_2;
        }

        if (slotEmpty_1 && !slotEmpty_2)
        {
            slotEmpty_1 = false;
            print("进入1号槽");
            return slot_1;
        }
        print("餐盘满了！");
        return null;
    }

}
