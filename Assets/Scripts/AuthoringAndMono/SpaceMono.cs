using ComponentsAndTags;
using Properties;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace AuthoringAndMono
{
    public class SpaceMono : MonoBehaviour
    {
        public float2 spaceDimensions;
        public uint randomSeed;
        public float shipSafetyRadius;
        public float asteroidSpawnFrequency;
        
        //TODO: add enemy ships
        /*public GameObject smallEnemyPrefab;
        public GameObject bigEnemyPrefab;*/
        
        public GameObject smallAsteroidPrefab;
        public GameObject bigAsteroidPrefab;
    }

    public class SpaceBaker : Baker<SpaceMono>
    {
        public override void Bake(SpaceMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpaceProperties.SpaceDimensions
            {
                Value = authoring.spaceDimensions
            });
            AddComponent(entity, new SpaceProperties.SpacePrefabs
            {
                SmallAsteroid = GetEntity(authoring.smallAsteroidPrefab, TransformUsageFlags.Dynamic),
                BigAsteroid = GetEntity(authoring.bigAsteroidPrefab, TransformUsageFlags.Dynamic)
            });
            AddComponent(entity, new SpaceProperties.ShipSafetyRadius
            {
                Value = authoring.shipSafetyRadius
            });
            AddComponent(entity, new SpaceProperties.SpaceSpawnFrequencies
            {
                AsteroidFrequency = authoring.asteroidSpawnFrequency
            });
            AddComponent(entity, new SpaceProperties.AsteroidsSpawnTimer
            {
                Timer = authoring.asteroidSpawnFrequency
            });
            AddComponent(entity, new SpaceRandom
            {
                Value = Random.CreateFromIndex(authoring.randomSeed)
            });
        }
    }
}