using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomMath;

public class Test : MonoBehaviour
{
    [SerializeField] Vec3 firstVec3;
    [SerializeField] Vec3 secondVec3;
    [SerializeField] Vector3 firstVector3;
    [SerializeField] Vector3 secondVector3;
    void Start()
    {
        firstVector3 = new Vector3(firstVec3.x, firstVec3.y, firstVec3.z);
        secondVector3 = new Vector3(secondVec3.x, secondVec3.y, secondVec3.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnValidate()
    {
        firstVector3 = new Vector3(firstVec3.x, firstVec3.y, firstVec3.z);
        secondVector3 = new Vector3(secondVec3.x, secondVec3.y, secondVec3.z);
        CheckCross();
    }
    void CheckMagnitude()
    {
        Debug.Log(firstVec3.magnitude);
        Debug.Log(firstVector3.magnitude);
        Debug.Log(firstVec3.sqrMagnitude);
        Debug.Log(firstVector3.sqrMagnitude);
    }
    void CheckNormalize()
    {
        Debug.Log("Vec3 Normalized:"+firstVec3.normalized);
        Debug.Log("Vector3 Normalized:"+firstVector3.normalized);
        Vector3 exampleVector3 = firstVector3;
        Vec3 exampleVec3 = firstVec3;
        exampleVec3.Normalize();
        exampleVector3.Normalize();
        Debug.Log("Vec3 example Normalize:"+exampleVec3);
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


}
