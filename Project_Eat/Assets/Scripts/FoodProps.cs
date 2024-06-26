using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Food;

// 食物的key, 属性
public class FoodProps : MonoBehaviour
{
    public string thisFoodKey;
    public TMP_Text foodNameText;
    public TMP_Text foodDescriptionText;
    public TMP_Text foodDetailsText;
    public AssignFoodImage assignFoodImage;

    public DraggableFood draggableFood;

    void Start()
    {
        GetRandomFoodKey();
        UpdateHoverUI();

        assignFoodImage = FindObjectOfType<AssignFoodImage>();
    }

    // 从foodKeyList中随机获取一个key，并打印
    void GetRandomFoodKey()
    {
        if (gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a == 0.5f) return;

        // 检查列表是否为空
        if (GameManager.Instance.FoodKeyList != null && GameManager.Instance.FoodKeyList.Count > 0)
        {
            // 生成一个随机索引
            int randomIndex = Random.Range(0, GameManager.Instance.FoodKeyList.Count);

            // 获取随机索引处的元素
            thisFoodKey = GameManager.Instance.FoodKeyList[randomIndex];

            draggableFood.foodKey = thisFoodKey;
            assignFoodImage.LoadFullFoodImage(thisFoodKey);

            // 打印所选的随机字符串
            //Debug.Log("Random Food is: " + thisFoodKey);
        }
        else
        {
            Debug.LogError("List is null or empty!");
        }
    }

    // 输出foodKeyList
    void printFoodKeyList()
    {

        // 检查列表是否为空
        if (GameManager.Instance.FoodKeyList != null && GameManager.Instance.FoodKeyList.Count > 0)
        {
            // 遍历列表中的每个元素，并打印它们
            foreach (string item in GameManager.Instance.FoodKeyList)
            {
                Debug.Log(item);
            }
        }
        else
        {
            Debug.LogError("List is null or empty!");
        }
    }

    void UpdateHoverUI()
    {
        Food thisFood = GameManager.Instance.FoodDictionary[thisFoodKey];

        string sideEffects = null;

        int count = thisFood.SideEffectNameList.Count;
        for (int i = 0; i < count; i++)
        {
            SideEffectTypeEnum sideEffectTypeEnum = thisFood.SideEffectNameList[i];

            sideEffects += GameManager.Instance.SideEffectDictionary[sideEffectTypeEnum].SideEffectName;

            if (i < count - 1)
            {
                sideEffects += "，";
            }
        }

        foodNameText.text = thisFood.FoodName;
        foodDescriptionText.text = thisFood.FoodDescription;

        foodDetailsText.text =
            "分数： " + thisFood.FoodPoint + "\n" +
            "时间： " + thisFood.FoodTime + "\n";

        if (count != 0)
        {
            foodDetailsText.text += "副作用： " + sideEffects;
        }
    }
}
