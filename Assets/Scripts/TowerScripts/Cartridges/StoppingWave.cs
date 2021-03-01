using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StoppingWave : Cartridge
{
    private float _stoppingCoeff;
    private LineRenderer _line;

    private CreepMoveController _targetUnitMoveScript;

    private bool slowDownd = false;
    public float StoppingCoeff
    {
        set => _stoppingCoeff = value;
    }

    private bool destroy = false;
    private float _timeToStoppingAfterDestroy;

    public float TimeToStoppingAfterDestroy
    {
        set => _timeToStoppingAfterDestroy = value;
    }

    private float _finelAtackTime;

    // Start is called before the first frame update
    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
        _line.SetPosition(0, GetComponentInParent<Tower>().gameObject.transform.position);

        _finelAtackTime = 0.0f;
        _targetUnitMoveScript = target.gameObject.GetComponent<CreepMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
            return;
        }
        if (destroy)
        {
            _line.SetPosition(1, _line.GetPosition(0));
            return;
        }

        if (!slowDownd)
        {
            _targetUnitMoveScript.SlowDownWithCoeff(_stoppingCoeff);
            slowDownd = true;
        }
        
        if (Time.time - _finelAtackTime >= SpeedOfAtack)
        {
            target.GetComponent<Creep>().Hit(Damage);
            _finelAtackTime = Time.time;
        }
        
        _line.SetPosition(1, target.transform.position);

    }

    public void DestroyObject()
    {
        StartCoroutine(this.DestroyWave());
    }

    private IEnumerator DestroyWave()
    {
        destroy = true;
        Debug.Log(1);
        yield return new WaitForSeconds(_timeToStoppingAfterDestroy);
        Debug.Log(2);

        _targetUnitMoveScript.SlowDownWithCoeff(1/_stoppingCoeff);
        Destroy(this.gameObject);

    }
    
}
