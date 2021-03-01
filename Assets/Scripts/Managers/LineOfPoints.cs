using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfPoints : MonoBehaviour
{
    private LineRenderer _line;

    private List<Vector3> pointsOfLine = new List<Vector3>();
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("MovePoint");
        foreach (GameObject point in points)
        {
            pointsOfLine.Add(point.transform.position);
        }
    }

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = pointsOfLine.Count;
        StartCoroutine(DrawLine());
    }

    IEnumerator DrawLine()
    {
        for (int i = 0; i <= pointsOfLine.Count - 1; i++)
        {
            _line.SetPosition(i,pointsOfLine[i]);
            yield return new WaitForSeconds(0.1f);

        }
    }
}
