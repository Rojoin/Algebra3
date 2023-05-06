using System.Collections;
using System.Collections.Generic;
using CustomMath;
using UnityEngine;

public class MeshCollider : MonoBehaviour
{
    private List<MyPlane> planes;
    public bool isActive;
    public int num;
    private List<Vec3> pointsInside;
    private Vec3 nearestPoint;
    private List<Vec3> previousVertex;

    void Start()
    {

        Mesh mesh = GetComponent<MeshFilter>().mesh;
        planes = new List<MyPlane>();
        for (int i = 0; i < mesh.GetIndices(0).Length; i += 3)
        {
            Vec3 auxA = new Vec3(mesh.vertices[mesh.GetIndices(0)[i]]);
            Vec3 auxB = new Vec3(mesh.vertices[mesh.GetIndices(0)[i + 1]]);
            Vec3 auxC = new Vec3(mesh.vertices[mesh.GetIndices(0)[i + 2]]);
            planes.Add(new MyPlane(auxA, auxB, auxC));

        }
        for (int i = 0; i < planes.Count; i++)
        {
            Vec3 aux = new Vec3(mesh.normals[i]);

            planes[i].SetNormalAndPosition(aux, planes[i].normal * planes[i].distance);

        }
    }

    void OnDestroy()
    {
        isActive = false;
    }
    // Hacer que funcione con rotaciones
    void Update()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        planes.Clear();
        previousVertex = new List<Vec3>();
        for (int i = 0; i < mesh.GetIndices(0).Length; i += 3)
        {
            //Creo un Plano vacio, que si hay un plano anterior en la lista lo iguala
            MyPlane previousPlane = MyPlane.Zero;

            if (planes.Count > 0)
            {
                previousPlane = planes[^1];
            }

            Vec3 a = new Vec3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            //Consigo los vertices del tri actual
            Vec3 verticeA = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i]]));
            verticeA.Scale(a);
            Vec3 verticeB = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 1]]));
            verticeB.Scale(a);
            Vec3 verticeC = new Vec3(transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 2]]));
            verticeC.Scale(a);

            //Creo el plano con los vertices
            var myPlane = new MyPlane(verticeA, verticeB, verticeC);

            //Si la lista no esta vacia y el plano es igual al plano anterior entro
            if (planes.Count > 0 && previousPlane == myPlane)
            {
                // si aunque sea 2 de los vertices son iguales al plano agrego un nuevo plano a la lista 
                var counter = 0;
                if (IsTheSameVertice(verticeA, previousVertex[0], previousVertex[1], previousVertex[2])) counter++;
                if (IsTheSameVertice(verticeB, previousVertex[0], previousVertex[1], previousVertex[2])) counter++;
                if (IsTheSameVertice(verticeC, previousVertex[0], previousVertex[1], previousVertex[2])) counter++;
                if (counter < 2)
                {
                    planes.Add(myPlane);
                    // agrego los vertices nuevos para chequear
                    AddNewPreviousVertex(verticeA, verticeB, verticeC);
                }
            }
            else//Si es el primer plano o no son iguales los plano agrego un plano
            {
                planes.Add(myPlane);
                AddNewPreviousVertex(verticeA, verticeB, verticeC);
            }

        }

        for (int i = 0; i < planes.Count; i++)
        {
            Vec3 aux = new Vec3(mesh.normals[i]);
            if (planes[i].normal != aux)
            {
                planes[i].SetNormalAndPosition(aux, planes[i].normal * planes[i].distance);
            }
        }

        GetNearestPoint();
        var planeRayCounter = 0;
        foreach (var plane in planes)
        {
            if (IsPointInPlane(plane))
            {
                planeRayCounter++;
            }
        }


    //    Debug.Log("Colisiono con :" + planeRayCounter + " planos");


    }

    private void AddNewPreviousVertex(Vec3 verticeA, Vec3 verticeB, Vec3 verticeC)
    {
        previousVertex.Clear();
        previousVertex.Add(verticeA);
        previousVertex.Add(verticeB);
        previousVertex.Add(verticeC);
    }

    bool IsTheSameVertice(Vec3 vertex, Vec3 vertice1, Vec3 vertice2, Vec3 vertice3)
    {
        return vertex == vertice1 || vertex == vertice2 || vertex == vertice3;
    }
    int NearestPositionValue(float position)
    {
        var aux = position / Grid.Delta;
        float x = aux - (int)aux > 0.5f ? aux + 1.0f : aux;
        if (x > Grid.size - 1)
        {
            x = Grid.size - 1;
        }
        else if (x < 0)
        {
            x = 0;
        }
        return (int)x;
    }

    bool IsPointInPlane(MyPlane plane)
    {
        float denom = Vec3.Dot(plane.normal, Vec3.Back * 10f);
        if (Mathf.Abs(denom) > Vec3.epsilon) // your favorite epsilon
        {
            float t = Vec3.Dot((plane.normal * plane.distance - nearestPoint), plane.normal) / denom;
            if (t >= Vec3.epsilon) return true; // you might want to allow an epsilon here too
        }
        return false;
    }
    void GetNearestPoint()
    {
        var near = nearestPoint;
        var x = NearestPositionValue(transform.position.x);
        var y = NearestPositionValue(transform.position.y);
        var z = NearestPositionValue(transform.position.z);

        nearestPoint = Grid.grid[x, y, z];

    }

    void CheckPoint()
    {


    }
    void OnDrawGizmos()
    {
        if (isActive)
        {
            var color = Color.red;
            foreach (var VARIABLE in planes)
            {

                DrawPlane(VARIABLE.normal * VARIABLE.distance, VARIABLE.normal, Color.red);
            }

            Gizmos.DrawRay(nearestPoint, Vec3.Back * 10f);

        }

    }
    public void DrawPlane(Vec3 position, Vec3 normal, Color color)
    {
        Vector3 v3;
        if (normal.normalized != Vector3.forward)
            v3 = Vector3.Cross(normal, Vector3.forward).normalized * normal.magnitude;
        else
            v3 = Vector3.Cross(normal, Vector3.up).normalized * normal.magnitude; ;
        var corner0 = position + v3;
        var corner2 = position - v3;
        var q = Quaternion.AngleAxis(90.0f, normal);
        v3 = q * v3;
        var corner1 = position + v3;
        var corner3 = position - v3;
        Debug.DrawLine(corner0, corner2, color);
        Debug.DrawLine(corner1, corner3, color);
        Debug.DrawLine(corner0, corner1, color);
        Debug.DrawLine(corner1, corner2, color);
        Debug.DrawLine(corner2, corner3, color);
        Debug.DrawLine(corner3, corner0, color);
        Debug.DrawRay(position, normal, Color.magenta);
    }

}
