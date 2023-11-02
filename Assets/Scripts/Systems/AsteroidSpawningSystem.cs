using System.Runtime.InteropServices;
using Aspects;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [BurstCompile]
    public partial struct AsteroidSpawningSystem : ISystem
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
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            new SpawnAsteroidJob
            {
                DeltaTime = Time.deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
    }

    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    public partial struct SpawnAsteroidJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;
        private void Execute(SpaceAspect space)
        {
            space.AsteroidSpawnTimer -= DeltaTime;
            if (!space.CanSpawnAsteroid) return;

            space.AsteroidSpawnRate -= 0.05f;
            space.AsteroidSpawnTimer = space.AsteroidSpawnRate;

            var newAsteroid = ECB.Instantiate(space.GetRandomAsteroidPrefab());
            var newAsteroidTransform = space.GetRandomSpaceTransform();
            
            ECB.SetComponent(newAsteroid, newAsteroidTransform);
        }
    }
}