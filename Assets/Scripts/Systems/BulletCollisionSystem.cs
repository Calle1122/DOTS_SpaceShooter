using Aspects;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems
{
    [BurstCompile]
    public partial struct BulletCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (bulletAspect, bullet) in SystemAPI.Query<BulletAspect>().WithEntityAccess())
            {
                foreach (var (asteroidAspect, asteroid) in SystemAPI.Query<AsteroidAspect>().WithEntityAccess())
                {
                    if (math.distancesq(bulletAspect.Position, asteroidAspect.Position) <= (.7f * .7f))
                    {
                        ecb.DestroyEntity(asteroid);
                        ecb.DestroyEntity(bullet);
                    }
                }
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
    }
}