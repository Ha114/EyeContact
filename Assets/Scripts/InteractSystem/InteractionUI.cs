using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject InteractOptionPrefab;
    [SerializeField] Transform InteractOptionsContextHolder;

    public IInteractable CurrentTarget { get; private set; }
    void OnEnable()
    {
        CurrentTarget = null;
        CurrentTarget = Player.gameObject.GetComponent<PlayerInteractor>().CurrentTarget;

        var x = CurrentTarget.GetInteractions();
        Debug.Log("InteractionUI count = " + x.Count);

        foreach( var x2 in x)
        {
            Debug.Log("InteractionUI foreach = " + x2.Description);
            GameObject option = Instantiate(InteractOptionPrefab);
            option.transform.SetParent(InteractOptionsContextHolder);
            option.GetComponent<InteractionOption>().SetInteractionDescriptionText(x2.Description);
        }
    }

    void OnDisable()
    {
        CurrentTarget = null;

        foreach (Transform child in InteractOptionsContextHolder) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
