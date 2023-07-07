using System;
using CustomMath;
using UnityEngine;

public struct MyMatrix4x4 : IEquatable<MyMatrix4x4>
{
    #region Variables
    public float m00;
    public float m10;
    public float m20;
    public float m30;
    public float m01;
    public float m11;
    public float m21;
    public float m31;
    public float m02;
    public float m12;
    public float m22;
    public float m32;
    public float m03;
    public float m13;
    public float m23;
    public float m33;
    #endregion

    public MyMatrix4x4(Vector4 firstColumn, Vector4 secondColumn, Vector4 thirdColumn, Vector4 forthColumn)
    {
        m00 = firstColumn.x;
        m01 = secondColumn.x;
        m02 = thirdColumn.x;
        m03 = forthColumn.x;
        m10 = firstColumn.y;
        m11 = secondColumn.y;
        m12 = thirdColumn.y;
        m13 = forthColumn.y;
        m20 = firstColumn.z;
        m21 = secondColumn.z;
        m22 = thirdColumn.z;
        m23 = forthColumn.z;
        m30 = firstColumn.w;
        m31 = secondColumn.w;
        m32 = thirdColumn.w;
        m33 = forthColumn.w;
    }
    public float this[int row, int col]
    {
        get
        {
            return this[row + col * 4];
        }
        set
        {
            this[row + col * 4] = value;
        }
    }
    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return m00;
                case 1:
                    return m10;
                case 2:
                    return m20;
                case 3:
                    return m30;
                case 4:
                    return m01;
                case 5:
                    return m11;
                case 6:
                    return m21;
                case 7:
                    return m31;
                case 8:
                    return m02;
                case 9:
                    return m12;
                case 10:
                    return m22;
                case 11:
                    return m32;
                case 12:
                    return m03;
                case 13:
                    return m13;
                case 14:
                    return m23;
                case 15:
                    return m33;
                default:
                    throw new IndexOutOfRangeException("Index out of Range!");
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    m00 = value;
                    break;
                case 1:
                    m10 = value;
                    break;
                case 2:
                    m20 = value;
                    break;
                case 3:
                    m30 = value;
                    break;
                case 4:
                    m01 = value;
                    break;
                case 5:
                    m11 = value;
                    break;
                case 6:
                    m21 = value;
                    break;
                case 7:
                    m31 = value;
                    break;
                case 8:
                    m02 = value;
                    break;
                case 9:
                    m12 = value;
                    break;
                case 10:
                    m22 = value;
                    break;
                case 11:
                    m32 = value;
                    break;
                case 12:
                    m03 = value;
                    break;
                case 13:
                    m13 = value;
                    break;
                case 14:
                    m23 = value;
                    break;
                case 15:
                    m33 = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Index out of Range!");
            }
        }
    }
    public static MyMatrix4x4 zero =>
        new(
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0),
            new Vector4(0, 0, 0, 0));
    public Vector3 lossyScale => //Devuelve la escala real del objeto. Esto es en caso de que se apliquen rotaciones y otros cálculos, donde se pierde la escala
        new(GetColumn(0).magnitude, GetColumn(1).magnitude, GetColumn(2).magnitude);

    public static MyMatrix4x4 Identity { get; } = new MyMatrix4x4
    (
        new Vector4(1, 0, 0, 0),
        new Vector4(0, 1, 0, 0),
        new Vector4(0, 0, 1, 0),
        new Vector4(0, 0, 0, 1)
    );
    public void SetRow(int index, Vector4 row) //Setea la fila
    {
        this[index, 0] = row.x;
        this[index, 1] = row.y;
        this[index, 2] = row.z;
        this[index, 3] = row.w;
    }
    public void SetColumn(int index, Vector4 col) //Setea la columna
    {
        this[0, index] = col.x;
        this[1, index] = col.y;
        this[2, index] = col.z;
        this[3, index] = col.w;
    }
    public Vector4 GetRow(int index) //Devuelve la fila
    {
        return index switch
        {
            0 => new Vector4(m00, m01, m02, m03),
            1 => new Vector4(m10, m11, m12, m13),
            2 => new Vector4(m20, m21, m22, m23),
            3 => new Vector4(m30, m31, m32, m33),
            _ => throw new IndexOutOfRangeException("Index out of Range!")
        };
    }
    public Vector4 GetColumn(int i) //Devuelve la columna
    {
        return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
    }

    public float determinant => Determinant(this); //Devuelve la determinante de esa matriz
    public static float Determinant(MyMatrix4x4 m)
    {
        return
            m[0, 3] * m[1, 2] * m[2, 1] * m[3, 0] - m[0, 2] * m[1, 3] * m[2, 1] * m[3, 0] -
            m[0, 3] * m[1, 1] * m[2, 2] * m[3, 0] + m[0, 1] * m[1, 3] * m[2, 2] * m[3, 0] +
            m[0, 2] * m[1, 1] * m[2, 3] * m[3, 0] - m[0, 1] * m[1, 2] * m[2, 3] * m[3, 0] -
            m[0, 3] * m[1, 2] * m[2, 0] * m[3, 1] + m[0, 2] * m[1, 3] * m[2, 0] * m[3, 1] +
            m[0, 3] * m[1, 0] * m[2, 2] * m[3, 1] - m[0, 0] * m[1, 3] * m[2, 2] * m[3, 1] -
            m[0, 2] * m[1, 0] * m[2, 3] * m[3, 1] + m[0, 0] * m[1, 2] * m[2, 3] * m[3, 1] +
            m[0, 3] * m[1, 1] * m[2, 0] * m[3, 2] - m[0, 1] * m[1, 3] * m[2, 0] * m[3, 2] -
            m[0, 3] * m[1, 0] * m[2, 1] * m[3, 2] + m[0, 0] * m[1, 3] * m[2, 1] * m[3, 2] +
            m[0, 1] * m[1, 0] * m[2, 3] * m[3, 2] - m[0, 0] * m[1, 1] * m[2, 3] * m[3, 2] -
            m[0, 2] * m[1, 1] * m[2, 0] * m[3, 3] + m[0, 1] * m[1, 2] * m[2, 0] * m[3, 3] +
            m[0, 2] * m[1, 0] * m[2, 1] * m[3, 3] - m[0, 0] * m[1, 2] * m[2, 1] * m[3, 3] -
            m[0, 1] * m[1, 0] * m[2, 2] * m[3, 3] + m[0, 0] * m[1, 1] * m[2, 2] * m[3, 3];
    }
    public static MyMatrix4x4 Inverse(MyMatrix4x4 m) //Devuelve la inversa de la matriz ingresada
    {
        float detA = Determinant(m); //Debe tener determinante, de otra forma, no es inversible
        if (detA == 0)
            return zero;

        MyMatrix4x4 aux = new MyMatrix4x4()
        {
            // sacar el determinante de cada una de esas posiciones
            //------0---------
            m00 = m.m11 * m.m22 * m.m33 + m.m12 * m.m23 * m.m31 + m.m13 * m.m21 * m.m32 - m.m11 * m.m23 * m.m32 - m.m12 * m.m21 * m.m33 - m.m13 * m.m22 * m.m31,
            m01 = m.m01 * m.m23 * m.m32 + m.m02 * m.m21 * m.m33 + m.m03 * m.m22 * m.m31 - m.m01 * m.m22 * m.m33 - m.m02 * m.m23 * m.m31 - m.m03 * m.m21 * m.m32,
            m02 = m.m01 * m.m12 * m.m33 + m.m02 * m.m13 * m.m32 + m.m03 * m.m11 * m.m32 - m.m01 * m.m13 * m.m32 - m.m02 * m.m11 * m.m33 - m.m03 * m.m12 * m.m31,
            m03 = m.m01 * m.m13 * m.m22 + m.m02 * m.m11 * m.m23 + m.m03 * m.m12 * m.m21 - m.m01 * m.m12 * m.m23 - m.m02 * m.m13 * m.m21 - m.m03 * m.m11 * m.m22,
            //-------1--------					     								    
            m10 = m.m10 * m.m23 * m.m32 + m.m12 * m.m20 * m.m33 + m.m13 * m.m22 * m.m30 - m.m10 * m.m22 * m.m33 - m.m12 * m.m23 * m.m30 - m.m13 * m.m20 * m.m32,
            m11 = m.m00 * m.m22 * m.m33 + m.m02 * m.m23 * m.m30 + m.m03 * m.m20 * m.m32 - m.m00 * m.m23 * m.m32 - m.m02 * m.m20 * m.m33 - m.m03 * m.m22 * m.m30,
            m12 = m.m00 * m.m13 * m.m32 + m.m02 * m.m10 * m.m33 + m.m03 * m.m12 * m.m30 - m.m00 * m.m12 * m.m33 - m.m02 * m.m13 * m.m30 - m.m03 * m.m10 * m.m32,
            m13 = m.m00 * m.m12 * m.m23 + m.m02 * m.m13 * m.m20 + m.m03 * m.m10 * m.m22 - m.m00 * m.m13 * m.m22 - m.m02 * m.m10 * m.m23 - m.m03 * m.m12 * m.m20,
            //-------2--------					     								    
            m20 = m.m10 * m.m21 * m.m33 + m.m11 * m.m23 * m.m30 + m.m13 * m.m20 * m.m31 - m.m10 * m.m23 * m.m31 - m.m11 * m.m20 * m.m33 - m.m13 * m.m31 * m.m30,
            m21 = m.m00 * m.m23 * m.m31 + m.m01 * m.m20 * m.m33 + m.m03 * m.m21 * m.m30 - m.m00 * m.m21 * m.m33 - m.m01 * m.m23 * m.m30 - m.m03 * m.m20 * m.m31,
            m22 = m.m00 * m.m11 * m.m33 + m.m01 * m.m13 * m.m31 + m.m03 * m.m10 * m.m31 - m.m00 * m.m13 * m.m31 - m.m01 * m.m10 * m.m33 - m.m03 * m.m11 * m.m30,
            m23 = m.m00 * m.m13 * m.m21 + m.m01 * m.m10 * m.m23 + m.m03 * m.m11 * m.m31 - m.m00 * m.m11 * m.m23 - m.m01 * m.m13 * m.m20 - m.m03 * m.m10 * m.m21,
            //------3---------					     								    
            m30 = m.m10 * m.m22 * m.m31 + m.m11 * m.m20 * m.m32 + m.m12 * m.m21 * m.m30 - m.m00 * m.m21 * m.m32 - m.m11 * m.m22 * m.m30 - m.m12 * m.m20 * m.m31,
            m31 = m.m00 * m.m21 * m.m32 + m.m01 * m.m22 * m.m30 + m.m02 * m.m20 * m.m31 - m.m00 * m.m22 * m.m31 - m.m01 * m.m20 * m.m32 - m.m02 * m.m21 * m.m30,
            m32 = m.m00 * m.m12 * m.m31 + m.m01 * m.m10 * m.m32 + m.m02 * m.m11 * m.m30 - m.m00 * m.m11 * m.m32 - m.m01 * m.m12 * m.m30 - m.m02 * m.m10 * m.m31,
            m33 = m.m00 * m.m11 * m.m22 + m.m01 * m.m12 * m.m20 + m.m02 * m.m10 * m.m21 - m.m00 * m.m12 * m.m21 - m.m01 * m.m10 * m.m22 - m.m02 * m.m11 * m.m20
        };

        MyMatrix4x4 ret = new MyMatrix4x4()
        {
            m00 = aux.m00 / detA,
            m01 = aux.m01 / detA,
            m02 = aux.m02 / detA,
            m03 = aux.m03 / detA,
            m10 = aux.m10 / detA,
            m11 = aux.m11 / detA,
            m12 = aux.m12 / detA,
            m13 = aux.m13 / detA,
            m20 = aux.m20 / detA,
            m21 = aux.m21 / detA,
            m22 = aux.m22 / detA,
            m23 = aux.m23 / detA,
            m30 = aux.m30 / detA,
            m31 = aux.m31 / detA,
            m32 = aux.m32 / detA,
            m33 = aux.m33 / detA

        };
        return ret;
    }
    public static MyMatrix4x4 Transpose(MyMatrix4x4 m)//Espeja respecto de la diagonal principal
    {
        return new MyMatrix4x4()
        {
            m01 = m.m10,
            m02 = m.m20,
            m03 = m.m30,
            m10 = m.m01,
            m12 = m.m21,
            m13 = m.m31,
            m20 = m.m02,
            m21 = m.m12,
            m23 = m.m32,
            m30 = m.m03,
            m31 = m.m13,
            m32 = m.m23,
        };
    }
    public Vec3 MultiplyVector(Vec3 v) //Multiplica las componentes del vector en la matriz (pero solo en X, Y y Z; ignorando W)
    {
        Vec3 v3; //No se tienen en cuenta ni la 4ta fila ni la 4ta columna
        v3.x = (float)((double)m00 * (double)v.x + (double)m01 * (double)v.y + (double)m02 * (double)v.z);
        v3.y = (float)((double)m10 * (double)v.x + (double)m11 * (double)v.y + (double)m12 * (double)v.z);
        v3.z = (float)((double)m20 * (double)v.x + (double)m21 * (double)v.y + (double)m22 * (double)v.z);
        return v3;
    }
    public Vec3 MultiplyPoint3x4(Vec3 p) //Multiplica las componentes del vector en la matriz (X, Y y Z pero no ignoro W)
    {
        Vec3 v3;
        v3.x = (float)((double)m00 * (double)p.x + (double)m01 * (double)p.y + (double)m02 * (double)p.z) + m03;
        v3.y = (float)((double)m10 * (double)p.x + (double)m11 * (double)p.y + (double)m12 * (double)p.z) + m13;
        v3.z = (float)((double)m20 * (double)p.x + (double)m21 * (double)p.y + (double)m22 * (double)p.z) + m23;
        return v3;
    }
    public Vec3 MultiplyPoint(Vec3 p)
    {
        Vec3 v3;

        v3.x = (float)((double)m00 * (double)p.x + (double)m01 * (double)p.y + (double)m02 * (double)p.z) + m03;
        v3.y = (float)((double)m10 * (double)p.x + (double)m11 * (double)p.y + (double)m12 * (double)p.z) + m13;
        v3.z = (float)((double)m20 * (double)p.x + (double)m21 * (double)p.y + (double)m22 * (double)p.z) + m23;
        float num = 1f / ((float)((double)m30 * (double)p.x + (double)m31 * (double)p.y + (double)m32 * (double)p.z) + m33);
        v3.x *= num;
        v3.y *= num;
        v3.z *= num;
        return v3;
    }
    public static MyMatrix4x4 TRS(Vec3 pos, MyQuat q, Vec3 s) //Devuelve la matriz TRS de los valores ingresados
    {
        return (Translate(pos) * Rotate(q) * Scale(s));
    }
    public MyQuat rotation { //Devuelve la matriz rotación de ese MyQuat.
        get { 
            MyMatrix4x4 m = this;
            MyQuat q = new MyQuat();
            q.w = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] + m[1, 1] + m[2, 2])) / 2; //Devuelve la raiz de un número que debe ser al menos 0.
            q.x = Mathf.Sqrt(Mathf.Max(0, 1 + m[0, 0] - m[1, 1] - m[2, 2])) / 2; //Por eso hace un min entre las posiciones de las diagonales.
            q.y = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] + m[1, 1] - m[2, 2])) / 2; 
            q.z = Mathf.Sqrt(Mathf.Max(0, 1 - m[0, 0] - m[1, 1] + m[2, 2])) / 2;
            q.x *= Mathf.Sign(q.x * (m[2, 1] - m[1, 2])); 
            q.y *= Mathf.Sign(q.y * (m[0, 2] - m[2, 0])); //Son los valores de la matriz que se van a modificar
            q.z *= Mathf.Sign(q.z * (m[1, 0] - m[0, 1]));
            return q;
        } 
    }
    public bool ValidTRS()
    {
        // Check if the matrix is a valid TRS matrix

        // Translation: The last row of the matrix should be (0, 0, 0, 1)
        if (m30 != 0 || m31 != 0 || m32 != 0 || m33 != 1)
        {
            return false;
        }

        // Rotation: The upper-left 3x3 submatrix should be an orthogonal matrix
        Vec3 column0 = new Vec3(m00, m10, m20);
        Vec3 column1 = new Vec3(m01, m11, m21);
        Vec3 column2 = new Vec3(m02, m12, m22);

        if (!Mathf.Approximately(Vec3.Dot(column0, column1), 0) ||
            !Mathf.Approximately(Vec3.Dot(column0, column2), 0) ||
            !Mathf.Approximately(Vec3.Dot(column1, column2), 0))
        {
            return false;
        }

        // Scale: The scale factors should be positive
        if (m00 < 0 || m11 < 0 || m22 < 0)
        {
            return false;
        }

        return true;
    }
    public void SetTRS(Vec3 pos, MyQuat q, Vec3 s)
    {
        this = TRS(pos, q, s);
    }
    public static MyMatrix4x4 Translate(Vec3 v) //Hay fotito
    {
        MyMatrix4x4 m;
        m.m00 = 1f;
        m.m01 = 0.0f;
        m.m02 = 0.0f;
        m.m03 = v.x;
        m.m10 = 0.0f;
        m.m11 = 1f;
        m.m12 = 0.0f;
        m.m13 = v.y;
        m.m20 = 0.0f;
        m.m21 = 0.0f;
        m.m22 = 1f;
        m.m23 = v.z;
        m.m30 = 0.0f;
        m.m31 = 0.0f;
        m.m32 = 0.0f;
        m.m33 = 1f;
        return m;
    }

    public static MyMatrix4x4 Rotate(MyQuat q) //Es así porque don opengl quiso que fuera así
    {
        double num1 = q.x * 2f;
        double num2 = q.y * 2f;
        double num3 = q.z * 2f;
        double num4 = q.x * num1;
        double num5 = q.y * num2;
        double num6 = q.z * num3;
        double num7 = q.x * num2;
        double num8 = q.x * num3;
        double num9 = q.y * num3;
        double num10 = q.w * num1;
        double num11 = q.w * num2;
        double num12 = q.w * num3;
        MyMatrix4x4 m;
        m.m00 = (float)(1.0 - num5 + num6);
        m.m10 = (float)(num7 + num12);
        m.m20 = (float)(num8 - num11);
        m.m30 = 0.0f;
        m.m01 = (float)(num7 - num12);
        m.m11 = (float)(1.0 - num4 + num6);
        m.m21 = (float)(num9 + num10);
        m.m31 = 0.0f;
        m.m02 = (float)(num8 + num11);
        m.m12 = (float)(num9 - num10);
        m.m22 = (float)(1.0 - num4 + num5);
        m.m32 = 0.0f;
        m.m03 = 0.0f;
        m.m13 = 0.0f;
        m.m23 = 0.0f;
        m.m33 = 1f;
        return m;
    }
    public static MyMatrix4x4 Scale(Vec3 v)
    {
        MyMatrix4x4 m;
        m.m00 = v.x;
        m.m01 = 0.0f;
        m.m02 = 0.0f;
        m.m03 = 0.0f;
        m.m10 = 0.0f;
        m.m11 = v.y;
        m.m12 = 0.0f;
        m.m13 = 0.0f;
        m.m20 = 0.0f;
        m.m21 = 0.0f;
        m.m22 = v.z;
        m.m23 = 0.0f;
        m.m30 = 0.0f;
        m.m31 = 0.0f;
        m.m32 = 0.0f;
        m.m33 = 1f;
        return m;
    }
    public bool Equals(MyMatrix4x4 other) //Si cada elemento es igual al otro
    {
        int num;
        if (GetColumn(0).Equals(other.GetColumn(0)))
        {
            Vector4 col = GetColumn(1);
            if (col.Equals(other.GetColumn(1)))
            {
                col = GetColumn(2);
                if (col.Equals(other.GetColumn(2)))
                {
                    col = GetColumn(3);
                    num = col.Equals(other.GetColumn(3)) ? 1 : 0;
                    return num != 0;
                }
            }
        }
        num = 0;
        return num != 0;
    }

    public override bool Equals(object obj)
    {
        return obj is MyMatrix4x4 other && Equals(other);
    }
    public static MyMatrix4x4 operator *(MyMatrix4x4 a, MyMatrix4x4 b)//Fila por columna//Componente a componente
    {
        MyMatrix4x4 ret = zero;
        for (int i = 0; i < 4; i++)
        {
            ret.SetColumn(i, a * b.GetColumn(i));
        }

        return ret;
    }
    public static Vector4 operator *(MyMatrix4x4 a, Vector4 v) // m4x4 * m1x4
    {
        Vector4 ret;
        ret.x = (float)((double)a.m00 * (double)v.x + (double)a.m01 * (double)v.y + (double)a.m02 * (double)v.z + (double)a.m03 * (double)v.w);
        ret.y = (float)((double)a.m10 * (double)v.x + (double)a.m11 * (double)v.y + (double)a.m12 * (double)v.z + (double)a.m13 * (double)v.w);
        ret.z = (float)((double)a.m20 * (double)v.x + (double)a.m21 * (double)v.y + (double)a.m22 * (double)v.z + (double)a.m23 * (double)v.w);
        ret.w = (float)((double)a.m30 * (double)v.x + (double)a.m31 * (double)v.y + (double)a.m32 * (double)v.z + (double)a.m33 * (double)v.w);
        return ret;
    }
    public static bool operator ==(MyMatrix4x4 a, MyMatrix4x4 b) => a.GetColumn(0) == b.GetColumn(0) && a.GetColumn(1) == b.GetColumn(1) && a.GetColumn(2) == b.GetColumn(2) && a.GetColumn(3) == b.GetColumn(3);
    public static bool operator !=(MyMatrix4x4 a, MyMatrix4x4 b) => !(a == b);
    public override int GetHashCode()
    {
        Vector4 col = GetColumn(0);
        int hashCode = col.GetHashCode();
        col = GetColumn(1);
        int num1 = col.GetHashCode() << 2;
        int num2 = hashCode ^ num1;
        col = GetColumn(2);
        int num3 = col.GetHashCode() >> 2;
        int num4 = num2 ^ num3;
        col = GetColumn(3);
        int num5 = col.GetHashCode() >> 1;
        return num4 ^ num5;
    }

}