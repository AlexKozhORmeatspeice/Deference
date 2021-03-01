using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class StoppingTowerAttack : SimpleTowerAttack
{
    [FormerlySerializedAs("_fieldOfViewAngle")] public float fieldOfViewAngle = 110.0f;

    [Range(0.0f, 1.0f)] [SerializeField] private float stoppingСoeff = 0.5f;

    [SerializeField] private Vector3 directionOfAttack = Vector3.forward;
    private List<GameObject> _enemysInView = new List<GameObject>();

    public float timeOfStoppingAfterLostTheTarget = 0.0f;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Creep>())
        {
            Vector3 posTower = transform.position;
            
            Vector3 direction = other.transform.position - posTower;
            
            float angle = Vector3.Angle(direction, directionOfAttack);
            bool enemyInList = _enemysInView.Any(x => x == other.gameObject);
            
            if (angle < fieldOfViewAngle * 0.5f && !enemyInList)
            {
                _enemysInView.Add(other.gameObject);
                
                Atack(other.gameObject);
            } 
            else if(angle >= fieldOfViewAngle && enemyInList )
            {
                other.gameObject.GetComponent<Creep>().MyWave.GetComponent<StoppingWave>().DestroyObject(); 
                _enemysInView.Remove(other.gameObject);
            }
        }
    }
    protected override void Atack(GameObject target)
    {
        Creep unit = target.GetComponent<Creep>();
        
        unit.MyWave = Instantiate(cartridgePrefab, transform.position, Quaternion.identity);
        unit.MyWave.transform.parent = this.gameObject.transform;
        
        StoppingWave waveScript = unit.MyWave.GetComponent<StoppingWave>();
        
        waveScript.target = target;
        waveScript.Damage = _towerPrefs.Damage;
        waveScript.StoppingCoeff = stoppingСoeff;
        waveScript.TimeToStoppingAfterDestroy = timeOfStoppingAfterLostTheTarget;
        waveScript.SpeedOfAtack = _towerPrefs.SpeedOfAtack;
        
        waveScript.gameObject.SetActive(true);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        return;
    }
    protected override void OnTriggerExit(Collider other)
    {
        GameObject wave = other.gameObject.GetComponent<Creep>().MyWave;
        if (wave != null)
        {
            wave.GetComponent<StoppingWave>().DestroyObject();
        }
        _enemysInView.Remove(other.gameObject);
    }
    protected override void Update()
    {
        return;
    }


}
