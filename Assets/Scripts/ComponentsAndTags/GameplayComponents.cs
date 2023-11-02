using Unity.Entities;

namespace ComponentsAndTags
{
    public struct Health : IComponentData
    {
        public int Value;
    }

    public struct Score : IComponentData
    {
        public int Value;
    }
}