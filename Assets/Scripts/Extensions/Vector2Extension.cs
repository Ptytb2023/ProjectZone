using UnityEngine;

using Random = UnityEngine.Random;

namespace Extensions
{
    public static class Vector2Extension
    {
        public static Vector2 GetVector2Direction(this Transform from, Transform to)
        {
            Vector2 direction = to.position - from.position;

            return direction.normalized;
        }

        public static Vector2 GetDirectionForward(this Transform transform)
        {
            float angle = transform.eulerAngles.z;

            return CalculateDirectionForward(angle);
        }

        public static Vector2 GetVector2DirectionForwardRange(this Transform transform, float minMax, float maxAngle)
        {
            float angle = transform.eulerAngles.z;

            angle += Random.Range(minMax, maxAngle);

            return CalculateDirectionForward(angle);
        }

        private static Vector2 CalculateDirectionForward(float angle)
        {
            return new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad));
        }
    }
}
