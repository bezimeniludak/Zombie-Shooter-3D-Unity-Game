using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnLocations;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private Camera _player;

    [SerializeField] private float _minDistance = 10;
    [SerializeField] private float _maxDistance = 30;
    [SerializeField] private int _numberToSpawn = 3;
    [SerializeField] private int _enemyLimit = 20;
    [SerializeField] private float _rate = 5;

    float _spawnTimer;
    float _elapsedTime = 0;
    public static float _currentNo = 0;
    GameObject _spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        _spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._gameState == GameManager.GameState.start)
        {
            DestroyAllZombies();
        }
        if (GameManager._gameState == GameManager.GameState.running)
        {
            _spawnTimer -= Time.deltaTime;
            IncreaseDifficulty();
            if (_spawnTimer <= 0f)
            {
                GenerateSpawnLocation();
                if (_currentNo < _enemyLimit)
                {
                    for (int i = 0; i < _numberToSpawn; i++)
                    {
                        Instantiate(_enemy, new Vector3(_spawnLocation.transform.position.x + GetModifier(), 0, _spawnLocation.transform.position.z + GetModifier())
                            , Quaternion.identity, _spawnLocation.transform);
                        _currentNo++;
                        Debug.Log("Zombies: " + _currentNo);
                    }
                    _spawnTimer = _rate;
                }
            }
        }
    }
    float GetModifier()
    {
        float modifier = Random.Range(0f, 1f);
        if (Random.Range(0, 2) > 0)
            return -modifier;
        else
            return modifier;
    }

    private void GenerateSpawnLocation()
    {
        foreach (GameObject sl in _spawnLocations)
        {
            float distance = Vector3.Distance(_player.transform.position, sl.transform.position);
            if (distance >= _minDistance && distance <= _maxDistance)
            {
                _spawnLocation = sl;
                break;
            }
        }
    }
    private void IncreaseDifficulty()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= 60)
        {
            _numberToSpawn += 2;
            if (_rate > 0)
                _rate--;
            _elapsedTime = 0;
        }
    }
    private void DestroyAllZombies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemies)
        {
            Destroy(enemy);
        }

    }
}
