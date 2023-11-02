using ComponentsAndTags;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct PlayerAspect : IAspect
    {
        public readonly Entity Entity;

        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<PlayerTag> _tag;
        private readonly RefRW<Health> _health;
        private readonly RefRW<Score> _score;
        
        public float3 Position => _localTransform.ValueRO.Position;

        public int Health
        {
            get => _health.ValueRO.Value;
            set => _health.ValueRW.Value = value;
        }

        public int Score
        {
            get => _score.ValueRO.Value;
            set => _score.ValueRW.Value = value;
        }
    }
}