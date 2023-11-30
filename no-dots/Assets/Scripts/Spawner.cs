using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public float spawnFrequency;

    private float _timeSincelastSpawn;
    private int _spawnCount;
    private float limitY = 8f;
    private float limitX = 14.7f;
    private float sizeX = 0.3f;
    private float sizeY = 0.4f;
    private int rows;
    private int cols;

    void Start()
    {
        _timeSincelastSpawn = spawnFrequency;
        rows = (int) ( 2 * limitY / sizeY) + 2;
        cols = (int)(2 * limitX / sizeX) + 2;
    }

    void Update()
    {
        _timeSincelastSpawn -= Time.deltaTime;
        if (_timeSincelastSpawn > 0)
        {
            return;
        }

        _timeSincelastSpawn = spawnFrequency;

        var newGo = Instantiate(prefab);
        var spawnColumn = _spawnCount;
        var spawnRow = 0;
        var targetPosition = calcTargetPos(_spawnCount);
        newGo.GetComponent<MoveLink>().targetPos = targetPosition;
        _spawnCount += 1;

        if (_spawnCount >= rows * cols)
        {
            this.enabled = false;
        }
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 150, 50), $"Link Count: {_spawnCount}");
    }

    private Vector2 calcTargetPos(int spawnCount)
    {
        int row = spawnCount % rows;
        int col = spawnCount / rows;
        
        return new Vector2(-limitX + col * sizeX, limitY - row * sizeY);
    }
}