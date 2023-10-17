using System.Runtime.InteropServices;
using Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace Systems
{
    [UpdateBefore(typeof(TransformSystemGroup))]
    [BurstCompile]
    public partial struct PlayerMoveSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            new PlayerMoveJob
            {
                DeltaTime = deltaTime
            }.Run();
        }
    }
    
    [BurstCompile]
    [StructLayout(LayoutKind.Auto)]
    
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;

        [BurstCompile]
        private void Execute(PlayerMoveAspect player)
        {
            player.Move(DeltaTime);
        }
    }
}