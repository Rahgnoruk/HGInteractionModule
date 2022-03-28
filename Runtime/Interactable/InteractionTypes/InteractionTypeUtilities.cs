using System.Collections.Generic;

namespace HyperGnosys.InteractionModule
{
    public static class InteractionTypeUtilities
    {
        public static bool InteractionMatch(List<InteractionType> interactionTypes, InteractableTag interactable)
        {
            foreach (InteractionType interactionType in interactionTypes)
            {
                if (interactable.InteractionTypes.Contains(interactionType))
                {
                    return true;
                }
            }
            return false;
        }
        public static (bool, List<InteractionType>) InteractionMatches(List<InteractionType> interactionTypes, InteractableTag interactable)
        {
            (bool, List<InteractionType>) matches = (false, new List<InteractionType>());
            foreach(InteractionType interactionType in interactionTypes)
            {
                if (interactable.InteractionTypes.Contains(interactionType))
                {
                    matches.Item2.Add(interactionType);
                }
            }
            if (matches.Item2.Count > 0)
            {
                matches.Item1 = true;
            }
            return matches;
        }
    }
}