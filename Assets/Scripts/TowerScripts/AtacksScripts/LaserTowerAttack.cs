using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Xml.Schema;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class LaserTowerAttack : SimpleTowerAttack
{
#pragma warning disable 649 // Field is never assigned to, and will always have its default value
    private List<Laser> _activeLasers = new List<Laser>();

    
#pragma warning restore 649
        
    // Start is called before the first frame update

    private void Start()
    {

        for (int i = 0; i < maxTargets; i++)
        {
            _activeLasers.Add(Instantiate(cartridgePrefab, transform.position, Quaternion.identity).GetComponent<Laser>()); 
            _activeLasers[i].transform.parent = this.gameObject.transform;
        }

    }

    // Update is called once per frame
    private int nowLaser = 0; 
    private bool activateLasers = false;
    protected override void Update()
    {
        if (_targetsInAreaOfAtack.Count == 0)
        {
            return;
        }
        
        bool haveNullTarget = _targets.Any(x => x == null);
        if (haveNullTarget)
        {  
            for (int i = 0; i < maxTargets; i++)
            {
                if (_activeLasers[i].target == null && i < _nowMaxEnemyOfAtack)
                {
                    _activeLasers[i].gameObject.SetActive(false);
                }

                if (_targets[i] == null)
                {
                    _targets[i] = _targetsInAreaOfAtack[Random.Range(0, _targetsInAreaOfAtack.Count)];
                }
            }

            activateLasers = false;
        }
        else if(!activateLasers)
        {
            Debug.Log("I'm active");
            for (int i = 0; i < _nowMaxEnemyOfAtack; i++)
            {
                nowLaser = i;
                Atack(_targets[i]);
            }
            
            activateLasers = true;
            
            nowLaser = 0;
            return;
        }
        
    }
    

    protected override void Atack(GameObject target)
    {
        Laser nowlaserScript = _activeLasers[nowLaser];

        if (nowlaserScript.gameObject.activeSelf == true)
        {
            return;
        }
        
        nowlaserScript.target = target;
        nowlaserScript.SpeedOfAtack = _towerPrefs.SpeedOfAtack;
        nowlaserScript.Damage = _towerPrefs.Damage;
        
        _activeLasers[nowLaser].gameObject.SetActive(true);
    }
}