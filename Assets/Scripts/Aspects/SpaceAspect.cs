using ComponentsAndTags;
using Properties;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct SpaceAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRO<LocalTransform> _localTransform;
        private readonly RefRO<SpaceProperties.SpaceDimensions> _spaceDimensions;
        private readonly RefRO<SpaceProperties.SpacePrefabs> _spacePrefabs;
        private readonly RefRW<SpaceProperties.SpaceSpawnFrequencies> _spaceSpawnFrequencies;
        private readonly RefRW<SpaceProperties.AsteroidsSpawnTimer> _asteroidSpawnTimer;
        private readonly RefRW<SpaceProperties.ShipSafetyRadius> _spaceSafetyRadius;
        private readonly RefRW<SpaceRandom> _spaceRandom;

        public float ShipSafetyRadius
        {
            get => _spaceSafetyRadius.ValueRO.Value;
            set => _spaceSafetyRadius.ValueRW.Value = value;
        }
        
        public LocalTransform GetRandomSpaceTransform()
        {
            return new LocalTransform
            {
                Position = GetRandomSpacePosition(),
                Rotation = GetRandomRotation(),
                Scale = GetRandomScale(0.9f, 1.1f)
            };
        }
        
        private float3 MinCorner => _localTransform.ValueRO.Position - HalfDimensions;
        private float3 MaxCorner => _localTransform.ValueRO.Position + HalfDimensions;
        private float3 HalfDimensions => new()
        {
            x = _spaceDimensions.ValueRO.Value.x * 0.5f,
            y = _spaceDimensions.ValueRO.Value.y * 0.5f,
            z = 0f
        };

        private float GetRandomScale(float min, float max) => _spaceRandom.ValueRW.Value.NextFloat(min, max);

        private quaternion GetRandomRotation() => quaternion.RotateZ(_spaceRandom.ValueRW.Value.NextFloat(0f, 360f));

        private float3 GetRandomSpacePosition()
        {
            float3 randomPosition;

            do
            {
                randomPosition = _spaceRandom.ValueRW.Value.NextFloat3(MinCorner, MaxCorner);
            } while (math.distancesq(_localTransform.ValueRO.Position, randomPosition) <= ShipSafetyRadius);

            return randomPosition;
        }
        
        public float AsteroidSpawnTimer
        {
            get => _asteroidSpawnTimer.ValueRO.Timer;
            set => _asteroidSpawnTimer.ValueRW.Timer = value;
        }
        
        public bool CanSpawnAsteroid => AsteroidSpawnTimer <= 0f;

        public float AsteroidSpawnRate
        {
            get => _spaceSpawnFrequencies.ValueRO.AsteroidFrequency;
            set
            {
                _spaceSpawnFrequencies.ValueRW.AsteroidFrequency = value;
                _spaceSpawnFrequencies.ValueRW.AsteroidFrequency = math.max(_spaceSpawnFrequencies.ValueRO.AsteroidFrequency, 0.001f);
            }
        }

        public Entity GetRandomAsteroidPrefab()
        {
            uint indexPicker = _spaceRandom.ValueRW.Value.NextUInt(1, 3);
            switch (indexPicker)
            {
                case 1:
                    return _spacePrefabs.ValueRO.SmallAsteroid;
                
                case 2:
                    return _spacePrefabs.ValueRO.BigAsteroid;
                
                default:
                    return _spacePrefabs.ValueRO.SmallAsteroid;
            }
        }
    }
}