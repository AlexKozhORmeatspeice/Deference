using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : Singleton<Loader>
{
    public GameObject linePrefab;
    public int countLines = 50;


    protected override void Awake()
    {
        base.Awake();
        ManagerPool.Instance.AddPool(PoolType.Entities).PopulateWith(linePrefab, countLines, 30, 1);
    }
}