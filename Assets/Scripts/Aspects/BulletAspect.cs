using ComponentsAndTags;
using Properties;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct BulletAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<BulletProperties.BulletSpeed> _bulletSpeed;
        private readonly RefRW<BulletProperties.BulletDamage> _bulletDamage;
        private readonly RefRW<BulletProperties.BulletLifetime> _bulletLifetime;
        private readonly RefRO<BulletTag> _tag;

        public void MoveBullet(float deltaTime)
        {
            _localTransform.ValueRW.Position += _localTransform.ValueRW.Up() * _bulletSpeed.ValueRO.Value * deltaTime;
        }
        
        public float3 Position => _localTransform.ValueRO.Position;

        public float Lifetime
        {
            get => _bulletLifetime.ValueRO.Value;
            set => _bulletLifetime.ValueRW.Value = value;
        }
    }
}