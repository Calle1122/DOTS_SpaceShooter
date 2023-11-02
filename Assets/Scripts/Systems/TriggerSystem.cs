/*using Aspects;
using ComponentsAndTags;
using Unity.Burst;
using Unity.Entities;
using Unity.Collections;
using Unity.Physics;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct TriggerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.TempJob);
        var pAspect = SystemAPI.GetAspect<PlayerAspect>(SystemAPI.GetSingletonEntity<PlayerTag>());

        var j = new ProcessTriggerEventsJob {
            OtherTag = SystemAPI.GetComponentLookup<AsteroidTag>(isReadOnly: true),
            PlayerTag = SystemAPI.GetComponentLookup<PlayerTag>(isReadOnly: true),
            Ecb = ecb
        };

        state.Dependency = j.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);
        state.Dependency.Complete();

        ecb.Playback(state.EntityManager);
    }

    [BurstCompile]
    public partial struct ProcessTriggerEventsJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<AsteroidTag> OtherTag;
        [ReadOnly] public ComponentLookup<PlayerTag> PlayerTag;

        public EntityCommandBuffer Ecb;

        [BurstCompile]
        public void Execute(Unity.Physics.TriggerEvent ce)
        {
            var entityA = ce.EntityA;
            var entityB = ce.EntityB;

            if (OtherTag.HasComponent(entityA) && PlayerTag.HasComponent(entityB))
            {
                Ecb.DestroyEntity(entityA);
            }

            if (OtherTag.HasComponent(entityB) && PlayerTag.HasComponent(entityA))
            {
                Ecb.DestroyEntity(entityB);
            }
        }
    }
}*/
