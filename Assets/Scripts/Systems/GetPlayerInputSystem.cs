using ComponentsAndTags;
using Unity.Entities;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

namespace Systems
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class GetPlayerInputSystem : SystemBase
    {
        private PlayerActions _playerActions;
        private Entity _playerEntity;

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerMoveInput>();

            _playerActions = new PlayerActions();
        }

        protected override void OnStartRunning()
        {
            _playerActions.Enable();
            _playerActions.PlayerMap.PlayerShoot.performed += OnPlayerShoot;
            _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        protected override void OnUpdate()
        {
            var currentMoveInput = _playerActions.PlayerMap.PlayerMovement.ReadValue<Vector2>();
            
            SystemAPI.SetSingleton(new PlayerMoveInput
            {
                Value = currentMoveInput
            });
        }

        protected override void OnStopRunning()
        {
            _playerActions.PlayerMap.PlayerShoot.performed -= OnPlayerShoot;
            _playerActions.Disable();

            _playerEntity = Entity.Null;
        }

        private void OnPlayerShoot(InputAction.CallbackContext obj)
        {
            if (!SystemAPI.Exists(_playerEntity)) return;
            
            SystemAPI.SetComponentEnabled<FireProjectileTag>(_playerEntity, true);
        }
    }
}