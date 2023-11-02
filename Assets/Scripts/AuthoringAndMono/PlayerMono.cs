using ComponentsAndTags;
using Properties;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class PlayerMono : MonoBehaviour
    {
        public float startingHealth;
        public float moveSpeed;
        public float fireCooldown;

        public GameObject bulletPrefab;
    }


    public class PlayerBaker : Baker<PlayerMono>
    {
        public override void Bake(PlayerMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new PlayerProperties.PlayerMovement
            {
                MoveSpeed = authoring.moveSpeed
            });
            AddComponent(entity, new PlayerProperties.PlayerCombat
            {
                FireCooldown = authoring.fireCooldown
            });
            AddComponent(entity, new PlayerProperties.PlayerShootTimer
            {
                Value = 0f
            });
            AddComponent(entity, new PlayerProperties.BulletPrefab
            {
                Value = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic)
            });
            AddComponent(entity, new FireProjectileTag());
            SetComponentEnabled<FireProjectileTag>(entity, false);
            AddComponent(entity, new PlayerTag());
            AddComponent(entity, new PlayerMoveInput());
            AddComponent(entity, new Health
            {
                Value = (int)authoring.startingHealth
            });
            AddComponent(entity, new Score());
        }
    }
}
