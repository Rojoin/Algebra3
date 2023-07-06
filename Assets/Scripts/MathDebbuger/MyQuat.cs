
using System;
using UnityEngine;
using UnityEngine.Internal;

namespace CustomMath
{
    [Serializable]
    public struct MyQuat : IEquatable<MyQuat>
    {
        public float x;
        public float y;
        public float z;
        /// <summary>
        /// Que tan desplazado esta del origen
        /// </summary>
        public float w;
        public const double kEpsilon = 1E-06;


        public static MyQuat identity => new MyQuat(0, 0, 0, 1);


        public MyQuat(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }
        //TODO
        public static MyQuat Inverse(Quaternion rotation)
        {
            return new MyQuat(-rotation.x, -rotation.y, -rotation.z, -rotation.w);
        }
        //TODO
        public static MyQuat Slerp(MyQuat a, MyQuat b, float t)
        {
            return identity;
        }
        //TODO

        public static MyQuat SlerpUnclamped(MyQuat a, MyQuat b, float t)
        {
            return identity;
        }

        public static MyQuat LerpUnclamped(MyQuat a, MyQuat b, float t)
        {
            if (Dot(a, b) < 0)
            {
                b.Set(-b.x, -b.y, -b.z, -b.w);
            }
            MyQuat toReturn;
            toReturn.x = a.x + t * (b.x - a.x);
            toReturn.y = a.y + t * (b.y - a.y);
            toReturn.z = a.z + t * (b.z - a.z);
            toReturn.w = a.w + t * (b.w - b.w);

            return toReturn.normalized;
        }

        public static MyQuat Lerp(MyQuat a, MyQuat b, float t)
        {
            if (t < 0) t = 0;
            if (t > 1) t = 1;
            return LerpUnclamped(a, b, t);
        }
        //TODO
        public static MyQuat AngleAxis(float angle, Vec3 axis)
        {
            return identity;
        }
        //TODO
        public static MyQuat LookRotation(Vec3 forward, [DefaultValue("Vec3.up")] Vec3 upwards)
        {
            forward.Normalize();

            Vec3 vector2 = Vec3.Cross(upwards, forward).normalized;
            Vec3 vector3 = Vec3.Cross(forward, vector2);
            

            float rotation = (vector2.x + vector3.y) + forward.z;
            var quaternion = new MyQuat();
            if (rotation > 0f)
            {
                var cos = (float)Math.Sqrt(rotation + 1f);
                quaternion.w = cos * 0.5f;
                cos = 0.5f / cos;
                quaternion.x = (vector3.z - forward.y) * cos;
                quaternion.y = (forward.x - vector2.z) * cos;
                quaternion.z = (vector2.y - vector3.x) * cos;
                return quaternion;
            }
            if ((vector2.x >= vector3.y) && (vector2.x >= forward.z))
            {
                var num7 = (float)Math.Sqrt(((1f + vector2.x) - vector3.y) - forward.z);
                var num4 = 0.5f / num7;
                quaternion.x = 0.5f * num7;
                quaternion.y = (vector2.y + vector3.x) * num4;
                quaternion.z = (vector2.z + forward.x) * num4;
                quaternion.w = (vector3.z - forward.y) * num4;
                return quaternion;
            }
            if (vector3.y > forward.z)
            {
                var num6 = (float)Math.Sqrt(((1f + vector3.y) - vector2.x) - forward.z);
                var num3 = 0.5f / num6;
                quaternion.x = (vector3.x + vector2.y) * num3;
                quaternion.y = 0.5f * num6;
                quaternion.z = (forward.y + vector3.z) * num3;
                quaternion.w = (forward.x - vector2.z) * num3;
                return quaternion;
            }
            var num5 = (float)Math.Sqrt(((1f + forward.z) - vector2.x) - vector3.y);
            var num2 = 0.5f / num5;
            quaternion.x = (forward.x + vector2.z) * num2;
            quaternion.y = (forward.y + vector3.z) * num2;
            quaternion.z = 0.5f * num5;
            quaternion.w = (vector2.y - vector3.x) * num2;
            return quaternion;

        }
        //TODO
        public static MyQuat LookRotation(Vec3 forward)
        {

            MyQuat toReturn = identity;
            if (forward.magnitude < kEpsilon)
            {
                return identity;
            }
            return identity;
        }

        //   public float this[int index] {  get{} ; set{} ;}

        /// <summary>
        /// Multiplico Todo
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static MyQuat operator *(MyQuat lhs, MyQuat rhs)
        {
            float x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
            float y = lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z;
            float z = lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x;
            float w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;

            return new MyQuat(x, y, z, w);
        }

