using Properties;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class AsteroidMono : MonoBehaviour
    {
        public float moveSpeed;
        public float rotationSpeed;
    }

    public class AsteroidBaker : Baker<AsteroidMono>
    {
        public override void Bake(AsteroidMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new SpaceProperties.AsteroidMovement
            {
                MoveSpeed = authoring.moveSpeed,
                RotationSpeed = authoring.rotationSpeed
            });
        }
    }
}