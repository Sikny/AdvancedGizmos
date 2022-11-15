using AG;
using UnityEngine;
using UnityEngine.Profiling;

namespace Experimental {
    public class GizmosTest : MonoBehaviour {
        public int figCount = 5000;

        private void OnDrawGizmos() {
            Profiler.BeginSample("Classic Gizmos");
            Gizmos.color = Color.red;
            var position = -Vector3.one;
            for(int i = 0; i < figCount; ++i)
                Gizmos.DrawSphere(position, 1);
            Profiler.EndSample();

            Profiler.BeginSample("Advanced Gizmos");
            AdvancedGizmos.Begin();
            position = new Vector3(0, 1, -1);
            for (int i = 0; i < figCount; ++i) {
                AdvancedGizmos.DrawSphere(position, 1);
            }
            AdvancedGizmos.Color = Color.green;
            AdvancedGizmos.DrawLine(-2 * Vector3.one, 2 * Vector3.one);
            AdvancedGizmos.Color = Color.blue;
            AdvancedGizmos.DrawLine(-2 * Vector3.one + Vector3.up, 2 * Vector3.one + Vector3.up);
            
            AdvancedGizmos.DrawCube(new Vector3(3, 2, 2), Quaternion.identity, Vector3.one);

            Profiler.EndSample();
        }
    }
}