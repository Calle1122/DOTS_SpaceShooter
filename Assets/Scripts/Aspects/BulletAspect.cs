using Properties;
using Unity.Entities;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct BulletAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<BulletProperties.BulletSpeed> _bulletSpeed;
        private readonly RefRW<BulletProperties.BulletDamage> _bulletDamage;

        public void MoveBullet(float deltaTime)
        {
            _localTransform.ValueRW.Position += _localTransform.ValueRW.Up() * _bulletSpeed.ValueRO.Value * deltaTime;
        }
    }
}