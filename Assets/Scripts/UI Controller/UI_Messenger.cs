using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

public class UI_Messenger : Singleton<UI_Messenger>
{
    public UnityEvent[] events;
    private SpawnTowerManager _spawnTowerManager;
    
    public void ChoseTower(int id)
    {
        if (!_spawnTowerManager) _spawnTowerManager = FindObjectOfType<SpawnTowerManager>();
        _spawnTowerManager.ChoseTower(id);
    }

    public void CallEvent(int id)
    {
        events[id]?.Invoke();
    }
}
