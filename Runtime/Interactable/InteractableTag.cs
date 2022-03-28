using HyperGnosys.Core;
using UnityEngine;
using UnityEngine.Events;

namespace HyperGnosys.InteractionModule
{
    public class InteractableTag : BodyTag
    {
        [SerializeField] private ObservableList<InteractionType> interactionTypes =  new ObservableList<InteractionType>();
        [SerializeField, HideInInspector] private bool isFirstReset = true;
        private UnityEvent<InteractableTag> onInteractionTypesChanged = new UnityEvent<InteractableTag>();
        private UnityEvent<InteractableTag> onInteractableDisabled = new UnityEvent<InteractableTag>();

        protected override void Awake()
        {
            base.Awake();
            interactionTypes.AddOnItemAddedListener(RaiseOnInteractionsChanged);
            interactionTypes.AddOnItemRemovedListener(RaiseOnInteractionsChanged);
        }
        private void RaiseOnInteractionsChanged(InteractionType interaction)
        {
            onInteractionTypesChanged.Invoke(this);
        }
        private void OnDisable()
        {
            onInteractableDisabled.Invoke(this);
        }
        public ObservableList<InteractionType> InteractionTypes { get => interactionTypes; }
        public UnityEvent<InteractableTag> OnInteractionTypesChanged { get => onInteractionTypesChanged; set => onInteractionTypesChanged = value; }
        public UnityEvent<InteractableTag> OnInteractableDisabled { get => onInteractableDisabled; set => onInteractableDisabled = value; }
    }
}