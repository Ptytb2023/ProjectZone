using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

using Random = UnityEngine.Random;

namespace Extensions
{
    public static class Vector2Extension
    {
        public static Vector2 GetVector2Direction(this Vector2 from, Vector2 to)
        {
            Vector2 direction = to - from;

            return direction.normalized;
        }

        public static Vector2 GetDirection(this Transform transform)
        {
            float angle = transform.eulerAngles.z;

            return CalculateDirection(angle);
        }

        public static Vector2 GetVector2DirectionRange(this Transform transform, float minMax, float maxAngle)
        {
            float angle = transform.eulerAngles.z;

            angle += Random.Range(minMax, maxAngle);

            return CalculateDirection(angle);
        }

        private static Vector2 CalculateDirection(float angle)
        {
            return new Vector2(
                    Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad));
        }
    }
}
