using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    public partial struct SpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BeginSimulationEntityCommandBufferSystem.Singleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var entityCommandBufferSystem =
                SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            foreach (var (spawnEntityData, spawnTimeData) in SystemAPI.Query<RefRO<SpawnEntityData>, RefRW<SpawnTimeData>>())
            {
                spawnTimeData.ValueRW.CurrentTime += deltaTime;
                if(spawnTimeData.ValueRO.CurrentTime < spawnTimeData.ValueRO.MaxTime) continue;
                spawnTimeData.ValueRW.CurrentTime = 0f;
            
                var entity = entityCommandBuffer.Instantiate(spawnEntityData.ValueRO.Entity);
                entityCommandBuffer.SetComponent(entity, new LocalTransform()
                {
                    Position = spawnEntityData.ValueRO.Position,
                    Rotation = quaternion.identity,
                    Scale = 1f
                });
            }
        }

        private void NotRecommended()
        {
            // foreach (var (spawnEntityData, spawnTimeData) in SystemAPI.Query<RefRO<SpawnEntityData>, RefRW<SpawnTimeData>>())
            // {
            //     spawnTimeData.ValueRW.CurrentTime += deltaTime;
            //     if(spawnTimeData.ValueRO.CurrentTime < spawnTimeData.ValueRO.MaxTime) continue;
            //     spawnTimeData.ValueRW.CurrentTime = 0f;
            //
            //     var entity = state.EntityManager.Instantiate(spawnEntityData.ValueRO.Entity);
            //     state.EntityManager.SetComponentData(entity, new LocalTransform()
            //     {
            //         Position = spawnEntityData.ValueRO.Position,
            //         Rotation = quaternion.identity,
            //         Scale = 1f
            //     });
            // }
        }

        private void CustomCommandBuffer()
        {
            // var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            // foreach (var (spawnEntityData, spawnTimeData) in SystemAPI.Query<RefRO<SpawnEntityData>, RefRW<SpawnTimeData>>())
            // {
            //     spawnTimeData.ValueRW.CurrentTime += deltaTime;
            //     if(spawnTimeData.ValueRO.CurrentTime < spawnTimeData.ValueRO.MaxTime) continue;
            //     spawnTimeData.ValueRW.CurrentTime = 0f;
            //
            //     var entity = entityCommandBuffer.Instantiate(spawnEntityData.ValueRO.Entity);
            //     entityCommandBuffer.SetComponent(entity, new LocalTransform()
            //     {
            //         Position = spawnEntityData.ValueRO.Position,
            //         Rotation = quaternion.identity,
            //         Scale = 1f
            //     });
            // }
            //
            // entityCommandBuffer.Playback(state.EntityManager);
            // entityCommandBuffer.Dispose();
        }

        private void ReadyToUseCommandBuffer()
        {
            // var entityCommandBufferSystem =
            //     SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            // var entityCommandBuffer = entityCommandBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            // foreach (var (spawnEntityData, spawnTimeData) in SystemAPI.Query<RefRO<SpawnEntityData>, RefRW<SpawnTimeData>>())
            // {
            //     spawnTimeData.ValueRW.CurrentTime += deltaTime;
            //     if(spawnTimeData.ValueRO.CurrentTime < spawnTimeData.ValueRO.MaxTime) continue;
            //     spawnTimeData.ValueRW.CurrentTime = 0f;
            //
            //     var entity = entityCommandBuffer.Instantiate(spawnEntityData.ValueRO.Entity);
            //     entityCommandBuffer.SetComponent(entity, new LocalTransform()
            //     {
            //         Position = spawnEntityData.ValueRO.Position,
            //         Rotation = quaternion.identity,
            //         Scale = 1f
            //     });
            // }
        }
    }
}