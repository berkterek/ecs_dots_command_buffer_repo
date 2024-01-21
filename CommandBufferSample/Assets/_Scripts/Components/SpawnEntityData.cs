using Unity.Entities;
using Unity.Mathematics;

namespace Sample1
{
    public struct SpawnEntityData : IComponentData
    {
        public float3 Position;
        public Entity Entity;
    }
}