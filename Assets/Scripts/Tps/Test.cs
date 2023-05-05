using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;
using EjerciciosAlgebra;
using MathDebbuger;


public class Test : MonoBehaviour
{
    [SerializeField]
    Vec3 firstVec3;
    [SerializeField]
    Vec3 secondVec3;
    [SerializeField]
    Vec3 aux;
    [SerializeField]
    Vector3 firstVector3;
    [SerializeField]
    Vector3 secondVector3;
    [SerializeField]
    private float lerp;

    [Range(1, 10)] public int exerciseNumber;
    void Start()
    {
        firstVector3 = new Vector3(firstVec3.x, firstVec3.y, firstVec3.z);
        secondVector3 = new Vector3(secondVec3.x, secondVec3.y, secondVec3.z);
        List<Vector3> vectors = new List<Vector3>
        {
            new Vec3(10.0f, 0.0f, 0.0f),
            new Vec3(10.0f, 10.0f, 0.0f),
            new Vec3(20.0f, 10.0f, 0.0f),
            new Vec3(20.0f, 20.0f, 0.0f)
        };
        Vector3Debugger.AddVector(firstVector3,Color.yellow,"Vector1");
        Vector3Debugger.AddVector(secondVector3,Color.blue,"Vector2");
        Vector3Debugger.AddVector(aux, Color.green,"Vector3");
        Vector3Debugger.EnableEditorView("Vector1");
        Vector3Debugger.EnableEditorView("Vector2");
        Vector3Debugger.EnableEditorView("Vector3");
    }

    // Update is called once per frame
    void Update()
    {
        ExerciceOne();
        Vector3Debugger.UpdatePosition("Vector1", firstVec3);
        Vector3Debugger.UpdatePosition("Vector2", secondVec3);
        Vector3Debugger.UpdatePosition("Vector3", aux);
    }
    private void OnValidate()
    {
        Vector3Debugger.UpdatePosition("Vector1", firstVec3);
        Vector3Debugger.UpdatePosition("Vector2", secondVec3);
        Vector3Debugger.UpdatePosition("Vector3",aux);
        firstVector3 = new Vector3(firstVec3.x, firstVec3.y, firstVec3.z);
        secondVector3 = new Vector3(secondVec3.x, secondVec3.y, secondVec3.z);
        switch (exerciseNumber)
        {
            case 5:
                lerp = 0;
                break;
            case 10:
                lerp = 1;
                break;
        }
    }
    #region Exercices
    void ExerciceOne()
    {

        
        switch (exerciseNumber)
        {
            case 1:
                Debug.Log(firstVec3 + secondVec3);
                aux = firstVec3 + secondVec3;
                break;
            case 2:
                aux = firstVec3 - secondVec3;
                Debug.Log(firstVec3 - secondVec3);
                break;
            case 3:
                aux = firstVec3;
                aux.Scale(secondVec3);
                Debug.Log(aux);
                break;
            case 4:
                aux = Vec3.Cross(secondVec3, firstVec3);
                Debug.Log(aux);

                break;
            case 5:
                aux = firstVec3;
                lerp += Time.deltaTime;
                aux = Vec3.Lerp(firstVec3, secondVec3, lerp);
                if (lerp > 1)
                {
                    lerp = 0;
                }
                Debug.Log(aux);
                break;
            case 6:
                aux = Vec3.Max(firstVec3, secondVec3);
                Debug.Log(aux);
                break;
            case 7:
                aux = Vec3.Project(firstVec3, secondVec3.normalized);
                Debug.Log(aux);
                break;
            case 8: // tangente entre el vector a y b
                aux = Vec3.Reflect(firstVec3, secondVec3.normalized);
                aux = -aux;
                var num = Vector3.Distance(firstVec3, secondVec3);
                aux = firstVec3 + secondVec3;
                aux = num * aux.normalized;
                Debug.Log(aux);
                break;
            case 9:
                aux = Vec3.Reflect(firstVec3, secondVec3.normalized);
                Debug.Log(aux);
                break;
            case 10:

                lerp -= Time.deltaTime;
                aux = Vec3.LerpUnclamped(firstVec3, secondVec3, lerp);
                if (lerp < -10)
                {
                    lerp = 1;
                }
                Debug.Log(aux);
                break;
        }

     
    }


    #endregion
    #region Comparisons
    void CheckMagnitude()
    {
        Debug.Log(firstVec3.magnitude);
        Debug.Log(firstVector3.magnitude);
        Debug.Log(firstVec3.sqrMagnitude);
        Debug.Log(firstVector3.sqrMagnitude);
    }
    void CheckNormalize()
    {
        Debug.Log("Vec3 Normalized:" + firstVec3.normalized);
        Debug.Log("Vector3 Normalized:" + firstVector3.normalized);
        Vector3 exampleVector3 = firstVector3;
        Vec3 exampleVec3 = firstVec3;
        exampleVec3.Normalize();
        exampleVector3.Normalize();
        Debug.Log("Vec3 example Normalize:" + exampleVec3);
        Debug.Log("Vector3 example Normalize:" + exampleVector3);
    }
    void CheckDot()
    {
        Debug.Log("Vec3 Dot" + Vec3.Dot(firstVec3, secondVec3));
        Debug.Log("Vector3 Dor:" + Vector3.Dot(firstVector3, secondVector3));
    }
    void CheckAngle()
    {
        Debug.Log("Vec3 Angle:" + Vec3.Angle(firstVec3, secondVec3));
        Debug.Log("Vector3 Angle:" + Vector3.Angle(firstVector3, secondVector3));
    }
    void CheckCross()
    {
        Debug.Log("Vec3 Cross:" + Vec3.Cross(firstVec3, secondVec3));
        Debug.Log("Vector3 Cross:" + Vector3.Cross(firstVector3, secondVector3));
    }
    void CheckLerp()
    {
        Debug.Log("Vec3 Lerp:" + Vec3.Lerp(firstVec3, secondVec3, lerp));
        Debug.Log("Vector3 Lerp:" + Vector3.Lerp(firstVector3, secondVector3, lerp));
        Debug.Log("Vec3 LerpUnclamped:" + Vec3.LerpUnclamped(firstVec3, secondVec3, lerp));
        Debug.Log("Vector3 LerpUnclamped:" + Vector3.LerpUnclamped(firstVector3, secondVector3, lerp));
    }
    void CheckClampMagnitude()
    {
        Debug.Log("Vec3 ClampMag:" + Vec3.ClampMagnitude(firstVec3, lerp));
        Debug.Log("Vector3 ClampMag:" + Vector3.ClampMagnitude(firstVector3, lerp));
    }
    void CheckDistance()
    {
        Debug.Log("Vec3 Distance:" + Vec3.Distance(firstVec3, secondVec3));
        Debug.Log("Vector3 Distance:" + Vector3.Distance(firstVector3, secondVector3));
    }
    void CheckProjection()
    {
        Debug.Log("Vec3 Project:" + Vec3.Project(firstVec3, secondVec3));
        Debug.Log("Vector3 Project:" + Vector3.Project(firstVector3, secondVector3));
    }
    void CheckReflect()
    {
        Debug.Log("Vec3 Reflect:" + Vec3.Reflect(firstVec3, secondVec3));
        Debug.Log("Vector3 Reflect:" + Vector3.Reflect(firstVector3, secondVector3));
    }

    #endregion
}
