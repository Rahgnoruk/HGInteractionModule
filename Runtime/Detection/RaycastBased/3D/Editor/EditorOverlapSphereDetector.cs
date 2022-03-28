using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace HyperGnosys.InteractionModule
{
    [CustomEditor(typeof(OverlapSphereDetector), true)]
    [CanEditMultipleObjects]
    public class EditorOverlapSphereDetector : EditorARaycastBasedDetector
    {
        private OverlapSphereDetector fov;

        private void OnSceneGUI()
        {
            fov = (OverlapSphereDetector)target;
            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.DetectionRadius);
            float globalViewAngleA = -fov.DetectionAngle / 2 + fov.transform.eulerAngles.y;
            float globalViewAngleB = fov.DetectionAngle / 2 + fov.transform.eulerAngles.y;
            Vector3 drawAngleA = VectorOperations.GetXZVectorDirectionFromAngle(globalViewAngleA);
            Vector3 drawAngleB = VectorOperations.GetXZVectorDirectionFromAngle(globalViewAngleB);

            Handles.DrawLine(fov.transform.position, fov.transform.position + drawAngleA * fov.DetectionRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + drawAngleB * fov.DetectionRadius);
            Handles.color = Color.red;
            if (fov.DetectedInteractables != null && fov.DetectedInteractables.Count > 0)
            {
                foreach (DetectedInteractable targetInView in fov.DetectedInteractables.List)
                {
                    Handles.DrawLine(fov.transform.position, targetInView.Interactable.transform.position);
                }
            }
        }
    }
}
#endif