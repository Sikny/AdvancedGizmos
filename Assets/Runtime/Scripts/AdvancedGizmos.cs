using System.Collections.Generic;
using System.Linq;
using AG.Runtime.Shapes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace AG {
    [InitializeOnLoad]
    public static partial class AdvancedGizmos {
        #region Material
        internal enum MaterialShader {
            Default,
            Unlit,
            None
        }
        private static Color _color;
        private static MaterialShader _currentMaterialShader;
        private static Material _currentMat;

        private static Material CurrentMat {
            get {
                if (_currentMat == null) {
                    _currentMat = GizmosSettings.gizmosMaterial;
                }
                return _currentMat;
            }
            set => _currentMat = value;
        }
        
        private static void SetMaterialType(MaterialShader newShader) {
            if (_currentMat != null && _currentMaterialShader == newShader) return;
            _currentMaterialShader = newShader;
            CurrentMat = GetMaterial(_currentMaterialShader);
            SetPass();
        }

        private static void SetPass() {
            CurrentMat.SetPass(0);
        }

        private static void UpdateMaterialColor() {
            if (_currentMaterialShader == MaterialShader.None) _currentMaterialShader = MaterialShader.Default;
            CurrentMat = GetMaterial(_currentMaterialShader);
            CurrentMat.color = _color;
            SetPass();
        }

        private static Material GetMaterial(MaterialShader shader) {
            switch (shader) {
                case MaterialShader.Default:
                    return GizmosSettings.gizmosMaterial;
                case MaterialShader.Unlit:
                    return GizmosSettings.gizmosUnlitMaterial;
            }
            return null;
        }
        
        #endregion
        
        #region Meshes
        private static Mesh _icoSphereMesh;
        private static Mesh IcoSphereMesh {
            get {
                if (_icoSphereMesh == null) {
                    _icoSphereMesh = IcoSphere.Create();
                }
                return _icoSphereMesh;
            }
        }
        private static Mesh _cubeMesh;
        private static Mesh CubeMesh {
            get {
                if (_cubeMesh == null) _cubeMesh = Cube.Create();
                return _cubeMesh;
            }
        }

        private static Mesh _linesMesh;

        private static Mesh LinesMesh {
            get {
                if (_linesMesh == null) _linesMesh = new Mesh();
                return _linesMesh;
            }
        }
        private static List<Vector3> _linesVertices;

        private static List<Vector3> LinesVertices {
            get {
                if (_linesVertices == null) _linesVertices = new List<Vector3>();
                return _linesVertices;
            }
        }
        #endregion

        #region Rendering
        private static GizmosSettings _gizmosSettings;
        private static GizmosSettings GizmosSettings {
            get {
                if (_gizmosSettings == null) {
                    var assetsGuid = AssetDatabase.FindAssets($"t:{typeof(GizmosSettings)}");
                    var assetPath = AssetDatabase.GUIDToAssetPath(assetsGuid[0]);
                    _gizmosSettings = AssetDatabase.LoadAssetAtPath<GizmosSettings>(assetPath);
                }
                return _gizmosSettings;
            }
        }
        
        private static Camera _sceneCamera;

        static AdvancedGizmos() {
            _currentMaterialShader = MaterialShader.Default;

            _color = Color.white;

            Camera.onPostRender -= OnCameraPostRender;
            Camera.onPostRender += OnCameraPostRender;
        }

        private static void OnCameraPostRender(Camera cam) {
            if (LinesVertices.Count > 0) {
                LinesMesh.indexFormat = IndexFormat.UInt32;
                LinesMesh.SetVertices(LinesVertices.ToArray());
                LinesMesh.SetIndices(Enumerable.Range(0, LinesVertices.Count).ToArray(), MeshTopology.Lines, 0);

                SetMaterialType(MaterialShader.Unlit);
                Graphics.DrawMeshNow(_linesMesh, Matrix4x4.identity);
            }

            // clear lines to prepare for next frame
            LinesMesh.Clear();
            LinesVertices.Clear();
        }
        #endregion
    }
}