using Aspects;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace Systems
{
    [BurstCompile]
    public partial struct AsteroidCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (playerAspect, player) in SystemAPI.Query<PlayerAspect>().WithEntityAccess())
            {
                foreach (var (asteroidAspect, asteroid) in SystemAPI.Query<AsteroidAspect>().WithEntityAccess())
                {
                    if (math.distancesq(playerAspect.Position, asteroidAspect.Position) <= (.5f * .5f))
                    {
                        ecb.DestroyEntity(asteroid);
                        playerAspect.Health--;
                    }
                }
                
                foreach (var (asteroidAspect, asteroid) in SystemAPI.Query<AsteroidAspect>().WithEntityAccess())
                {
                    if (playerAspect.Health <= 0)
                    {
                        ecb.DestroyEntity(asteroid);
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