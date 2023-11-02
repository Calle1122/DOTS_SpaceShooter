using Aspects;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [BurstCompile]
    public partial struct AsteroidMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Dependency.Complete();
            
            new MoveAsteroidJob()
            {
                DeltaTime = Time.deltaTime
            }.Run();
        }
    }

    [BurstCompile]
    public partial struct MoveAsteroidJob : IJobEntity
    {
        public float DeltaTime;
        
        private void Execute(AsteroidAspect asteroid)
        {
            if (asteroid.HasReachedPlayer) return;
            
            asteroid.MoveTowardsPlayer(DeltaTime);
        }
    }
}