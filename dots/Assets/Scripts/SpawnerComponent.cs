using Unity.Entities;

public struct SpawnerComponent : IComponentData
{
    public Entity prefab;
    public float spawnFrequency;
    public int spawnCount;
}