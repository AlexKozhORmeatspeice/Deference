using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class UI_Element : MonoBehaviour
{
    public enum SwitchingMode
    {
        Simple,
        AnimationsUsingScripts,
        AnimationsUsingAnimator
    }
    public SwitchingMode currentMode;
    
    [Header("Simple")]
    public GameObject[] enabledObjects;
    public GameObject[] disableObjects;

    [Header("Animations Using Animator")] 
    public Animator[] animators;
    public string nameKey = "isActive";
    
    public delegate void TurnAnim();
    public TurnAnim turnOn;
    public TurnAnim turnOff;
    
    
    [HideInInspector]
    public bool isActive;
    /*
    [Space]
    public string[] subElementsName;
    */
    private UI_Element[] subElements;

    private void Awake()
    {
        isActive = enabledObjects[0].activeInHierarchy;
        UI_Element[] childs = new UI_Element[transform.childCount];
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i] = transform.GetChild(i).GetComponent<UI_Element>();
        }
        subElements = childs;
    }

    public virtual void TurnOn()
    {
        if (isActive) return;
        
        isActive = true;
        switch (currentMode)
        {
            case SwitchingMode.Simple:
                ActiveObjects(true);
                break;
            case SwitchingMode.AnimationsUsingScripts:
                turnOn?.Invoke();
                break;
            case SwitchingMode.AnimationsUsingAnimator:
                foreach (var a in animators)
                {
                    a.SetBool(nameKey, true);
                }
                break;
        }
    }

    public virtual void TurnOff()
    {
        if (!isActive) return;
        
        isActive = false;
        switch (currentMode)
        {
            case SwitchingMode.Simple:
                ActiveObjects(false);
                break;
            case SwitchingMode.AnimationsUsingScripts:
                turnOff?.Invoke();
                break;
            case SwitchingMode.AnimationsUsingAnimator:
                foreach (var a in animators)
                {
                    a.SetBool(nameKey, true);
                }
                break;
        }
    }

    public virtual void ActiveObjects(bool active)
    {
        foreach (var a in enabledObjects)
        {
            a.SetActive(active);
        }
        
        foreach (var a in disableObjects)
        {
            a.SetActive(!active);
        }
        if (subElements.Length > 0)
        {
            foreach (UI_Element a in subElements)
            {
                a.TurnOff();
            }
        }
      
        
    }

    public void SwitchActive()
    {
        if (isActive) TurnOff();
        else TurnOn();
    }
}
