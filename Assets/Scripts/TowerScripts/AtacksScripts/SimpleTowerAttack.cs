using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public partial class SimpleTowerAttack : MonoBehaviour
{
#pragma warning disable 649 // Field is never assigned to, and will always have its default value
    protected float timeAtacked;
    [SerializeField] protected GameObject cartridgePrefab;

    public GameObject CartridgePrefab
    {
        get => cartridgePrefab;
    }
    [SerializeField ] protected int maxTargets;

    protected GameObject[] _targets;
    protected Tower _towerPrefs;

    protected List<GameObject> _targetsInAreaOfAtack = new List<GameObject>();

    public int _nowMaxEnemyOfAtack;
    

#pragma warning restore 649

    protected virtual void Awake()
    {
        _nowMaxEnemyOfAtack = 1;
        _targets = new GameObject[maxTargets];
        _towerPrefs = GetComponentInParent<Tower>();
    }

    protected virtual void Update()
    {
        if (_targetsInAreaOfAtack.Count == 0)
            return;
        bool haveNullTarget = _targets.Any(x => x == null);
        
        if (haveNullTarget)
        {
            for (int i = 0; i < maxTargets; i++)
            {
                _targets[i] = _targetsInAreaOfAtack[Random.Range(0, _targetsInAreaOfAtack.Count)];
            }
            
        }

        if (!haveNullTarget && Time.time - timeAtacked >= _towerPrefs.SpeedOfAtack)
        {
            for (int i = 0; i < _nowMaxEnemyOfAtack; i++)
            {
                Debug.Log(i);
                Atack(_targets[i]);
            }
            timeAtacked = Time.time;
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        GameObject gm = other.gameObject;

        if (gm.GetComponent<Creep>())
        {
            _targetsInAreaOfAtack.Add(gm);
        } else return;
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        GameObject gm = other.gameObject;

        if (gm.GetComponent<Creep>())
        {
            _targetsInAreaOfAtack.Remove(gm);
        }

        GameObject exitTarget = null;
        if (_targets.Any(x => { exitTarget = x; return x == gm; }))
        {
            exitTarget = null;
        }
    }

    protected virtual void Atack(GameObject target)
    {
        if (target == null) return;
        
        GameObject cartidge = Instantiate(cartridgePrefab, transform.position, Quaternion.identity);
        Cartridge cartridgeScr = cartidge.GetComponent<Cartridge>();
        
        cartridgeScr.Damage = _towerPrefs.Damage;
        cartidge.GetComponent<Cartridge>().target = target;

        timeAtacked = Time.time;
    }
}
