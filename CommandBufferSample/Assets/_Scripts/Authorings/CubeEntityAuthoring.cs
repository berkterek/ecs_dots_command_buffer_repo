using Unity.Entities;
using UnityEngine;

namespace Sample1
{
    public class CubeEntityAuthoring : MonoBehaviour
    {
        private class CubeEntityBaker : Baker<CubeEntityAuthoring>
        {
            public override void Bake(CubeEntityAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<CubeTag>(entity);

                AddComponent(entity, new MoveData()
                {
                    Speed = 2f
                });
            }
        }
    }    
}

