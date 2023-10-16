using Properties;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct PlayerShootAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _localTransform;
        private readonly RefRO<PlayerProperties.PlayerCombat> _playerCombat;
        private readonly RefRO<PlayerProperties.BulletPrefab> _bulletPrefab;
        private readonly RefRW<PlayerProperties.PlayerShootTimer> _playerShootTimer;

        public void Shoot()
        {
            if (!CanShoot)
            {
                return;
            }

            _playerShootTimer.ValueRW.Value = _playerCombat.ValueRO.FireCooldown;
        }   

        public bool CanShoot => _playerShootTimer.ValueRO.Value <= 0f;
        
        public float PlayerShootTimer
        {
            get => _playerShootTimer.ValueRO.Value;
            set => _playerShootTimer.ValueRW.Value = value;
        }

        public Entity BulletPrefab => _bulletPrefab.ValueRO.Value;

        public LocalTransform BulletSpawnPoint => new LocalTransform
        {
            Position = new float3(_localTransform.ValueRO.Position.x, _localTransform.ValueRO.Position.y, _localTransform.ValueRO.Position.z),
            Rotation = quaternion.identity,
            Scale = 1f
        };
    }
}