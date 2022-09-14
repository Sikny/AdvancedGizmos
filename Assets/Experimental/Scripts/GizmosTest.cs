using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace Experimental {
    public class GizmosTest : MonoBehaviour {
        public Material gizmosMat;
        public int figCount = 5000;

        private void OnDrawGizmos() {
            Profiler.BeginSample("Classic Gizmos");
            Gizmos.color = Color.red;
            var position1 = -Vector3.one;
            var position2 = Vector3.one;
            for(int i = 0; i < figCount; ++i)
                Gizmos.DrawSphere(position1, 1);
            Profiler.EndSample();

            Profiler.BeginSample("Advanced Gizmos - Draw Mesh Now");
            position1 = new Vector3(0, 1, -1);
            position2 = new Vector3(0, -1, 1);
            //var lines = new Vector3[figCount * 2];
            for (int i = 0; i < figCount * 2 - 1; ++i) {
                /*lines[i] = position1;
                lines[i + 1] = position2;*/
                AG.AdvancedGizmos.DrawSphere(position1, 1);
            }
            //AdvancedGizmos.DrawLines(lines);

            Profiler.EndSample();
        }
    }
}