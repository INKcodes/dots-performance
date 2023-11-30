using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public float spawnFrequency;
    [SerializeField] public int spawnCount;

    private float _timeSincelastSpawn;
    private int _currentLinkCount;
    
    public const int AreaMultiplier = 2;
    private const float LimitY = 8f * AreaMultiplier;
    private const float LimitX = 14.7f * AreaMultiplier;
    private const float SizeX = 0.3f;
    private const float SizeY = 0.4f;
    private const int Rows= (int) ( 2 * LimitY / SizeY) + 2;
    private const int Cols= (int)(2 * LimitX / SizeX) + 2;
    private const int MaximumLinksToSpawn = Rows * Cols;

    void Start()
    {
        _timeSincelastSpawn = spawnFrequency;
    }

    void Update()
    {
        if (_currentLinkCount >= MaximumLinksToSpawn)
        {
            return;
        }
        
        _timeSincelastSpawn -= Time.deltaTime;
        if (_timeSincelastSpawn > 0)
        {
            return;
        }

        _timeSincelastSpawn = spawnFrequency;

        for (int i = 0; i < spawnCount; ++i)
        {
            var newGo = Instantiate(prefab);
            var spawnColumn = _currentLinkCount;
            var spawnRow = 0;
            var targetPosition = calcTargetPos(_currentLinkCount);
            newGo.GetComponent<MoveLink>().targetPos = targetPosition;
            _currentLinkCount += 1;
            if (_currentLinkCount >= MaximumLinksToSpawn)
            {
                break;
            }
        }

        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 150, 50), $"Link Count: {_currentLinkCount}");
    }

    private Vector2 calcTargetPos(int spawnCount)
    {
        int row = spawnCount % Rows;
        int col = spawnCount / Rows;
        
        return new Vector2(-LimitX + col * SizeX, LimitY - row * SizeY);
    }
}