using HyperGnosys.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    /// <summary>
    /// Tutorial de Sebastian Lague:
    /// https://www.youtube.com/watch?v=rQG9aUWarwE&list=PLdsay4PZtiH4CZKg13FwPMbhkiv74lB84&index=32
    /// Tutorial de CodeMonkey: 
    /// https://www.youtube.com/watch?v=CSeUMTaNFYk&list=PLdsay4PZtiH4CZKg13FwPMbhkiv74lB84&index=13
    /// </summary>
    public class OverlapSphereDetector : ARaycastBasedDetector
    {
        #region Attributes
        [SerializeField] private bool canDetect = true;
        [SerializeField] private Transform fieldOfViewOrigin;
        [Range(0, 360)]
        [SerializeField] private int detectionAngle = 90;
        [SerializeField] private float detectionRadius = 20f;
        [SerializeField] private bool registerExiting = false;
        private Coroutine findTargetsCoroutine;
        private Collider[] collidersInRange;
        private HashSet<GameObject> targetsInRange = new HashSet<GameObject>();
        private HashSet<GameObject> processedTargets = new HashSet<GameObject>();
        private GameObject target;
        #endregion

        #region Field Of View Mesh
        [SerializeField] private bool drawFieldOfView = true;
        [SerializeField] private LayerMask obstacleLayerMask = 1;
        [SerializeField] private MeshFilter meshFilter;
        [Range(0, 1)]
        [SerializeField] private float meshResolution = 0.5f;
        [SerializeField] private bool improveEdges = false;
        [SerializeField] private int improveEdgesIterations = 4;
        [SerializeField] private float edgeDistanceThreshold = 0.5f;
        [SerializeField] private bool addWallPenetration = false;
        [SerializeField] private float penetrationDepth = 0.2f;
        #endregion
        private Mesh mesh;

        private void OnEnable()
        {
            if(fieldOfViewOrigin == null)
            {
                fieldOfViewOrigin = this.transform;
            }
            mesh = new Mesh();
            mesh.name = "View Mesh";
            meshFilter.mesh = mesh;
            if (meshFilter == null)
            {
                HGDebug.LogError($"No se asgignó el MeshFilter en {transform.name}", Debugging);
            }
            findTargetsCoroutine = StartCoroutine(FindTargets());
        }
        private void LateUpdate()
        {
            if (drawFieldOfView)
            {
                ///Se llama en LateUpdate para que el resto del codigo 
                ///ya se haya realizado y el personaje ya haya rotado
                DrawFieldOfView();
            }
        }

        private IEnumerator FindTargets()
        {
            while (canDetect)
            {
                if (DetectionUpdateRate <= 0)
                {
                    yield return new WaitForFixedUpdate();
                }
                else
                {
                    yield return new WaitForSeconds(DetectionUpdateRate);
                }
                FindVisibleTargets();
            }
        }
        
        public void FindVisibleTargets()
        {
            collidersInRange = Physics.OverlapSphere(fieldOfViewOrigin.position, detectionRadius, TargetLayerMask);
            targetsInRange.Clear();
            for (int i = 0; i < collidersInRange.Length; i++)
            {
                target = collidersInRange[i].gameObject;
                targetsInRange.Add(target);
            }
            ///Quita los targets que viste pero ya no son visibles
            processedTargets.RemoveWhere(IsNoLongerInRange);
            AddNewTargetInRange();
        }
        private bool IsNoLongerInRange(GameObject processedTarget)
        {
            if (processedTarget == null) return true;
            bool remainsInRange = targetsInRange.Contains(processedTarget);
            if (!remainsInRange)
            {
                HGDebug.Log($"{processedTarget.name} is no longer in range", this, Debugging);
                OnObjectExit(processedTarget, registerExiting);
                ///Verdadero para que RemoveWhere quite este target de los procesados
                return true;
            }
            else
            {
                HGDebug.Log($"{processedTarget.name} remains in range", this, Debugging);
            }
            ///Si sigue siendo visible no lo quitas del HashSet
            return false;
        }
        private void AddNewTargetInRange()
        {
            ///Por cada target que estas viendo
            foreach (GameObject targetInRange in targetsInRange)
            {
                ///Si no lo viste la vez pasada entonces es nuevo
                if (!processedTargets.Contains(targetInRange))
                {
                    HGDebug.Log($"{targetInRange.name} came into range", this, Debugging);
                    OnObjectEnter(targetInRange, registerExiting);
                    processedTargets.Add(targetInRange);
                }
            }
        }
        public int DetectionAngle { get => detectionAngle; set => detectionAngle = value; }
        public float DetectionRadius { get => detectionRadius; set => detectionRadius = value; }

        private int rayAmount;
        private float currentAngle;
        private float angleIncrease;

        public void DrawFieldOfView()
        {
            rayAmount = Mathf.RoundToInt(detectionAngle * meshResolution);
            ///angleIncrease = ViewAngle / rayAmount;
            ///angleIncrease = ViewAngle / (ViewAngle * meshRaysPerAngle);
            angleIncrease = 1 / meshResolution;
            List<Vector3> viewPoints = new List<Vector3>();
            ViewCastInfo oldViewCast = new ViewCastInfo();
            for (int i = 0; i <= rayAmount; i++)
            {
                //vertex = origin + VectorOperations.GetXZVectorDirectionFromAngle(currentAngle) * ViewDistance;
                currentAngle = transform.eulerAngles.y - detectionAngle / 2 + angleIncrease * i;
                ViewCastInfo newViewCast = ViewCast(currentAngle);
                if (improveEdges && i > 0)
                {
                    bool edgeDistanceThresholdExceeded =
                        Mathf.Abs(oldViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;
                    if (oldViewCast.hit != newViewCast.hit || oldViewCast.hit && newViewCast.hit
                        && edgeDistanceThresholdExceeded)
                    {
                        EdgeInfo edge = FindEdge(oldViewCast, newViewCast);
                        if (edge.pointA != Vector3.zero)
                        {
                            viewPoints.Add(edge.pointA);
                        }
                        if (edge.pointB != Vector3.zero)
                        {
                            viewPoints.Add(edge.pointB);
                        }
                    }
                }
                viewPoints.Add(newViewCast.point);
                oldViewCast = newViewCast;

                HGDebug.DrawLine(transform.position,
                        transform.position + VectorOperations.GetXZVectorDirectionFromAngle(currentAngle) * detectionRadius,
                        Debugging, Color.red);
            }
            int vertexCount = viewPoints.Count + 1;
            Vector3[] vertices = new Vector3[vertexCount];
            ///Cada tres número representan un triángulo
            ///cada número es el index de cada vértice del triángulo
            ///Es -2 porque cada triángulo comparte tanto el vértice del 
            ///origen como el segundo con el triángulo anterior.
            int[] triangleVertexIndexes = new int[(vertexCount - 2) * 3];
            ///El ViewMesh será un hijo del GameObject al que esté agregado este script
            ///así que todas las posiciones se deben calcular tomando en cuenta que 
            ///el origen es la posición de este GameObject
            vertices[0] = Vector3.zero;
            Vector3 vertex = Vector3.zero;
            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertex = viewPoints[i] + Vector3.forward * penetrationDepth * Convert.ToInt16(addWallPenetration);
                vertices[i + 1] = transform.InverseTransformPoint(vertex);
                if (i < vertexCount - 2)
                {
                    triangleVertexIndexes[i * 3] = 0;
                    triangleVertexIndexes[i * 3 + 1] = i + 1;
                    triangleVertexIndexes[i * 3 + 2] = i + 2;
                }
            }
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangleVertexIndexes;
            mesh.RecalculateNormals();
        }
        private ViewCastInfo ViewCast(float globalAngle)
        {
            Vector3 dir = VectorOperations.GetXZVectorDirectionFromAngle(globalAngle);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, dir, out hit, detectionRadius, obstacleLayerMask))
            {
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            else
            {
                return new ViewCastInfo(false, transform.position + dir * detectionRadius, detectionRadius, globalAngle);
            }
        }

        private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
        {
            float minAngle = minViewCast.angle;
            float maxAngle = maxViewCast.angle;
            Vector3 minPoint = Vector3.zero;
            Vector3 maxPoint = Vector3.zero;

            for (int i = 0; i < improveEdgesIterations; i++)
            {
                float angle = (minAngle + maxAngle) / 2;
                ViewCastInfo newViewCast = ViewCast(angle);
                bool edgeDistanceThresholdExceeded =
                        Mathf.Abs(minViewCast.distance - newViewCast.distance) > edgeDistanceThreshold;
                if (newViewCast.hit == minViewCast.hit && !edgeDistanceThresholdExceeded)
                {
                    minAngle = angle;
                    minPoint = newViewCast.point;
                }
                else
                {
                    maxAngle = angle;
                    maxPoint = newViewCast.point;
                }
            }
            return new EdgeInfo(minPoint, maxPoint);
        }

        public struct ViewCastInfo
        {
            public bool hit;
            public Vector3 point;
            public float distance;
            public float angle;

            public ViewCastInfo(bool hit, Vector3 point, float distance, float angle)
            {
                this.hit = hit;
                this.point = point;
                this.distance = distance;
                this.angle = angle;
            }
        }

        public struct EdgeInfo
        {
            public Vector3 pointA;
            public Vector3 pointB;

            public EdgeInfo(Vector3 pointA, Vector3 pointB)
            {
                this.pointA = pointA;
                this.pointB = pointB;
            }
        }
    }
}