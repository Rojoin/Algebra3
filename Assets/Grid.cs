using System.Collections;
using System.Collections.Generic;
using CustomMath;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
   public  float delta = 0.2f;
   public static float Delta;
   public static int size = 10;
   public static Vec3[,,] grid;
   public bool isActive = false;


    // fijarme en x y z cual esta más cerca de nuestro transfomr divido la spearacion de la grilla
    // fijarme todos los putnos dentro del objeto y guardarlos(tirar rayo y que de una cantodad impar)
    // Cuando choca un plano, tirar otro rayo más para saber si ersta tocando la misma geometria
    // chequear los puntos entre los dos y si al menos uno esta derntro collisionan
    //
  
    void Start()
    {
        
        Delta = delta;
        grid = new Vec3[size, size, size];
        isActive = true;
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int z = 0; z < grid.GetLength(2); z++)
                {
                    grid[x, y, z] = new Vec3(x, y, z) *delta;
                }
            }
        }
    }


    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if (!isActive)return;
            for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int z = 0; z < grid.GetLength(2); z++)
                {

                    Gizmos.DrawWireSphere(new Vec3(x,y,z)*delta,0.1f);
                }
            }
        }
    }
}
