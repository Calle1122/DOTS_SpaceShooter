using Unity.Entities;

namespace Properties
{
    public static class BulletProperties
    {
        public struct BulletSpeed : IComponentData
        {
            public float Value;
        }
        
        public struct BulletDamage : IComponentData
        {
            public float Value;
        }
    }
}