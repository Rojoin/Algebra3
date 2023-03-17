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
}
