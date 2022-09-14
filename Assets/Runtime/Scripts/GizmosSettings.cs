using UnityEngine;

namespace AG {
    public class GizmosSettings : ScriptableObject {
        [SerializeField] internal Material gizmosMaterial;
        [SerializeField] internal Material gizmosUnlitMaterial;
    }
}