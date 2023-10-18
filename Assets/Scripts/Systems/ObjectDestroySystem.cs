using Aspects;
using ComponentsAndTags;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace Systems
{
    [BurstCompile]
    public partial struct ObjectDestroySystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            
            foreach (var objectToDestroy in SystemAPI.Query<DestroyableObjectAspect>().WithAll<IsDestroyedTag>())
            {
                objectToDestroy.Destroy(ecb);
            }
            
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}