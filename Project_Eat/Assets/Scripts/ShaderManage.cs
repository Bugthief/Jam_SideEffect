using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderManage : MonoBehaviour
{
    public bool inSideEffect = false;
    public bool lyhh = false;
    private void Update()
    {
        // 老眼昏花
        if (Input.GetKey(KeyCode.R))
        {
            lyhh = true;
        }
        else
        {
            lyhh = false;
        }

        if (!inSideEffect)
            Reset();
    }

    void LYHH()
    {
        // 老眼昏花
        gameObject.GetComponent<RGBShiftEffect>().amount = 0.01f;
        gameObject.GetComponent<RGBShiftEffect>().speed = 3f;

        gameObject.GetComponent<BloomEffect>().strength = 0.8f;
        gameObject.GetComponent<BloomEffect>().size = 5f;
        gameObject.GetComponent<BloomEffect>().cutOff = 0.25f;
    }

    void Reset()
    {
        gameObject.GetComponent<RGBShiftEffect>().amount = 0f;
        gameObject.GetComponent<RGBShiftEffect>().speed = 0f;

        gameObject.GetComponent<BloomEffect>().strength = 0.0f;
        gameObject.GetComponent<BloomEffect>().size = 0f;
        gameObject.GetComponent<BloomEffect>().cutOff = 0f;
    }
}
