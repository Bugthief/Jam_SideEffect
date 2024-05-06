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

            if (eatingCoroutine != null)
            {
                StopCoroutine(eatingCoroutine);
            }
        }
    }

    public void MoveToEatingPlate(GameObject food_1, GameObject food_2)
    {
        eating_1 = food_1; 
        eating_2 = food_2;

        food_1.transform.position = slot_1_transform.position;
        food_2.transform.position = slot_2_transform.position;

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
        //eating_2.GetComponent<AssignFoodImage>().LoadEmptyFoodImage();
    }
}
