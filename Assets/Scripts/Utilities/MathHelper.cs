using Unity.Mathematics;

namespace Utilities
{
    public static class MathHelper
    {
        public static float3 GetHeading(float3 objectPosition, float3 targetPosition)
        {
            return math.normalize(targetPosition - objectPosition);
        }
    }
}