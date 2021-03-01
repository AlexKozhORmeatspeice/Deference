using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour
{
#pragma warning disable 649 // Field is never assigned to, and will always have its default value

    [SerializeField] private float _radiusOfAtack = 5.0f;
    public float RadiusOfAtack => _radiusOfAtack; 
    [SerializeField] private float _speedOfAtack = 1.0f;
    public float SpeedOfAtack => _speedOfAtack;
    [SerializeField] private float _damage = 1.0f;
    public float Damage => _damage;

    private int _level;
    private int maxLevel = 3;
    [SerializeField] private GameObject button;

    public GameObject Button
    {
        get => button;
    }

    [SerializeField] TypeTowers towerType;
    
    public enum TypeTowers : byte
    {
        SimpleTower = 1,
        LaserTower = 2,
        StoppingTower = 3,
        NullTower
    }

#pragma warning restore 649

    private void Awake()
    {   
        GetComponentInChildren<SphereCollider>().radius = RadiusOfAtack;
    }

    public void Destroyer()
    {
        Destroy(gameObject);
    }
    
    
    public void UpgradeOnOneLevel()
    {

        if (_level >= maxLevel)
        {
            Debug.Log("max level"); return; 
        }
        Debug.Log(_level);

        switch (towerType)
        {
            case TypeTowers.SimpleTower:
                switch (_level)
                {
                    case  0: 
                        GetComponentInChildren<SimpleTowerAttack>().CartridgePrefab.GetComponent<Cartridge>().RadiusOfBomb += 2.0f;
                        break;
                    case 1: 
                        _radiusOfAtack += 2.0f; 
                        GetComponentInChildren<SphereCollider>().radius = RadiusOfAtack;
                        break;
                    case 2:
                        GetComponentInChildren<SimpleTowerAttack>()._nowMaxEnemyOfAtack = 2;
                        break;
                } 
                break;
            
            case TypeTowers.LaserTower:
                GetComponentInChildren<SimpleTowerAttack>()._nowMaxEnemyOfAtack += 1;
                break;
            
            case  TypeTowers.StoppingTower:
                switch (_level)
                {
                    case  0: 
                        GetComponentInChildren<StoppingTowerAttack>().timeOfStoppingAfterLostTheTarget += 2.0f;
                        break;
                    case 1:
                        _damage += 0.05f;
                        break;
                    case 2:
                        GetComponentInChildren<StoppingTowerAttack>().fieldOfViewAngle += 20.0f;
                        break;
                } 
                break;
            
        }
        _level++;
        Debug.Log($"Object upgraded to {_level}");
    }

}
