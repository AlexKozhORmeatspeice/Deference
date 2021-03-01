using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnTowerManager : MonoBehaviour
{
#pragma warning disable 649 // Field is never assigned to, and will always have its default value

    [SerializeField]
    private GameObject[] towers;  
    private Tower.TypeTowers _currentlyTower;

    private GameObject _nowTowerBeingToCreat;

    void Start()
    {
        _currentlyTower = Tower.TypeTowers.NullTower;
    }
#pragma warning restore 649

    // Update is called once per frame
    void Update()
    {
        if (_currentlyTower != Tower.TypeTowers.NullTower)
        {
            ActivateSpawnerOfTower(_currentlyTower);
        }
    }

    private void ActivateSpawnerOfTower(Tower.TypeTowers typeOfTower) //надо будет заменить проверки положения мыши на проверку положения тачей
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit; 
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            TowerPointOfInst pointOfSpawn = hitObject.GetComponent<TowerPointOfInst>(); // определяет выбранную точку спауна объекта
            if (pointOfSpawn && !pointOfSpawn.IsActive)
            {
                if (_nowTowerBeingToCreat == null) // создает неактивную башню
                {
                    _nowTowerBeingToCreat = Instantiate(towers[(int)_currentlyTower - 1], hit.point, Quaternion.identity);
                    
                    _nowTowerBeingToCreat.GetComponent<Collider>().enabled = false;
                    _nowTowerBeingToCreat.GetComponentInChildren<SimpleTowerAttack>().enabled = false;
                }
                
                Vector3 spawnPoint = hitObject.transform.position;
                spawnPoint.y += _nowTowerBeingToCreat.transform.localScale.y / 2;

                _nowTowerBeingToCreat.transform.position = spawnPoint;

                if (Input.GetMouseButtonDown(0)) // фиксирует положение созданной башни
                {
                    _nowTowerBeingToCreat.GetComponent<Collider>().enabled = true;
                    _nowTowerBeingToCreat.GetComponentInChildren<SimpleTowerAttack>().enabled = true;
                    
                    pointOfSpawn.Tower = _nowTowerBeingToCreat;
                    
                    _nowTowerBeingToCreat = null;
                    _currentlyTower = Tower.TypeTowers.NullTower;
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _currentlyTower = Tower.TypeTowers.NullTower;
                }
                Destroy(_nowTowerBeingToCreat);
            }
        }
    }
    
    public void ChoseTower(int towerType)
    {
        _currentlyTower = (Tower.TypeTowers)(towerType);
        Debug.Log("Changed tower");
    }
}
