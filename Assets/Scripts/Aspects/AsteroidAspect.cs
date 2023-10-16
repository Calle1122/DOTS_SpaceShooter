using Properties;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Utilities;

namespace Aspects
{
    public readonly partial struct AsteroidAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<SpaceProperties.AsteroidMovement> _asteroidMovement;
        
        public void MoveTowardsPlayer(float deltaTime)
        {
            _localTransform.ValueRW.Position += MathHelper.GetHeading(_localTransform.ValueRO.Position, new float3(0, 0, 0)) * _asteroidMovement.ValueRO.MoveSpeed * deltaTime;
        }

        public bool HasReachedPlayer => math.distancesq(_localTransform.ValueRO.Position, new float3(0, 0, 0)) <= .5f;
    }
}