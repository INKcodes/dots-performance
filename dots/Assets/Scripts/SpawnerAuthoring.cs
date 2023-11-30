using DefaultNamespace;
using Unity.Entities;
using UnityEngine;

public class SpawnerAuthoring : MonoBehaviour
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public float spawnFrequency;
    [SerializeField] public int spawnCount;

    public class SpawnerAuthoringBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity,
                new SpawnerComponent
                    {
                        prefab = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic),
                        spawnFrequency = authoring.spawnFrequency,
                        spawnCount = authoring.spawnCount
                    });
            AddComponent<SpawnTimer>(entity);
            AddComponent<LinkCounter>(entity);
        }
    }
}