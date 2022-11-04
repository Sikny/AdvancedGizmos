using System.Collections.Generic;
using AG.Runtime.Shapes;
using UnityEditor;
using UnityEngine;

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
            UpdateMaterialColor();
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

        private static Dictionary<Color, Line> _lines;

        private static Dictionary<Color, Line> Lines {
            get {
                if (_lines == null) _lines = new Dictionary<Color, Line>();
                return _lines;
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

        static AdvancedGizmos() {
            _currentMaterialShader = MaterialShader.Default;

            _color = Color.white;
        }
        #endregion
    }
}