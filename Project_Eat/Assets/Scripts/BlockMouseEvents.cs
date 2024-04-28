using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMouseEvents : MonoBehaviour
{
    // 在 Update 方法中检测鼠标左键按下事件
    void Update()
    {
        // 如果鼠标左键被按下
        if (Input.GetMouseButtonDown(0))
        {
            // 阻止事件，即不执行任何操作
            return;
        }
    }
}
