using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    public List<Transform> pathPoints; // 关键路径点的 Transform
    public Color lineColor = Color.white; // 连线颜色

    private void OnDrawGizmos()
    {
        if (pathPoints.Count < 2)
            return;

        // 设置连线颜色
        Gizmos.color = lineColor;

        // 绘制路径线条
        for (int i = 1; i < pathPoints.Count; i++)
        {
            Gizmos.DrawLine(pathPoints[i - 1].position, pathPoints[i].position);
        }
    }
}
