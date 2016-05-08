using UnityEngine;
using System.Collections;

public class InteractionEventManager : MonoBehaviour {

    public delegate void InteractionAction(bool bIsBeginning);
    public static event InteractionAction OnInteraction;
    

    

    //bIsBeginning: true if a hand is engaging in the interaction, false is hand is ending the interaction
    public static void ReportInteraction(bool bIsBeginning)
    {
        if(OnInteraction != null)
        {
            OnInteraction(bIsBeginning);
        }
    }
}
