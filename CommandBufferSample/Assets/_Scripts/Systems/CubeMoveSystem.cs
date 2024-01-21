using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Sample1
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct CubeMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            float3 direction = new float3(0f, 0f, 1f);
            foreach (var (moveData, localTransform) in SystemAPI.Query<RefRO<MoveData>, RefRW<LocalTransform>>())
            {
                localTransform.ValueRW.Position += deltaTime * moveData.ValueRO.Speed * direction;
            }
        }
    }
}