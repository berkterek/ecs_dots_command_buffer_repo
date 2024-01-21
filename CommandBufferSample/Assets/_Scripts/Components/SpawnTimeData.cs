using Unity.Entities;

namespace Sample1
{
    public struct SpawnTimeData : IComponentData
    {
        public float CurrentTime;
        public float MaxTime;
    }
}