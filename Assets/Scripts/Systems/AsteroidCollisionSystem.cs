using Unity.Burst;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    public partial struct AsteroidCollisionSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            
        }
    }
}