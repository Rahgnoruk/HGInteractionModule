using HyperGnosys.Core;
using UnityEngine;
namespace HyperGnosys.InteractionModule
{
    public class RaycastDetector : ARaycastBasedDetector
    {
        [SerializeField, Tooltip("Object from which the Raycast " +
            "is produced. It will be shot in it's forward direction." +
            "If it's left blank, this object will be used")]
        private Transform raycastOrigin;

        [SerializeField, Tooltip("Range at which this object" +
            "can detect Raycastables")]
        private float maxDistance = 1.5f;

        ///Variable that stores the object we are 
        ///currently looking at. Its a gameobject so we can know when we are seeing a new,
        ///and therefore unprocessed, object.
        [SerializeField] private GameObject currentHitObject;
        [Tooltip("This event fires whenever the Raycast hits something, even if it's not Raycastable")]
        [SerializeField] private GameObjectEventProperty onRaycastEnterEvent = new GameObjectEventProperty();
        [SerializeField] private GameObjectEventProperty onRaycastExitEvent = new GameObjectEventProperty();

        RaycastHit hitInfo = new RaycastHit();

        void Start()
        {
            if (raycastOrigin == null)
            {
                this.raycastOrigin = this.transform;
            }
        }

        /// <summary>
        /// Every fixed update shoot a Raycast. If it doesn't hit, Notify all the subscribers
        /// that this object is no longer looking at something. If it hits, check if the hit 
        /// object is equal to the one we have stored (the object we were looking at).
        /// If it's the same, then we don't need to do anything else because we have already
        /// assigned it and Notified the subscribers. If it's a different object, then check if
        /// it's Raycastable. If it's not Raycastable, Notify the subscribers that we are no longer 
        /// looking at a Raycastable. If it's Raycastable, set the hit object as the one we are looking at,
        /// activate the Raycastable Raycasted Method and Notify the subscribers that we are looking at a Raycastable. 
        /// </summary>
        /// <param name="collision"></param>
        void FixedUpdate()
        {
            ///Shoot a Raycast and see if it hits something
            if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, maxDistance))
            {
                ///Check if the hit object is different from the one we had seen before.
                ///If we were already looking at it, we already processed it and stored it, wether it's Raycastable or not.
                if (this.currentHitObject != hitInfo.transform.gameObject)
                {
                    ///Since we are definitely not looking at the same object
                    ///as before, lets process the new object

                    ///If we are this.Debugging, check if we used to be looking at nothing and notify
                    if (currentHitObject == null)
                    {
                        HGDebug.Log("RaycastEvent in " + transform.name + " was hitting nothing and it now"
                            + " started hitting " + hitInfo.transform?.name, this.Debugging);
                    }

                    ///First, lets deal with any object we might have been seeing.
                    UnseeObject();

                    ///Save the new object that we are looking at
                    ///(We are sure there's an object because we are inside the RaycastHit condition)
                    currentHitObject = hitInfo.transform.gameObject;
                    onRaycastEnterEvent.Raise(currentHitObject);
                    HGDebug.Log($"RaycastEvent in {transform.name} started hitting {currentHitObject?.name}", this.Debugging);

                    ///See if the collided object is interactable and can be interacted through this Detector's sensors
                    ///If it is, the base class will raise the perinent events
                    if(!OnObjectEnter(currentHitObject, false))
                    {
                        return;
                    }
                }
                ///Else the hit object has already been processed, raycastable or not
            }
            ///If it's not hitting something
            else
            {
                ///If we used to be looking at something, unsee it
                UnseeObject();
            }
        }

        /// <summary>
        /// Called when we stop seeing an object, Raycastable or Not
        /// </summary>
        private void UnseeObject()
        {
            ///If we weren't looking at an object before, there's no need to raise RaycastExit
            ///So only raise RaycastExit if we were looking at an object
            if (currentHitObject != null)
            {
                HGDebug.Log($"RaycastEvent in {transform.name} stopped hitting {currentHitObject?.name}", this.Debugging);
                ///Tell suscribers that we are no longer looking at this object
                onRaycastExitEvent.Raise(currentHitObject);
                ///Check if the object we are no longer looking at was being Detected by our Sensors
                ///Notify accordingly
                OnObjectExit(currentHitObject, false);
                currentHitObject = null;
            }
        }
        public GameObjectEventProperty OnRaycastEnterEvent { get => onRaycastEnterEvent; set => onRaycastEnterEvent = value; }
        public GameObjectEventProperty OnRaycastExitEvent { get => onRaycastExitEvent; set => onRaycastExitEvent = value; }
        private GameObject CurrentHitObject { get => currentHitObject; set => currentHitObject = value; }
    }
}