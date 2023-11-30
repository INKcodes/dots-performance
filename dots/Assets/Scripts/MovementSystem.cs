using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using IJobEntity = Unity.Entities.IJobEntity;

namespace DefaultNamespace
{
    [BurstCompile]
    [UpdateAfter(typeof(SpawnerSystem))]
    public partial struct MovementSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new MoveLinkJob
            {
                DeltaTime =  SystemAPI.Time.DeltaTime
            }.ScheduleParallel();
        }
    }
    
    [BurstCompile]
    [WithAll(typeof(TargetPosComponent))]
    public partial struct MoveLinkJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(in TargetPosComponent targetPos, ref LocalTransform transform)
        {
            transform.Position = MoveTowards(transform.Position, new float3(targetPos.Value.x, targetPos.Value.y, 0), 1.6f * DeltaTime);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static float3 MoveTowards(float3 current, float3 target, float maxDistanceDelta) {
            float dirX = target.x - current.x;
            float dirY = target.y - current.y;
            float dirZ = target.z - current.z;
 
            float sqrLength =  dirX * dirX + dirY * dirY + dirZ * dirZ;
 
            if (sqrLength == 0.0 || maxDistanceDelta >= 0.0 && sqrLength <= maxDistanceDelta * maxDistanceDelta)
                return target;
 
            float dist = math.sqrt(sqrLength);
 
            return new float3(current.x + dirX / dist * maxDistanceDelta,
                current.y + dirY / dist * maxDistanceDelta,
                current.z + dirZ / dist * maxDistanceDelta);
        }
    }
}