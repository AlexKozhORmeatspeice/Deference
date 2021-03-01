using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UI_CallingOtherEvents
{
    [SerializeField]
    private UnityEvent turnOn;
    [SerializeField]
    private UnityEvent turnOff;
    public void TurnOn()
    {
        turnOn.Invoke();
        turnOff.Invoke();
    }
}
