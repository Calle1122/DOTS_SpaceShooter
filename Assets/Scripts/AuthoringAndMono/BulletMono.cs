using ComponentsAndTags;
using Properties;
using Unity.Entities;
using UnityEngine;

namespace AuthoringAndMono
{
    public class BulletMono : MonoBehaviour
    {
        public float baseDamage;
        public float bulletSpeed; 
    }

    public class BulletBaker : Baker<BulletMono>
    {
        public override void Bake(BulletMono authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new BulletProperties.BulletSpeed
            {
                Value = authoring.bulletSpeed
            });
            AddComponent(entity, new BulletProperties.BulletDamage
            {
                Value = authoring.baseDamage
            });
            AddComponent(entity, new IsDestroyedTag());
            SetComponentEnabled<IsDestroyedTag>(entity, false);
        }
    }
}