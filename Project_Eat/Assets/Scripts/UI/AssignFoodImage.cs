using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignFoodImage : MonoBehaviour
{
    public string thisFoodKey;
    public SpriteRenderer spriteRenderer;

    public void LoadFullFoodImage(string foodKey)
    {
        if (!gameObject.transform.GetChild(0).gameObject.TryGetComponent<SpriteRenderer>(out spriteRenderer)) return;

        thisFoodKey = foodKey;
        string address = "Arts/Food/Full/" + thisFoodKey;

        spriteRenderer.sprite = Resources.Load<Sprite>(address);
    }

    public void LoadHalfFoodImage()
    {
        string address = "Arts/Food/Half/" + thisFoodKey;
        spriteRenderer.sprite = Resources.Load<Sprite>(address);
    }

    public void LoadEmptyFoodImage()
    {
        string address = "Arts/Food/Empty";
        spriteRenderer.sprite = Resources.Load<Sprite>(address);
    }
}
