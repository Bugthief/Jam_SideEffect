using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Food
{
    public string FoodKey { get; private set; }
    public string FoodName { get; private set; }
    public List<FoodTypeEnum> FoodTypeList { get; private set; }
    public string FoodDescription { get; private set; }

    public float FoodPoint { get; private set; }
    public float FoodTime { get; private set; }
    public List<SideEffectTypeEnum> SideEffectNameList { get; private set; }

    public Food()
    {
        FoodTypeList = new List<FoodTypeEnum>(); // Instantiate FoodTypeList
        SideEffectNameList = new List<SideEffectTypeEnum>(); // Instantiate SideEffectNameList
    }

    public void SetFoodKey(string foodKey)
    {
        FoodKey = foodKey;
    }

    public void SetFoodName(string foodName)
    {
        FoodName = foodName;
    }

    public void AddFoodType(FoodTypeEnum foodType)
    {
        FoodTypeList.Add(foodType);
    }

    public void SetFoodDescripton(string fooddescription)
    {
        FoodDescription = fooddescription;
    }

    public void SetFoodPoint(float foodPoint)
    {
        FoodPoint = foodPoint;
    }

    public void SetFoodTime(float foodTime)
    {
        FoodTime = foodTime;
    }

    public void AddSideEffect(SideEffectTypeEnum sideEffect)
    {
        SideEffectNameList.Add(sideEffect);
    }

    public enum FoodTypeEnum
    {
        Meat,
        Vegetables,
        Soup,
        Drink,
        Strange,
        Main,
        Dessert
    }

    public enum SideEffectTypeEnum
    {
        InLove,
        Elder,
        ThrowUp,
        Anger,
        CoffeePlus,
        Excited,
        ImSick,
        Vegan,
        Toxic,
        Continuing6,
        Pungent,
        Bloating,
        Burning,
        Mood,
        Numb,
        Pica,
        Shining
    }
}
