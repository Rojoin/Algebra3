using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace CustomMath
{
    [Serializable]
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude { get { return (x * x + y * y + z * z); } }
        public Vec3 normalized { get { return new Vec3(x / magnitude, y / magnitude, z / magnitude); } }
        public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
        #endregion

        #region constants
        //Llegado a 0
        public const float epsilon = 1e-05f;
        #endregion

        #region Default Values
        public static Vec3 Zero { get { return new Vec3(0.0f, 0.0f, 0.0f); } }
        public static Vec3 One { get { return new Vec3(1.0f, 1.0f, 1.0f); } }
        public static Vec3 Forward { get { return new Vec3(0.0f, 0.0f, 1.0f); } }
        public static Vec3 Back { get { return new Vec3(0.0f, 0.0f, -1.0f); } }
        public static Vec3 Right { get { return new Vec3(1.0f, 0.0f, 0.0f); } }
        public static Vec3 Left { get { return new Vec3(-1.0f, 0.0f, 0.0f); } }
        public static Vec3 Up { get { return new Vec3(0.0f, 1.0f, 0.0f); } }
        public static Vec3 Down { get { return new Vec3(0.0f, -1.0f, 0.0f); } }
        public static Vec3 PositiveInfinity { get { return new Vec3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity); } }
        public static Vec3 NegativeInfinity { get { return new Vec3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity); } }
        #endregion                                                                                                                                                                               

        #region Constructors
        public Vec3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0.0f;
        }

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3(Vec3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector3 v3)
        {
            this.x = v3.x;
            this.y = v3.y;
            this.z = v3.z;
        }

        public Vec3(Vector2 v2)
        {
            this.x = v2.x;
            this.y = v2.y;
            this.z = 0.0f;
        }
        #endregion

        #region Operators
        public static bool operator ==(Vec3 left, Vec3 right)
        {
            float diff_x = left.x - right.x;
            float diff_y = left.y - right.y;
            float diff_z = left.z - right.z;
            float sqrmag = diff_x * diff_x + diff_y * diff_y + diff_z * diff_z;
            return sqrmag < epsilon * epsilon;
     

        }
        public static bool operator !=(Vec3 left, Vec3 right)
        {
            return !(left == right);
        }

        public static Vec3 operator +(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x + rightV3.x, leftV3.y + rightV3.y, leftV3.z + rightV3.z);
        }

        public static Vec3 operator -(Vec3 leftV3, Vec3 rightV3)
        {
            return new Vec3(leftV3.x - rightV3.x, leftV3.y - rightV3.y, leftV3.z - rightV3.z);

        }

        public static Vec3 operator -(Vec3 v3)
        {
            return new Vec3(-v3.x, -v3.y, -v3.z);
        }

        public static Vec3 operator *(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(v3.x * scalar, v3.y * scalar, v3.z * scalar);
        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3.x / scalar, v3.y / scalar, v3.z / scalar);
        }

        public static implicit operator Vector3(Vec3 v3)
        {
            return new Vector3(v3.x, v3.y, v3.z);
        }
     

        public static implicit operator Vector2(Vec3 v2)
        {
            return new Vector2(v2.x, v2.y);
        }
        #endregion

        #region Functions
        public override string ToString()
        {
            return "X = " + x.ToString() + "   Y = " + y.ToString() + "   Z = " + z.ToString();
        }
        //https://answers.unity.com/questions/1294512/how-vectorangle-works-internally-in-unity.html
        //Dot nos da el coseno
        public static float Angle(Vec3 from, Vec3 to)
        {
            return Mathf.Acos(Mathf.Clamp(Dot(from.normalized, to.normalized), -1f, 1f)) * 180 / Mathf.PI;
        }
        //http://speace.chenjianqiu.ltd/unity2019_3/ScriptReference/Vector3.ClampMagnitude.html
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            return vector.magnitude > maxLength ? vector.normalized * maxLength : vector;
        }
        //https://forum.unity.com/threads/sqrmagnitude-or-magnitude.80443/
        public static float Magnitude(Vec3 vector)
        {
            return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        public static Vec3 Cross(Vec3 a, Vec3 b) // Vector perpendicular a los dos que le paso en sentido horario
        {
            float i = (a.y * b.z) - (a.z * b.y);
            float j = (a.x * b.z) - (a.z * b.x);
            float k = (a.x * b.y) - (a.y * b.x);
            return new Vec3(i, -j, k);
        }
        public static float Distance(Vec3 a, Vec3 b)
        {

            float distX = b.x - a.x;
            float distY = b.y - a.y;
            float distZ = b.z - a.z;

            return Mathf.Sqrt((distX * distX) + (distY * distY) + (distZ * distZ));
        }
        // http://www.sunshine2k.de/articles/coding/vectorreflection/vectorreflection.html#DotProduct
        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }
        //http://speace.chenjianqiu.ltd/unity2019_3/ScriptReference/Vector3.Lerp.html
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            Vec3 direction = (b - a);
            if (t < 0) t = 0;
            if (t > 1) t = 1;
            return a + (  direction*t);
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            Vec3 direction = (b - a);
            return a + ( direction*t );
        }
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            float maxX = a.x > b.x ? a.x : b.x;
            float maxY = a.y > b.y ? a.y : b.y;
            float maxZ = a.z > b.z ? a.z : b.z;
            return new Vec3(maxX, maxY, maxZ);
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            float minX = a.x < b.x ? a.x : b.x;
            float minY = a.y < b.y ? a.y : b.y;
            float minZ = a.z < b.z ? a.z : b.z;
            return new Vec3(minX, minY, minZ);
        }
        public static float SqrMagnitude(Vec3 vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }
        // EL dot nos da el coceno , lo multiplicamos por la normal  para llevarlo a esa altura y le modificamos su magnitude con la sqrMag
        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            float sqrMag = Dot(onNormal, onNormal);
            if (sqrMag < epsilon)
            {
                return Zero;
            }
            else
            {
                float dot = Dot(vector, onNormal);
                return onNormal * dot / sqrMag;
            }

        }
        //Vector choca con una normal y Conseguis la perpendicular al plano.
        //Dot da el angulo y -2 nos da el doble del angulo mirando para el otro lado
        //Al hacerlo por la normal del plano nos da el plano normaliza y despeus lo multiplicamos por el original para tener el reflejado
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            return inDirection - 2 * (Dot(inDirection, inNormal)) * inNormal;
        }
        public void Set(float newX, float newY, float newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }
        //https://docs.unity3d.com/ScriptReference/Vector3.Scale.html
        public void Scale(Vec3 scale)
        {
            x *= scale.x;
            y *= scale.y;
            z *= scale.z;
        }
        //https://www.khanacademy.org/computing/computer-programming/programming-natural-simulations/programming-vectors/a/vector-magnitude-normalization#:~:text=To%20normalize%20a%20vector%2C%20therefore,the%20unit%20vector%20readily%20accessible.
        public void Normalize()
        {
            float mag = Magnitude(this);
            x = x / mag;
            y = y / mag;
            z = z / mag;
        }
        #endregion

        #region Internals
        public override bool Equals(object other)
        {
            if (!(other is Vec3)) return false;
            return Equals((Vec3)other);
        }

        public bool Equals(Vec3 other)
        {
            return x == other.x && y == other.y && z == other.z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2);
        }
        #endregion
    }
}