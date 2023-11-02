using Aspects;
using ComponentsAndTags;
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
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (bulletAspect, bullet) in SystemAPI.Query<BulletAspect>().WithEntityAccess())
            {
                bulletAspect.Lifetime -= deltaTime;
                
                foreach (var (asteroidAspect, asteroid) in SystemAPI.Query<AsteroidAspect>().WithEntityAccess())
                {
                    if (math.distancesq(bulletAspect.Position, asteroidAspect.Position) <= (.7f * .7f))
                    {
                        foreach (var playerAspect in SystemAPI.Query<PlayerAspect>())
                        {
                            playerAspect.Score++;
                        }
                        
                        ecb.DestroyEntity(asteroid);
                        ecb.DestroyEntity(bullet);
                    }
                }

                if (bulletAspect.Lifetime <= 0)
                {
                    ComponentLookup<IsDestroyedTag> destroy = SystemAPI.GetComponentLookup<IsDestroyedTag>();
                    destroy.SetComponentEnabled(bullet, true);
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