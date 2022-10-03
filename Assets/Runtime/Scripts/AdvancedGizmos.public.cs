using System;
using UnityEngine;

namespace AG {
    public static partial class AdvancedGizmos {
        public static Color Color {
            get => _color;
            set {
                if (_color == value) return;
                _color = value;
                UpdateMaterialColor();
            }
        }

        public static void Begin() {
            SetPass();
        }
        
        public static void DrawSphere(Vector3 position, float radius) {
            DrawSphere(position, Quaternion.identity, radius * Vector3.one);
        }

        public static void DrawSphere(Vector3 position, Quaternion rotation, float radius) {
            DrawSphere(position, rotation, radius * Vector3.one);
        }

        public static void DrawSphere(Vector3 position, Quaternion rotation, Vector3 scale) {
            SetMaterialType(MaterialShader.Default);
            Graphics.DrawMeshNow(IcoSphereMesh, Matrix4x4.TRS(position, rotation, scale));
        }

        public static void DrawCube(Vector3 position, Quaternion rotation, Vector3 scale) {
            //SetMaterialType(MaterialShader.Default);
            //Graphics.DrawMeshNow(CubeMesh, Matrix4x4.TRS(position, rotation, scale));
            Graphics.DrawMeshNow(CubeMesh, Matrix4x4.TRS(position, rotation, scale));
        }

        public static void DrawLine(Vector3 start, Vector3 end) {
            LinesVertices.Add(start);
            LinesVertices.Add(end);
        }
    }
}