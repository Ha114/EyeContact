using System.Collections.Generic;
using UnityEngine;

public class ArchiwumItem : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject Archive;

    public void Interact()
    {
        Debug.Log(gameObject.name + " - Interact");
 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Archive.SetActive(!Archive.activeInHierarchy);
    }

    public string GetDescription()
    {
        return "Open Archiwum";
    }


    public IReadOnlyList<Interaction> GetInteractions()
    {
        throw new System.NotImplementedException();
    }
}
