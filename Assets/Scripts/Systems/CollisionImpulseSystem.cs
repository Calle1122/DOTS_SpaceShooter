/*using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace Systems
{
    [UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
    [UpdateAfter(typeof(PhysicsSystemGroup))]
    public class CollisionImpulseSystem : JobComponentSystem 
    {
        [BurstCompile]
        private struct CollisionJob : ICollisionEventsJob {
            public void Execute(CollisionEvent collisionEvent) {
                Debug.Log($"Collision between entities { collisionEvent.EntityA.Index } and { collisionEvent.EntityB.Index }");
            }
        }

        private BuildPhysicsWorld buildPhysicsWorldSystem;
        private StepPhysicsWorld stepPhysicsWorldSystem;
        private EndSimulationEntityCommandBufferSystem commandBufferSystem;

        protected override void OnCreate() {
            base.OnCreate();
            buildPhysicsWorldSystem = World.GetExistingSystem<BuildPhysicsWorld>();
            stepPhysicsWorldSystem = World.GetExistingSystem<StepPhysicsWorld>();
            commandBufferSystem = World.GetExistingSystem<EndSimulationEntityCommandBufferSystem>();
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps) {    
            JobHandle jobHandle = new CollisionJob().Schedule(
                stepPhysicsWorldSystem.Simulation, 
                ref buildPhysicsWorldSystem.PhysicsWorld, 
                inputDeps);    
            commandBufferSystem.AddJobHandleForProducer(jobHandle);    
            return jobHandle;    
        }
    }
}*/