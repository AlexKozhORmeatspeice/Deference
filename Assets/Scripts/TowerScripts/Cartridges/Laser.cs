using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Cartridge
{
    private float dist;
    private LineRenderer _lineRenderer;
    
    private float _finelAtackTime;
    
    private void Awake()
    {
        _finelAtackTime = Time.time;
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.SetPosition(0, GetComponentInParent<Tower>().gameObject.transform.position);

    }

    protected override void OnTriggerEnter(Collider other)
    { 
        return;
    }

    protected override void Attack()
    {
        if (target == null)
        {
            _lineRenderer.SetPosition(1, GetComponentInParent<Tower>().gameObject.transform.position);
            return;
        }

        if (Time.time - _finelAtackTime >= SpeedOfAtack)
        {
            target.GetComponent<Creep>().Hit(Damage);
            _finelAtackTime = Time.time;
        }

        
        _lineRenderer.SetPosition(1, target.transform.position);
    }
}
