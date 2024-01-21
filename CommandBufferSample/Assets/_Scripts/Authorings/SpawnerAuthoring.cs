using UnityEngine;
using Unity.Entities;

namespace Sample1
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public Vector3 Position;
        
        private class SpawnerBaker : Baker<SpawnerAuthoring>
        {
            public override void Bake(SpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent(entity, new SpawnTimeData()
                {
                    CurrentTime = 0f,
                    MaxTime = 1.5f
                });

                AddComponent(entity, new SpawnEntityData()
                {
                    Position = authoring.Position,
                    Entity = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }    
}