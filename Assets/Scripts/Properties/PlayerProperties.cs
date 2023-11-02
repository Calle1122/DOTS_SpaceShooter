using Unity.Entities;
using UnityEngine;

namespace Properties
{
    public static class PlayerProperties
    {
        public struct PlayerMovement : IComponentData
        {
            public float MoveSpeed;
        }

        public struct PlayerCombat : IComponentData
        {
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
