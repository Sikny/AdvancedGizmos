using System.Collections.Generic;
using UnityEngine;

namespace AG.Runtime.Shapes {
    internal static class IcoSphere {
        // return index of point in the middle of p1 and p2
        private static int GetMiddlePoint(int p1, int p2, ref List<Vector3> vertices, ref Dictionary<long, int> cache,
            float radius) {
            // first check if we have it already
            bool firstIsSmaller = p1 < p2;
            long smallerIndex = firstIsSmaller ? p1 : p2;
            long greaterIndex = firstIsSmaller ? p2 : p1;
            long key = (smallerIndex << 32) + greaterIndex;

            if (cache.TryGetValue(key, out var ret)) {
                return ret;
            }

            // not in cache, calculate it
            Vector3 point1 = vertices[p1];
            Vector3 point2 = vertices[p2];
            Vector3 middle = new Vector3
            (
                (point1.x + point2.x) / 2f,
                (point1.y + point2.y) / 2f,
                (point1.z + point2.z) / 2f
            );

            // add vertex makes sure point is on unit sphere
            int i = vertices.Count;
            vertices.Add(middle.normalized * radius);

            // store it, return index
            cache.Add(key, i);

            return i;
        }

        public static Mesh Create() {
            Mesh mesh = new Mesh();
            mesh.Clear();
            Vector3[] vertices = mesh.vertices;
            Dictionary<long, int> middlePointIndexCache = new Dictionary<long, int>();

            int recursionLevel = 1; // todo as parameter
            float radius = 1f; // todo as parameter

            // create 12 vertices of a icosahedron
            float t = (1f + Mathf.Sqrt(5f)) / 2f;

            List<Vector3> vertList = new List<Vector3> {
                new Vector3(-1f, t, 0f).normalized * radius,
                new Vector3(1f, t, 0f).normalized * radius,
                new Vector3(-1f, -t, 0f).normalized * radius,
                new Vector3(1f, -t, 0f).normalized * radius,
                new Vector3(0f, -1f, t).normalized * radius,
                new Vector3(0f, 1f, t).normalized * radius,
                new Vector3(0f, -1f, -t).normalized * radius,
                new Vector3(0f, 1f, -t).normalized * radius,
                new Vector3(t, 0f, -1f).normalized * radius,
                new Vector3(t, 0f, 1f).normalized * radius,
                new Vector3(-t, 0f, -1f).normalized * radius,
                new Vector3(-t, 0f, 1f).normalized * radius
            };


            // create 20 triangles of the icosahedron
            List<int[]> faces = new List<int[]> {
                // 5 faces around point 0
                new[] { 0, 11, 5 },
                new[] { 0, 5, 1 },
                new[] { 0, 1, 7 },
                new[] { 0, 7, 10 },
                new[] { 0, 10, 11 },
                // 5  adjacent faces 
                new[] { 1, 5, 9 },
                new[] { 5, 11, 4 },
                new[] { 11, 10, 2 },
                new[] { 10, 7, 6 },
                new[] { 7, 1, 8 },
                // 5  faces around point 3
                new[] { 3, 9, 4 },
                new[] { 3, 4, 2 },
                new[] { 3, 2, 6 },
                new[] { 3, 6, 8 },
                new[] { 3, 8, 9 },
                // 5  adjacent faces 
                new[] { 4, 9, 5 },
                new[] { 2, 4, 11 },
                new[] { 6, 2, 10 },
                new[] { 8, 6, 7 },
                new[] { 9, 8, 1 }
            };


            // refine triangles
            for (int i = 0; i < recursionLevel; ++i) {
                List<int[]> faces2 = new List<int[]>();
                foreach (var tri in faces) {
                    // replace triangle by 4 triangles
                    int a = GetMiddlePoint(tri[0], tri[1], ref vertList, ref middlePointIndexCache, radius);
                    int b = GetMiddlePoint(tri[1], tri[2], ref vertList, ref middlePointIndexCache, radius);
                    int c = GetMiddlePoint(tri[2], tri[0], ref vertList, ref middlePointIndexCache, radius);

                    faces2.Add(new[] { tri[0], a, c });
                    faces2.Add(new[] { tri[1], b, a });
                    faces2.Add(new[] { tri[2], c, b });
                    faces2.Add(new[] { a, b, c });
                }

                faces = faces2;
            }

            mesh.vertices = vertList.ToArray();

            List<int> triList = new List<int>();
            for (int i = 0; i < faces.Count; ++i) {
                triList.Add(faces[i][0]);
                triList.Add(faces[i][1]);
                triList.Add(faces[i][2]);
            }

            mesh.triangles = triList.ToArray();
            mesh.uv = new Vector2[vertices.Length];

            Vector3[] normals = new Vector3[vertList.Count];
            for (int i = 0; i < normals.Length; ++i)
                normals[i] = vertList[i].normalized;


            mesh.normals = normals;

            mesh.RecalculateBounds();
            mesh.RecalculateTangents();
            mesh.RecalculateNormals();
            //mesh.Optimize();

            return mesh;
        }
    }
}