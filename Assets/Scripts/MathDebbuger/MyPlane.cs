using System;
using System.Collections;
using UnityEngine;

using UnityEngine;
using System;

namespace CustomMath
{
    [Serializable]
    public struct MyPlane
    {//https://github.com/Unity-Technologies/UnityCsReference/blob/master/Runtime/Export/Geometry/Plane.cs

        public Vec3 normal;
        public Vec3 verA;
        public Vec3 verB;
        public Vec3 verC;

        public float distance;
        public MyPlane flipped => new(-normal, -distance);

        public MyPlane(Vec3 inNormal, Vec3 inPoint)
        {
            this.normal = inNormal.normalized;
            this.distance = 0f + Vec3.Dot(inNormal, inPoint);
            verA = inPoint;
            verB = inPoint;
            verC = inPoint;
        }

        public MyPlane(Vec3 inNormal, float d)
        {
            this.normal = inNormal.normalized;
            this.distance = d;
            verA =normal;
            verB =normal;
            verC =normal;
        }


        public MyPlane(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = Vec3.Cross(b - a, c - a).normalized;
            this.distance = -Vec3.Dot(this.normal, a);
            verA = a;
            verB = b;
            verC = c;
        }
        public static bool operator ==(MyPlane left, MyPlane right)
        {

            return left.normal == right.normal && left.distance == right.distance;
        }

        public static bool operator !=(MyPlane left, MyPlane right)
        {
            return !(left == right);
        }
        public static MyPlane Zero { get { return new MyPlane(Vec3.Zero, 0); } }
        public void SetNormalAndPosition(Vec3 inNormal, Vec3 inPoint)
        {
            this.normal = inNormal.normalized;
            this.distance = 0f + Vec3.Dot(inNormal, inPoint);
        }


        public void Set3Points(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = Vec3.Cross(b - a, c - a).normalized;
            this.distance = -Vec3.Dot(this.normal, a);
        }

        public void Flip()
        {
            this.normal = -normal;
            this.distance = -distance;
        }


        public void Translate(Vec3 translation)
        {
            distance += Vector3.Dot(normal, translation);
        }

        public static MyPlane Translate(MyPlane plane, Vec3 translation)
        {
            return new MyPlane(plane.normal, plane.distance += Vector3.Dot(plane.normal, translation));
        }


        public Vector3 ClosestPointOnPlane(Vec3 point)
        {
            var pointToPlaneDistance = GetDistanceToPoint(point);
            return point - (normal * pointToPlaneDistance);
        }
        //vector generado entre el planoy el punto y la normal
        // si el angulo es menor a 90 es mayor
        public float GetDistanceToPoint(Vec3 point)
        {
            return Vec3.Dot(normal, point) + distance;
        }

        //Calcula si un punto esta del lado "positivo" del Plano
        //El lado positivo es una convension
        // si la distancia hacia el punto es mayor a 0 devuelve positivo
        public bool GetSide(Vec3 point)
        {
            return Vec3.Dot(normal, point) + distance > 0.0F;
        }

        public bool SameSide(Vec3 inPt0, Vec3 inPt1)
        {
            float d0 = GetDistanceToPoint(inPt0);
            float d1 = GetDistanceToPoint(inPt1);
            return (d0 > 0.0f && d1 > 0.0f) ||
                   (d0 <= 0.0f && d1 <= 0.0f);
        }

    }
}