using System.Linq;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace DefaultNamespace
{
    [BurstCompile]
    public partial struct SpawnerSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<LinkCounter>();
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            
            new SpawnLinkJob()
            {
                DeltaTime = deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
            }.Schedule();
        }
    }


    [BurstCompile]
    public partial struct SpawnLinkJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        [BurstCompile]
        private void Execute(SpawnerAspect spawner)
        {
            if (spawner.LinkCount >= SpawnerAspect.MaximumLinksToSpawn)
            {
                return;
            }
            
            spawner.SpawnTimer -= DeltaTime;
            
            if (spawner.SpawnTimer > 0) return;
            spawner.SpawnTimer = spawner.SpawnFrequency;

            for (int i = 0; i < spawner.LinksToSpawn; ++i)
            {
                var newEntity = ECB.Instantiate(spawner.Prefab);
                ECB.AddComponent(newEntity, new TargetPosComponent
                {
                    Value = calcTargetPos(spawner.LinkCount)
                });
                spawner.LinkCount += 1;
                if (spawner.LinkCount >= SpawnerAspect.MaximumLinksToSpawn)
                {
                    break;
                }
            }
        }

        private float3 calcTargetPos(int currentSpawnCount)
        {
            int row = currentSpawnCount % SpawnerAspect.Rows;
            int col = currentSpawnCount / SpawnerAspect.Rows;

            return new float3(-SpawnerAspect.LimitX + col * SpawnerAspect.SizeX, SpawnerAspect.LimitY - row * SpawnerAspect.SizeY, 0);
        }
    }
}