using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Object = System.Object;
using Random = UnityEngine.Random;

public class CreepMoveController : MonoBehaviour
{
#pragma warning disable 649 // Field is never assigned to, and will always have its default value
    [SerializeField] private float speed = 3.0f;

    private List<Vector3> _movePoints = new List<Vector3>();
    private List<GameObject> _branchesObjects = new List<GameObject>();


    private NavMeshAgent _agent; 
#pragma warning restore 649
    
    private void Awake()
    {   
        MakeWay();
        
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = speed;
    }

    // Start is called before the first frame update
    
    private int _countOfPoint = 0;

    // Update is called once per frame
    void Update()
    {
        _agent.speed = speed;

        Vector3 nowEndPos = _movePoints[_countOfPoint];
        nowEndPos.y = transform.position.y;
        if(Vector3.Distance(transform.position, nowEndPos) <= 0.1f)
        {
            if (_countOfPoint + 1 >= _movePoints.Count)
            {
                _movePoints.Clear();
                Destroy(gameObject);
                return;
            }
            _countOfPoint++;

            nowEndPos = _movePoints[_countOfPoint];
            nowEndPos.y = transform.position.y;
        }

        _agent.SetDestination(nowEndPos);
    }


    private void MakeWay()
    {
        GameObject movePointsObject = GameObject.Find("MovePoints");
        for (int i = 0; i < movePointsObject.transform.childCount; i++)
        {
            GameObject nowChild = movePointsObject.transform.GetChild(i).gameObject;
            if (nowChild.CompareTag("MovePoint"))
            {
                _movePoints.Add(nowChild.transform.position);
            }

            if (nowChild.CompareTag("BranchPoint"))
            {
                _branchesObjects.Add(nowChild);
            }
        }
        for (int i = 0; i < _branchesObjects.Count; i++)
        {
            _movePoints.Add(_branchesObjects[i].transform.position);
            AddMovePointsInBranch(_branchesObjects[i]);
        }
    }
    private void AddMovePointsInObject(GameObject gm, List<Vector3> thisList)
    {
        for (int i = 0; i < gm.transform.childCount; i++)
        {
            GameObject nowChild = gm.transform.GetChild(i).gameObject;
            if(nowChild.CompareTag("MovePoint"))
            {
                thisList.Add(nowChild.transform.position);
            }
        }
    }

    private void AddMovePointsInBranch(GameObject branch)
    { 
            GameObject way;
            string typeOfRoad = "";
            
            switch (SpawnUnitManager.NowStateOfWave)
            {
                case SpawnUnitManager.WaveStateTypes.LeftType:
                    typeOfRoad = "Left";
                    break;
                case SpawnUnitManager.WaveStateTypes.RightType:
                    typeOfRoad = "Right";
                    break;
                case SpawnUnitManager.WaveStateTypes.RandomType:
                    typeOfRoad = (Random.Range(0.0f, 1.0f) > 0.5f) ?  "Left" : "Right";
                    break;
                
            }
            
            way = branch.transform.Find("Start").Find(typeOfRoad).gameObject;
            AddMovePointsInObject(way, _movePoints);

            
            GameObject endPos = branch.transform.Find("End").gameObject;
            _movePoints.Add(endPos.transform.position);
            AddMovePointsInObject(endPos, _movePoints);
            
    }

    public void SlowDownWithCoeff(float slowValue)
    {
        speed *= slowValue;
    }
}
