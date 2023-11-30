using System;
using System.Globalization;
using DefaultNamespace;
using TMPro;
using Unity.Entities;
using UnityEditorInternal;
using UnityEngine;

public class ShowLinks : MonoBehaviour
{
    private EntityManager _entityManager;

    // Start is called before the first frame update
    private void Start()
    {
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void OnGUI()
    {
        var spawnCount = _entityManager.CreateEntityQuery(typeof(TargetPosComponent)).CalculateEntityCount();
        GUI.Label(new Rect(0, 0, 150, 50), $"Link Count: {spawnCount}");
    }
}