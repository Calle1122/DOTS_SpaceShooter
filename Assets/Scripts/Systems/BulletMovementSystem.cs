using Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    [BurstCompile]
    public partial struct BulletMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new MoveBulletJob
            {
                DeltaTime = deltaTime
            }.Run();
        }
    }
    
    [BurstCompile]
    public partial struct MoveBulletJob : IJobEntity
    {
        public float DeltaTime;
        
        [BurstCompile]
        private void Execute(BulletAspect bullet)
        {
            bullet.MoveBullet(DeltaTime);
        }
    }
}