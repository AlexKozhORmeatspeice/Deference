using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    private int segments = 100;
    public float radius;
    private LineRenderer _line;

    [SerializeField] float drawSpeed = 0.3f;

    private SphereCollider _collider;

    void Start()
    { 
        _collider = GetComponent<SphereCollider>();

        radius = _collider.radius;
        _line = gameObject.GetComponent<LineRenderer>();
    
        
        _line.positionCount = (segments + 2);
        _line.useWorldSpace = false;
        MakeCircle();
    }

    private void Update()
    {
        //radius = Mathf.Sqrt(Mathf.Pow(_collider.transform.localScale.x,2)  + Mathf.Pow(_collider.transform.localScale.z,2) )  * 2;
    }

    public void MakeCircle()
    {
        StartCoroutine(CreatePoints());
    }
    IEnumerator CreatePoints()
    {
        radius = _collider.radius;
        float x;
        float z;

        float change = 2 * (float)Math.PI / segments;
        float angle = change;

        x = Mathf.Sin(angle) * radius;
        _line.SetPosition(0, new Vector3(x, 0, Mathf.Cos(angle) * radius));

        for (int i = 1; i < (segments + + 2); i++)
        {
            x = Mathf.Sin(angle) * radius;
            z = Mathf.Cos(angle) * radius;

            yield return new WaitForSeconds(drawSpeed);
            _line.SetPosition((int)i, new Vector3(x, 0, z));

            angle += change;
        }
    }
}