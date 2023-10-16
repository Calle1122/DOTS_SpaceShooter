using Aspects;
using Properties;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    [BurstCompile]
    public partial struct PlayerShootSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerProperties.PlayerCombat>();
            state.RequireForUpdate<PlayerProperties.BulletPrefab>();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            new PlayerShootJob
            {
                DeltaTime = Time.deltaTime,
                ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged)
            }.Run();
        }
    }
    
    [BurstCompile]
    public partial struct PlayerShootJob : IJobEntity
    {
        public float DeltaTime;
        public EntityCommandBuffer ECB;

        private void Execute(PlayerShootAspect shoot)
        {
            shoot.PlayerShootTimer -= DeltaTime;

            if (!shoot.CanShoot) return;
            //TODO: Add check for shoot input
            
            // Instantiate bullet on shoot input
            
            shoot.Shoot();

            var newBullet = ECB.Instantiate(shoot.BulletPrefab);
            var newBulletTransform = shoot.BulletSpawnPoint;
            
            ECB.SetComponent(newBullet, newBulletTransform);
        }
    }
}