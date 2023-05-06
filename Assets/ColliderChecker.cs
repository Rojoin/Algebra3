using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
  [SerializeField]  private MyMeshCollider[] meshes;
  [SerializeField]  private Material red;
  [SerializeField]  private Material green;
  [SerializeField]  private bool collide;



  void Update()
  {
      collide = false;
      foreach (var point in meshes[1].pointsInside)
      {
          if (meshes[0].CheckPointsAgainstAnotherMesh(point))
          {
              collide = true;
          }
          
      }

      meshes[0].GetComponent<MeshRenderer>().material = collide ? green : red;
      meshes[1].GetComponent<MeshRenderer>().material = collide ? green : red;


  }
}
