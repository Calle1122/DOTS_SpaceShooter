using Unity.Entities;
using Unity.Mathematics;

namespace Properties
{
    public static class SpaceProperties
    {
        public struct SpaceDimensions : IComponentData
        {
            public float2 Value;
        }
        
        public struct SpacePrefabs : IComponentData
        {
            public Entity SmallAsteroid;
            public Entity BigAsteroid;
        }

        public struct ShipSafetyRadius : IComponentData
        {
            public float Value;
        }

        public struct SpaceSpawnFrequencies : IComponentData
        {
            public float AsteroidFrequency;
            //public float EnemyShipFrequency;
        }
        
        public struct AsteroidsSpawnTimer : IComponentData
        {
            public float Timer;
        }
    }
}