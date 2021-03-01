using System.Collections.Generic;
using UnityEngine;

public class SpawnUnitManager : MonoBehaviour
{
#pragma warning disable 649 // Field is never assigned to, and will always have its default value

    private int _waveLevel = 0;
    [SerializeField] private int _maxWaveLevel;
    [SerializeField] private int _maxCountOfUnitsOnWave;
    [SerializeField] private int _plusToMaxCountOfUnitsEveryWave;
    
    [SerializeField] private List<GameObject> typesEnemys = new List<GameObject>();

    [SerializeField] [Range(1.0f, 30.0f)] private float TimeBetwenWaves = 15.0f;

    [SerializeField] [Range(1.0f, 10.0f)] private float timeBetweenSpawns = 1.0f;

    private float _timeOfNewWave;
    private float _enemySpawnTime;

    [SerializeField] private string alternateListOfWaveStates = "000";
    private static WaveStateTypes _nowStateOfWave;
    static public WaveStateTypes NowStateOfWave => _nowStateOfWave;

    public enum WaveStateTypes
    {
        RightType = 0,
        LeftType = 1, 
        RandomType = 2,
        OnlyOneRoute = 3
    }
#pragma warning restore 649

    // Start is called before the first frame update
    void Start()
    {
        _enemySpawnTime = 0.0f;

    }

    private int _waveUnitCounter = -1;

    private int _statesCounter = 0;
    // Update is called once per frame
    void Update()
    {
        if (_waveLevel > _maxWaveLevel)
        {
            return;
        }
        if (_waveUnitCounter >= _maxCountOfUnitsOnWave || _waveUnitCounter == -1)
        {
            _waveUnitCounter = 0;
            _maxCountOfUnitsOnWave += _plusToMaxCountOfUnitsEveryWave;
            
            _timeOfNewWave = Time.time;
            if (_nowStateOfWave != WaveStateTypes.OnlyOneRoute)
            {
                _nowStateOfWave = (WaveStateTypes)(int.Parse(alternateListOfWaveStates[_statesCounter].ToString()));
                Debug.Log(_nowStateOfWave);
                _statesCounter++;
            }
            
            _waveLevel++;
            
        }

        Vector3 spawnPoint = transform.position;
        spawnPoint.y = 1.5f; 
        if (Time.time - _enemySpawnTime >= timeBetweenSpawns && Time.time - _timeOfNewWave >= TimeBetwenWaves)
        {
            Instantiate(typesEnemys[Random.Range(0, typesEnemys.Count)], spawnPoint, Quaternion.identity);
            _enemySpawnTime = Time.time;
            _waveUnitCounter++;
        }

    }

}
