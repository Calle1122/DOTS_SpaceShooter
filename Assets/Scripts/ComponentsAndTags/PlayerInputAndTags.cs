using Unity.Entities;
using Unity.Mathematics;

namespace ComponentsAndTags
{
    public struct PlayerMoveInput : IComponentData
    {
        public float2 Value;
    }
    
    public struct PlayerTag : IComponentData {}
    
    public struct FireProjectileTag : IComponentData, IEnableableComponent {}
}