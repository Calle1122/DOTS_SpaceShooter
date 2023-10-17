using ComponentsAndTags;
using Properties;
using Unity.Entities;
using Unity.Transforms;

namespace Aspects
{
    public readonly partial struct PlayerMoveAspect : IAspect
    {
        public readonly Entity Entity;
        
        private readonly RefRW<LocalTransform> _localTransform;
        private readonly RefRO<PlayerProperties.PlayerMovement> _playerMovement;
        private readonly RefRO<PlayerMoveInput> _playerMoveInput;

        public void Move(float deltaTime)
        {
            _localTransform.ValueRW = _localTransform.ValueRW.RotateZ(_playerMoveInput.ValueRO.Value.x * _playerMovement.ValueRO.MoveSpeed * deltaTime);
        }
    }
}