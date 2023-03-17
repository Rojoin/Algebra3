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
        CheckNormalize();
    }
    void CheckMagnitude()
    {
        Debug.Log(firstVec3.magnitude);
        Debug.Log(firstVector3.magnitude);
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

}
