using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedGizmos.Runtime.Shapes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;

namespace AdvancedGizmos
{
    [InitializeOnLoad]
    public static class AdvancedGizmos {
        #region Meshes

        private static Mesh _icoSphereMesh;
        private static Mesh _cubeMesh;
        private static Mesh IcoSphereMesh {
            get {
                if (_icoSphereMesh == null) {
                    _icoSphereMesh = IcoSphere.Create();
                }
                return _icoSphereMesh;
            }
        }
        private static Mesh CubeMesh {
            get {
                if (_cubeMesh == null) _cubeMesh = Cube.Create();
                return _cubeMesh;
            }
        }

        private static Mesh _linesMesh;
        private static Vector3[] _lineVertices;
        private static int[] _linesIndices;
        
        #endregion

        
        public static void DrawSphere(Vector3 position, Quaternion rotation, Vector3 scale) {
            Graphics.DrawMeshNow(IcoSphereMesh, Matrix4x4.TRS(position, rotation, scale));
        }

        public static void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale) {
            Graphics.DrawMeshNow(CubeMesh, Matrix4x4.TRS(position, rotation, scale));
        }

        public static void DrawLines(Vector3[] lines) {
            if(_linesMesh == null) _linesMesh = new Mesh();
            if (_linesIndices == null || _linesIndices.Length != lines.Length) {
                _linesIndices = Enumerable.Range(0, lines.Length).ToArray();
            }
            
            _linesMesh.indexFormat = IndexFormat.UInt32;
            _linesMesh.SetVertices(lines);
            _linesMesh.SetIndices(_linesIndices, MeshTopology.Lines, 0);

            Graphics.DrawMeshNow(_linesMesh, Matrix4x4.identity);
        }

        public static void SetMaterial(Material mat, int pass = 0) {
            mat.SetPass(0);
        }
    }
}