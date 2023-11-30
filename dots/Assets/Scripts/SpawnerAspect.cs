using Unity.Entities;

namespace DefaultNamespace
{
    public readonly partial struct SpawnerAspect : IAspect
    {
        public const int AreaMultiplier = 2;
        public const float LimitY = 8f * AreaMultiplier;
        public const float LimitX = 14.7f * AreaMultiplier;
        public const float SizeX = 0.3f;
        public const float SizeY= 0.4f;
        public const int Rows= (int)(2 * LimitY / SizeY) + 2;
        public const int Cols= (int)(2 * LimitX / SizeX) + 2;
        public const int MaximumLinksToSpawn = Rows * Cols;
        
        private readonly RefRW<SpawnTimer> spawnTimer;
        private readonly RefRO<SpawnerComponent> spawnerComponent;
        private readonly RefRW<LinkCounter> linkCount;
        
        public float SpawnTimer
        {
            get => spawnTimer.ValueRO.Value;
            set => spawnTimer.ValueRW.Value = value;
        }

        public float SpawnFrequency => spawnerComponent.ValueRO.spawnFrequency;

        public Entity Prefab => spawnerComponent.ValueRO.prefab;

        public int LinksToSpawn => spawnerComponent.ValueRO.spawnCount;
        
        public int LinkCount
        {
            get => linkCount.ValueRO.Value;
            set => linkCount.ValueRW.Value = value;
        }
    }
}