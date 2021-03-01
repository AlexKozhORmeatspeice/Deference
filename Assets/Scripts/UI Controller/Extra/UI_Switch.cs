using UnityEngine;
[System.Serializable]
public class UI_Switch
{
    [SerializeField]
    private UI_Element[] turnOn;
    [SerializeField]
    private UI_Element[] turnOff;
    public void TurnOn()
    {
        foreach (var a in turnOn)
        {
            a.TurnOn();
        }
        foreach (var a in turnOff)
        {
            a.TurnOff();
        }
    }
    public int idBack;
}
