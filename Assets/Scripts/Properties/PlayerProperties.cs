using Unity.Entities;
using UnityEngine;

namespace Properties
{
    public static class PlayerProperties
    {
        public struct PlayerHealth : IComponentData
        {
            public float Health;
        }
        
        public struct PlayerMovement : IComponentData
        {
            public float MoveSpeed;
        }

        public struct PlayerCombat : IComponentData
        {
            public float Damage;
            public float BulletSpeed;

            public float FireCooldown;
        }
        
        public struct PlayerShootTimer : IComponentData
        {
            public float Value;
        }

        public struct BulletPrefab : IComponentData
        {
            public Entity Value;
        }
    }
}
