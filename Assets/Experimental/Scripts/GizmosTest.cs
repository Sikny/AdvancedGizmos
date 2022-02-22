using System;
using UnityEngine;

namespace AdvancedGizmos {
    [ExecuteInEditMode]
    public class GizmosTest : MonoBehaviour {
        public Material gizmosMat;

        /*private void Update() {
            int sphereCount = 10000;
            Vector3 position = Vector3.one;
            var positions = new Vector3[sphereCount];
            for (int i = 0; i < sphereCount; ++i) {
                positions[i] = position;
            }
            double time = EditorApplication.timeSinceStartup;
            AdvancedGizmos.DrawSpheresOptimized(positions, 1, gizmosMat);
            //Debug.Log("Time for drawing gizmos (Custom way) : " + (EditorApplication.timeSinceStartup - time));
        }*/

        private void OnEnable() {
            Camera.onPreCull -= RenderWithCamera;
            Camera.onPreCull += RenderWithCamera;
        }

        private void OnDisable() {
            Camera.onPreCull -= RenderWithCamera;
        }

        private void RenderWithCamera(Camera cam) {
            if (cam) {
                AdvancedGizmos.DrawSpheresOptimized(new[] { Vector3.one }, 1, gizmosMat);
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(-Vector3.one, 1);
            
            Gizmos.DrawCube(new Vector3(0, 0, 0), Vector3.one);
        }

        /*private void OnDrawGizmos() {
            int sphereCount = 10000;
            double time = EditorApplication.timeSinceStartup;
            Gizmos.color = Color.red;
            Vector3 position = -Vector3.one;
            //Gizmos.DrawSphere(position, 1);
            for (int i = 0; i < sphereCount; ++i) {
                Gizmos.DrawSphere(position, 1);
            }
            Debug.Log("Time for drawing gizmos (classic way) : " + (EditorApplication.timeSinceStartup - time));
        }*/
    }
}