using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CustomMath;
using UnityEngine;

public class MyMeshCollider : MonoBehaviour
{
    private List<MyPlane> planes;
    public bool isActive;
    public int num;
    public List<Vec3> pointsInside;
    public Vec3 nearestPoint;
    private List<Vec3> previousVertex;
    private List<Vec3> poinsToCheck;




    struct Ray
    {
        public Vec3 origin;
        public Vec3 destination;

        public Ray(Vec3 origin, Vec3 destination)
        {
            this.origin = origin;
            this.destination = destination;
        }
    }

    void Start()
    {

        Mesh mesh = GetComponent<MeshFilter>().mesh;

        poinsToCheck = new List<Vec3>();
        previousVertex = new List<Vec3>();
        pointsInside = new List<Vec3>();
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



            // transform.TransformPoint
            //Vec3 a = new Vec3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);

            //Consigo los vertices del tri actual
            Vec3 verticeA = new Vec3((transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i]])));

            Vec3 verticeB = new Vec3((transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 1]])));

            Vec3 verticeC = new Vec3((transform.TransformPoint(mesh.vertices[mesh.GetIndices(0)[i + 2]])));
            // verticeA *= -1;
            // verticeB *= -1;
            // verticeC *= -1;

            //Creo el plano con los vertices
            var myPlane = new MyPlane(verticeA, verticeB, verticeC);
            myPlane.normal *= -1;
            planes.Add(myPlane);


        }

        for (int i = 0; i < planes.Count; i++)
        {
            planes[i].Flip();
        }

        GetNearestPoint();
        AddPointsToCheck();
        AddPointsInside();


        Debug.Log("Points:" + pointsInside.Count);
        // Debug.Log("Colisiono con :" + planeRayCounter + " planos");


    }

    private Vec3 FromLocalToWolrd(Vec3 point, Transform transformRef)
    {
        Vector3 result = Vector3.zero;

        result = new Vector3(point.x * transformRef.localScale.x, point.y * transformRef.localScale.y, point.z * transformRef.localScale.z);

        result = transform.rotation * (Vector3)result;

        Vec3 finalResult = new Vec3(result);

        return finalResult + new Vec3(transformRef.position);
    }
    private void AddPointsToCheck()
    {
        poinsToCheck.Clear();
        int maxX = (int)(nearestPoint.x + 3.0f); int minX = (int)(nearestPoint.x - 3.0f);
        int maxY = (int)(nearestPoint.y + 3.0f); int minY = (int)(nearestPoint.y - 3.0f);
        int maxZ = (int)(nearestPoint.z + 3.0f); int minZ = (int)(nearestPoint.z - 3.0f);

        maxX = Mathf.Clamp(maxX, 0, Grid.size - 1);
        maxY = Mathf.Clamp(maxY, 0, Grid.size - 1);
        maxZ = Mathf.Clamp(maxZ, 0, Grid.size - 1);
        minX = Mathf.Clamp(minX, 0, Grid.size - 1);
        minY = Mathf.Clamp(minY, 0, Grid.size - 1);
        minZ = Mathf.Clamp(minZ, 0, Grid.size - 1);


        for (int x = minX; x < maxX; x++)
        {
            for (int y = minY; y < maxY; y++)
            {
                for (int z = minZ; z < maxZ; z++)
                {

                    poinsToCheck.Add(Grid.grid[x, y, z]);

                }
            }
        }
    }

    void AddPointsInside()
    {
        pointsInside.Clear();
        foreach (var point in poinsToCheck)
        {
            Ray ray = new Ray(point, Vec3.Forward * 10f);
            var counter = 0;
            foreach (var plane in planes)
            {
                if (IsPointInPlane(plane, ray, out Vec3 t))
                {
                    if (IsValidPlane(plane, t))
                    {
                        counter++;
                    }
                }
            }
            if (counter % 2 == 1) pointsInside.Add(point);
        }
    }

    private bool IsValidPlane(MyPlane plane, Vec3 point)
    {
        float x1 = plane.verA.x; float y1 = plane.verA.y; float z1 = plane.verA.z;
        float x2 = plane.verB.x; float y2 = plane.verB.y; float z2 = plane.verB.z;
        float x3 = plane.verC.x; float y3 = plane.verC.y; float z3 = plane.verC.z;

        // Area del triangulo
        float areaOrig = Mathf.Abs((x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1));

        // Areas de los 3 triangulos hechos con el punto y las esquinas
        float area1 = Mathf.Abs((x1 - point.x) * (y2 - point.y) - (x2 - point.x) * (y1 - point.y));
        float area2 = Mathf.Abs((x2 - point.x) * (y3 - point.y) - (x3 - point.x) * (y2 - point.y));
        float area3 = Mathf.Abs((x3 - point.x) * (y1 - point.y) - (x1 - point.x) * (y3 - point.y));


        // Si la suma del area de los 3 triangulos es igual a la del original estamos adentro
        return Math.Abs(area1 + area2 + area3 - areaOrig) < Vec3.epsilon; //fijatse de cambiar pr comparacion aepsilon

        //chequear con los 3 puntos de plane si el punto pertence al vertice
        // si pertenece true, y sumo counter
        //si no false;
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
        x = Mathf.Clamp(x, 0, Grid.size - 1);
        return (int)x;
    }

    bool IsPointInPlane(MyPlane plane, Ray ray, out Vec3 point)
    {
        point = Vec3.Zero;// Tiene que se el punto de interseccion

        float denom = Vec3.Dot(plane.normal, ray.destination);
        if (Mathf.Abs(denom) > Vec3.epsilon)
        {
            float t = Vec3.Dot((plane.normal * plane.distance - ray.origin), plane.normal) / denom;
            if (t >= Vec3.epsilon)
            {
                point = ray.origin + ray.destination * t;  //Vec3.Lerp
                return true;
            }
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

    public bool CheckPointsAgainstAnotherMesh(Vec3 externalPoint)
    {
        return pointsInside.Any(pointsInside => pointsInside == externalPoint);
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

            foreach (var point in pointsInside)
            {
                Gizmos.DrawRay(point, Vec3.Back * 10f);
            }

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
