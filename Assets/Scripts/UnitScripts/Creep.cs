using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Creep : MonoBehaviour
{
    [SerializeField] private float Lives = 3.0f;

    [SerializeField] private float damage = 1.0f;
    public float Damage => damage;

    
    private GameObject myWave;
    public GameObject MyWave
    {
        get => myWave;
        set => myWave = value;
    }
    

    [SerializeField] private int cost = 1;
    

    private void Update()
    {
        if (Lives <= 0) 
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    public void Hit(float damage)
    {
        this.Lives -= damage;
    }
}
