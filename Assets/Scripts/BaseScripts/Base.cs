using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private float lives = 5.0f; // не использовать

    private float _maxHealth;

    private float Lives
    {
        get => lives;
        set
        {
            if (!(value > _maxHealth))
            {
                lives = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = lives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Lives <= 0)
        {
            Destroy(gameObject);
            Debug.Log("You lost the game");
            Time.timeScale = 0.0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Creep unit = other.gameObject.GetComponent<Creep>();
        if (unit)
        {
            GetDamage(unit.Damage);
            Destroy(unit.gameObject);
        }

    }
    
    private void GetDamage(float damage)
    {
        Lives -= damage;
        Debug.Log($"Base get damaged, lives = {Lives}");
    }
}
