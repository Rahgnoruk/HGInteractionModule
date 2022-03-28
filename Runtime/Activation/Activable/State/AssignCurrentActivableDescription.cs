using HyperGnosys.Core;
using UnityEngine;

namespace HyperGnosys.InteractionModule
{
    public class AssignCurrentActivableDescription : MonoBehaviour
    {
        [SerializeField] private StringObservablePropertyComponent descriptionProperty;

        public void AssignDescription(Activable activable)
        {
            descriptionProperty.Value = activable.Description;
        }
        public void RemoveDescription()
        {
            descriptionProperty.Value = "";
        }

        public StringObservablePropertyComponent DescriptionProperty { get => descriptionProperty; private set => descriptionProperty = value; }
    }
}