        public static Vec3 operator *(MyQuat rotation, Vec3 point)
        {
            Vec3 quatVec3 = new Vec3(rotation.x, rotation.y, rotation.z);
            float scalar = rotation.w;
            // Quaternion al cuadrado - modulo de quaternion * el vector a rotar + 2 veces el producto punto del quaternion entre el quaternion
            // y el vector + 2 veces el quaternion por el producto cruz entre el quaternion y el vector
            return 2.0f * Vec3.Dot(quatVec3, point) * quatVec3 + (scalar * scalar - Vec3.Dot(quatVec3, quatVec3))
                * point + 2.0f * scalar * Vec3.Cross(quatVec3, point);
        }
        public static implicit operator Quaternion(MyQuat quaternion)
        {
            return new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }

        public static bool operator ==(MyQuat lhs, MyQuat rhs)
        {
            return (lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w);
        }

        public static bool operator !=(MyQuat lhs, MyQuat rhs)
        {
            return !(lhs == rhs);
        }

        public static float Dot(MyQuat a, MyQuat b)
        {
            return (a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w);
        }

        public void SetLookRotation(Vec3 view)
        {

        }

        public void SetLookRotation(Vec3 view, [DefaultValue("Vec3.Up")] Vec3 up)
        {

        }


        public static float Angle(MyQuat a, MyQuat b)
        {
            if (a.magnitude == 0 || b.magnitude == 0)
            {
                return 0;
            }

            return Mathf.Acos(Mathf.Abs(Dot(a, b)) * Mathf.Rad2Deg / (a.magnitude * b.magnitude));
        }

        public Vec3 eulerAngles
        {
            get
            {
                //Calculamos x & z separadas porque si se rotan entre si puede producir el gymballock
                //Calculamos la  x en base a y
                float sinr_cosp = 2 * (this.w * this.x + this.y * this.z);
                float cosr_cosp = 1 - 2 * (this.x * this.x + this.y * this.y);
                float x = Mathf.Atan2(sinr_cosp, cosr_cosp);

                //Calculamos y sola ya que es independiente 
                float sinp = Mathf.Sqrt(1 + 2 * (this.w * this.y - this.x * this.z));
                float cosp = Mathf.Sqrt(1 - 2 * (this.w * this.y - this.x * this.z));
                float y = 2 * Mathf.Atan2(sinp, cosp) - Mathf.Rad2Deg;

                //Calculamos la  z en base a y
                float siny_cosp = 2 * (this.w * this.z + this.x * this.y);
                float cosy_cosp = 1 - 2 * (this.y * this.y + this.z * this.z);
                float z = Mathf.Atan2(siny_cosp, cosy_cosp);
                return new Vec3(x, y, z);
            }
            set => this = MyQuat.Euler(value);
        }

        //Calculamos un quaternion en base a las rotaciones
        public static MyQuat Euler(float x, float y, float z)
        {
            float sin;// calculamos la parte imaginaria
            float cos;// calculamos la parte real
            MyQuat qX;
            MyQuat qY;
            MyQuat qZ;
            MyQuat toReturn = identity;

            sin = Mathf.Sin(Mathf.Deg2Rad * x * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * x * 0.5f);
            qX = new MyQuat(sin, 0, 0, cos);

            sin = Mathf.Sin(Mathf.Deg2Rad * y * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * y * 0.5f);
            qY = new MyQuat(0, sin, 0, cos);

            sin = Mathf.Sin(Mathf.Deg2Rad * z * 0.5f);
            cos = Mathf.Cos(Mathf.Deg2Rad * z * 0.5f);
            qZ = new MyQuat(0, 0, sin, cos);

            toReturn = (qX * qY) * qZ;

            return toReturn;

        }

        public static MyQuat Euler(Vec3 euler)
        {
            return Euler(euler.x, euler.y, euler.z);
        }
        //TODO
        public static MyQuat RotateTowards(MyQuat from, MyQuat to, float maxDegreesDelta)
        {
            return identity;
        }

        public static MyQuat Normalize(MyQuat q)
        {
            float mag = q.magnitude;
            return new MyQuat(q.x / mag, q.y / mag, q.z / mag, q.w / mag);
        }

        public void Normalize()
        {
            this = Normalize(this);
        }

        public MyQuat normalized => Normalize(this);
        //TODO
        public void SetFromToRotation(Vec3 fromDirection, Vec3 toDirection)
        {

        }
        //TODO
        public static MyQuat FromRotation(Vec3 fromDirection, Vec3 toDirection)
        {
            return identity;
        }

        public void Set(float newX, float newY, float newZ, float newW)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
            this.w = newW;
        }
        public bool Equals(MyQuat other)
        {
            return w.Equals(other.w) && x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        public override bool Equals(object obj)
        {
            return obj is MyQuat other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(w, x, y, z);
        }
        public float magnitude
        {
            get
            {
                return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2) + Mathf.Pow(w, 2));
            }
        }
    }
}