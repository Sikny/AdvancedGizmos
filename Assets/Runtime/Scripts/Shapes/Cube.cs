using UnityEngine;

namespace AG.Runtime.Shapes {
    internal static class Cube {
        public static Mesh Create() {
            Mesh mesh = new Mesh();
            mesh.Clear();
            const float p = 0.5f;
            const float n = -p;
            mesh.vertices = new Vector3[] {
                //Z axis
                new Vector3(n, n, n), //0
                new Vector3(n, p, n), //1 
                new Vector3(p, p, n), //2
                new Vector3(p, n, n), //3 (near(-) side)
                new Vector3(n, n, p), //4
                new Vector3(n, p, p), //5
                new Vector3(p, p, p), //6
                new Vector3(p, n, p), //7 (far(+) side)

                //X axis
                new Vector3(n, n, n), //8
                new Vector3(n, n, p), //9
                new Vector3(n, p, p), //10
                new Vector3(n, p, n), //11 (left(-) side)
                new Vector3(p, n, n), //12
                new Vector3(p, n, p), //13
                new Vector3(p, p, p), //14
                new Vector3(p, p, n), //15 (right(+) side)

                //Y axis bottom, top
                new Vector3(n, n, p), //16
                new Vector3(n, n, n), //17
                new Vector3(p, n, n), //18
                new Vector3(p, n, p), //19 (bottom(-) side)
                new Vector3(n, p, p), //20
                new Vector3(n, p, n), //21
                new Vector3(p, p, n), //22
                new Vector3(p, p, p), //23 (top(-) side)
            };

            mesh.triangles = new[] {
                0, 1, 2,
                2, 3, 0, //far
                7, 6, 5,
                5, 4, 7, //near

                8, 9, 10,
                10, 11, 8, //left
                15, 14, 13,
                13, 12, 15, //right

                16, 17, 18,
                18, 19, 16, //bottom
                21, 20, 23,
                23, 22, 21, //top
            };
            mesh.Optimize();
            mesh.RecalculateNormals();
            return mesh;
        }
    }
}