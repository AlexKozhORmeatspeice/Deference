using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Controller : Singleton<UI_Controller>
{
    public string[] sections = new[]
    {
        "MainMenu",
        "Info",
        "Settings",
        "Skins"
    };

    public UI_Switch[] switches;
    
    [HideInInspector] public bool ready;
    [HideInInspector] public int currentSection;
    /*
    [Space]
    public string[] globalElementsName;

    public UI_Element[] UiElements;
    */

    protected void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void TurnOnSection(int idSection)
    {
        if ((int) currentSection == idSection) return;
        currentSection = idSection;
        switches[idSection].TurnOn();
        ready = false;
    }
    
    public void Back()
    {
        TurnOnSection(switches[currentSection].idBack);
    }
    
    /*
    public void SwitchGlobalElement(int idElement)
    {
        if (UiElements[idElement].isActive) UiElements[idElement].TurnOff();
        else UiElements[idElement].TurnOn();
    }
    */
}