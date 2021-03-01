using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TowerPointOfInst : MonoBehaviour
{
    private GameObject tower;
    private bool isActive;
    
    public GameObject Tower
    {
        set =>  tower = value;
    }
    public bool IsActive
    {
        get => isActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        isActive = (tower != null);
    }
}
