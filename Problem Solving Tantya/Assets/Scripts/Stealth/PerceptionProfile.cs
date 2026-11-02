using UnityEngine;

namespace Tantya.Stealth
{
    [CreateAssetMenu(fileName = "PerceptionProfile", menuName = "Tantya/Stealth/Perception Profile", order = 1)]
    public class PerceptionProfile : ScriptableObject
    {
        [Header("Vision")]
        [Min(0f)] public float viewDistance = 9.0f;
        [Min(0f)] public float instantDistance = 1.5f;

        [Range(1.0f, 360.0f)] public float focusAngle = 70f;
        [Range(1.0f, 360.0f)] public float peripheralAngle = 140f;
        [Range(0.0f, 1.0f)] public float peripheralStrength = 0.35f;

        public AnimationCurve distanceFallOff;
        public Vector2 eyeOffset = new Vector2(0.1f, 1.4f);
        public LayerMask occluders;

        [Header("Awareness")]
        [Min(0f)] public float gainRate = 1.2f;
        [Min(0f)] public float instantMultiplier = 3.0f;
        [Min(0f)] public float decayRate = 0.25f;
        [Min(0f)] public float decayDelay = 1.5f;
        [Min(0f)] public float combatDecayMultiplier = 0.4f;
        [Range(0.0f, 1.0f)] public float suspiciousThreshold = 0.3f;
        [Range(0.0f, 1.0f)] public float alertThreshold = 0.7f;
        [Range(0.0f, 0.3f)] public float hysteresis = 0.1f;

        [Header("Hearing")]
        [Min(0f)] public float hearingMultiplier = 1f;
        [Min(0f)] public float noiseAwarenessGain = 0.35f;
        [Range(0f, 1f)] public float wallDamping = 0.5f;
        [Range(0.0f, 1f)] public float noiseAwarenessCap = 0.65f;

        [Header("Memory")]
        [Min(0.1f)] public float memoryDuration = 12f;

        [Header("Update/LOD")]
        [Min(0.02f)] public float nearTickInterval = 0.05f;
        [Min(0.02f)] public float farTickInterval = 0.25f;
        [Min(0f)] public float lodDistance = 20f;

        public void OnValidate()
        {
            if (peripheralAngle < focusAngle)
            {
                peripheralAngle = focusAngle;
            }
            
            if (alertThreshold < suspiciousThreshold)
            {
                alertThreshold = suspiciousThreshold;
            }

            if (noiseAwarenessCap >= alertThreshold)
            {
                noiseAwarenessCap = alertThreshold - 0.01f;
            }

            if (instantDistance > viewDistance)
            {
                instantDistance = viewDistance;
            }
        }
    }
}