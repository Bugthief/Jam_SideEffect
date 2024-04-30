using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FoodLoader : MonoBehaviour
{
    public static TextAsset csvFile;

    public static Dictionary<string, Food> BuildFoodDictionary()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("CSV/Food");

        Dictionary<string, Food> foodDictionary = new Dictionary<string, Food>();

        string csvText = csvFile.text;
        string[] lines = csvText.Split('\n');


        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrEmpty(line))
                continue;

            string[] fields = line.Split(',');


            string foodKey = fields[0];
            string foodName = fields[1];
            string[] foodTypeList = fields[2].Split(';');
            string foodDescription = fields[3];
            float.TryParse(fields[4], out float foodPoint);
            float.TryParse(fields[5], out float foodTime);
            string[] sideEffectNameList = fields[6].Split(';');


            Food food = new Food();
            food.SetFoodKey(foodKey);
            food.SetFoodName(foodName);
            foreach (string foodType in foodTypeList)
            {
                if (Enum.TryParse(foodType, out Food.FoodTypeEnum foodTypeEnum))
                {
                    food.AddFoodType(foodTypeEnum);
                }
            }
            food.SetFoodDescripton(foodDescription);
            food.SetFoodPoint(foodPoint);
            food.SetFoodTime(foodTime);
            foreach (string sideEffectName in sideEffectNameList)
            {
                if (Enum.TryParse(sideEffectName, out Food.SideEffectTypeEnum sideEffectTypeEnum))
                {
                    food.AddSideEffect(sideEffectTypeEnum);
                }
            }

            foodDictionary.Add(foodKey, food);
        }

        return foodDictionary;
    }

}
