using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class EatingPlate : MonoBehaviour
{
    public GameObject eating_1;
    public GameObject eating_2;

    public Transform slot_1_transform;
    public Transform slot_2_transform;

    public FoodManager foodManager;

    private Coroutine eatingCoroutine;
    public float eatingTime;

    private void Start()
    {
        foodManager.OnEatingStatusChanged += EatingStatusChanged;
    }

    private void EatingStatusChanged(bool newStatus)
    {
        if (!newStatus)
        {
            Destroy(eating_1);
            Destroy(eating_2);
        }
    }

    public void MoveToEatingPlate(GameObject food_1, GameObject food_2)
    {
        eating_1 = food_1;
        eating_2 = food_2;

        food_1.transform.position = slot_1_transform.position;
        food_2.transform.position = slot_2_transform.position;

        string key1 = eating_1.GetComponent<DraggableFood>().foodKey;
        string key2 = eating_2.GetComponent<DraggableFood>().foodKey;

        eating_1.GetComponent<AssignFoodImage>().LoadFullFoodImage(key1);
        eating_2.GetComponent<AssignFoodImage>().LoadFullFoodImage(key2);


        if (eatingCoroutine != null)
        {
            StopCoroutine(eatingCoroutine);
        }

        eatingCoroutine = StartCoroutine(PerformEating(eatingTime));

        List<string> foodKeyList = new List<string>
        {
            key1,
            key2
        };

        NextPlateInfo nextPlateInfo = FindAnyObjectByType<NextPlateInfo>();
        nextPlateInfo.GenerateNextPlateInfo(foodKeyList);
    }

    public IEnumerator PerformEating(float eatingTime)
    {
        float quarterTime = eatingTime / 4f;

        yield return new WaitForSeconds(quarterTime);
        eating_1.GetComponent<AssignFoodImage>().LoadHalfFoodImage();

        yield return new WaitForSeconds(quarterTime);
        eating_2.GetComponent<AssignFoodImage>().LoadHalfFoodImage();

        yield return new WaitForSeconds(quarterTime);
        eating_1.GetComponent<AssignFoodImage>().LoadEmptyFoodImage();

        yield return new WaitForSeconds(quarterTime);
        if (eating_2 != null)
        {
            eating_2.GetComponent<AssignFoodImage>().LoadEmptyFoodImage();
        }

    }
}
