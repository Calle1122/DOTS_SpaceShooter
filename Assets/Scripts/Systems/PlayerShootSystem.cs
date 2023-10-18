using Aspects;
using ComponentsAndTags;
using Properties;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    [BurstCompile]
    public partial struct PlayerShootSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
            
            state.RequireForUpdate<PlayerProperties.PlayerCombat>();
            state.RequireForUpdate<PlayerProperties.BulletPrefab>();
        }
        
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecbSingleton = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var deltaTime = SystemAPI.Time.DeltaTime;
            
            new ReloadWeaponJob
            {
                DeltaTime = deltaTime
            }.Run();
            
            foreach (var (projectilePrefab, transform) in SystemAPI.Query<PlayerProperties.BulletPrefab, LocalTransform>().WithAll<FireProjectileTag>())
            {
                new FireBulletJob
                {
                    ECB = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged),
                    InitialTransform = transform
                }.Run();
            }
        }
        
        public partial struct FireBulletJob : IJobEntity
        {
            public EntityCommandBuffer ECB;
            public LocalTransform InitialTransform;
            
            [BurstCompile]
            private void Execute(PlayerShootAspect shoot)
            {
                if (!shoot.CanShoot) return;

                shoot.Shoot();

                var newProjectile = ECB.Instantiate(shoot.BulletPrefab);
                var newProjectileTransform = LocalTransform.FromPositionRotation(InitialTransform.Position, InitialTransform.Rotation);
                
                ECB.SetComponent(newProjectile, newProjectileTransform);
            }
        }
        
        public partial struct ReloadWeaponJob : IJobEntity
        {
            public float DeltaTime;
            
            [BurstCompile]
            private void Execute(PlayerShootAspect shoot)
            {
                shoot.PlayerShootTimer -= DeltaTime;
            }
        }
    }
}