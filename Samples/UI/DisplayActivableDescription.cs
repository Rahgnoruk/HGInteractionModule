using TMPro;
using UnityEngine;
namespace HyperGnosys.InteractionModule
{
    public class DisplayActivableDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public void DisplayDescription(Activable activable)
        {
            text.text = activable.Description;
        }
        public void HideDescription()
        {
            text.text = "";
        }

        public TMP_Text Text { get => text; private set => text = value; }
    }
}