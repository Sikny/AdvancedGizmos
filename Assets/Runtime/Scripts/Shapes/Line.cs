using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

namespace AG.Runtime.Shapes {
    internal struct Line {
        private List<Vector3> _vertices;

        private List<Vector3> Vertices {
            get {
                if (_vertices == null) _vertices = new List<Vector3>();
                return _vertices;
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
            Vertices.Add(start);
            Vertices.Add(end);
        }

        public void Draw() {
            Mesh.indexFormat = IndexFormat.UInt32;
            Mesh.SetVertices(Vertices);
            Mesh.SetIndices(Enumerable.Range(0, Vertices.Count).ToArray(), MeshTopology.Lines, 0);
            Graphics.DrawMeshNow(Mesh, Matrix4x4.identity);
            
            // clear line to prepare for next frame
            Mesh.Clear();
            Vertices.Clear();
        }
    }
}