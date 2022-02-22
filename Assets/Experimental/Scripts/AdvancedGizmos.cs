using AdvancedGizmos.Runtime.Shapes;
using UnityEditor;
using UnityEngine;

namespace AdvancedGizmos
{
    [InitializeOnLoad]
    public static class AdvancedGizmos {
        private const int MAXMeshInstance = 1023;
        private static readonly Mesh IcoSphereMesh;
        
        static AdvancedGizmos() {
            IcoSphereMesh = IcoSphere.Create();
        }
        
        public static void DrawSpheresOptimized(Vector3[] centers, float radius, Material material) {
            int passes = centers.Length / MAXMeshInstance;
            int passSize = Mathf.Min(MAXMeshInstance, centers.Length);
            int lastPassSize = centers.Length - passes * passSize;
            var matrices = new Matrix4x4[passSize];

            for (int i = 0; i <= passes; ++i) {
                if(i == passes) passSize = lastPassSize;
                for (int j = 0; j + i * passSize < centers.Length && j < passSize; ++j) {
                    matrices[j] = Matrix4x4.TRS(centers[j + i*passSize], Quaternion.identity, Vector3.one * radius);
                }
                Graphics.DrawMeshInstanced(IcoSphereMesh, 0, material, matrices, passSize);
            }
        }
    }
}