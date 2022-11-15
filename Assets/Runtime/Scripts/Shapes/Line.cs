using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace AG.Runtime.Shapes {
    internal struct Line {
        private List<Vector3> _vertices;
        private List<int> _indices;
        private int _verticesCount;

        private List<Vector3> Vertices {
            get {
                if (_vertices == null) _vertices = new List<Vector3>();
                return _vertices;
            }
        }
        private List<int> Indices {
            get {
                if (_indices == null) _indices = new List<int>();
                return _indices;
            }
        }
        private Mesh _mesh;

        private Mesh Mesh {
            get {
                if (_mesh == null) _mesh = new Mesh();
                return _mesh;
            }
        }

        public Line(Vector3 start, Vector3 end) : this() {
            AddLine(start, end);
        }

        public void AddLine(Vector3 start, Vector3 end) {
            AddVertex(start);
            AddVertex(end);
        }

        private void AddVertex(Vector3 ver) {
            if (_verticesCount > Vertices.Count - 1) {
                Vertices.Add(ver);
                Indices.Add(_verticesCount);
            }
            Vertices[_verticesCount++] = ver;
        }

        public void Draw() {
            Mesh.indexFormat = IndexFormat.UInt32;
            Mesh.SetVertices(Vertices, 0, _verticesCount);
            Mesh.SetIndices(Indices, 0, _verticesCount, MeshTopology.Lines, 0);
            Graphics.DrawMeshNow(Mesh, Matrix4x4.identity);
        }

        /// <summary>
        /// clear line to prepare for next frame
        /// </summary>
        public void Clear() {
            //Mesh.Clear();
            //Vertices.Clear();
            _verticesCount = 0;
        }
    }
}