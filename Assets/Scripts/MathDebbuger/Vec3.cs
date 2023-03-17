using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace CustomMath
{
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Variables
        public float x;
        public float y;
        public float z;

        public float sqrMagnitude { get { return (x * x + y * y + z * z); } }
        public Vector3 normalized { get { return new Vec3(x * sqrMagnitude, y * sqrMagnitude, z * sqrMagnitude); } }
        public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } }
        #endregion

        #region constants
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
            return new Vec3(v3) * scalar;
        }
        public static Vec3 operator *(float scalar, Vec3 v3)
        {
            return new Vec3(v3) * scalar;

        }
        public static Vec3 operator /(Vec3 v3, float scalar)
        {
            return new Vec3(v3) / scalar;
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
        public static float Angle(Vec3 from, Vec3 to)
        {
            throw new NotImplementedException();
        }
        public static Vec3 ClampMagnitude(Vec3 vector, float maxLength)
        {
            if (vector.magnitude > maxLength)
            {

                return vector*maxLength;
            }
                return vector;
        }
        public static float Magnitude(Vec3 vector)
        {
            return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
        }
        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            float i = (a.y * b.z) - (a.z * b.y);
            float j = (a.x * b.z) - (a.z * b.x);
            float k = (a.x * b.y) - (a.y * b.x);
            return new Vec3(i, -j, k);
        }
        public static float Distance(Vec3 a, Vec3 b)
        {

            float distX = a.x - b.x;
            float distY = a.y - b.y;
            float distZ = a.z - b.z;

            return Mathf.Sqrt((distX * distX) + (distY * distY) + (distZ * distZ));
        }
        public static float Dot(Vec3 a, Vec3 b)
        {
            return 0;
        }
        public static Vec3 Lerp(Vec3 a, Vec3 b, float t)
        {
            throw new NotImplementedException();
        }
        public static Vec3 LerpUnclamped(Vec3 a, Vec3 b, float t)
        {
            throw new NotImplementedException();
        }
        public static Vec3 Max(Vec3 a, Vec3 b)
        {
            throw new NotImplementedException();
        }
        public static Vec3 Min(Vec3 a, Vec3 b)
        {
            throw new NotImplementedException();
        }
        public static float SqrMagnitude(Vec3 vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }
        public static Vec3 Project(Vec3 vector, Vec3 onNormal)
        {
            throw new NotImplementedException();
        }
        public static Vec3 Reflect(Vec3 inDirection, Vec3 inNormal)
        {
            throw new NotImplementedException();
        }
        public void Set(float newX, float newY, float newZ)
        {
            x = newX;
            y = newY;
            z = newZ;
        }
        public void Scale(Vec3 scale)
        {
            throw new NotImplementedException();
        }
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