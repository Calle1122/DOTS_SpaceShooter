using ComponentsAndTags;
using Unity.Entities;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct DestroyableObjectAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<IsDestroyedTag> _destroyedTag;

        public void Destroy(EntityCommandBuffer ECB)
        {
            ECB.DestroyEntity(Entity);
        }
    }
}