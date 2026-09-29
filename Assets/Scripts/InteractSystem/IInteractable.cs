using System;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionSlot
{
    Primary,
    Secondary,
    Tertiary
}
public interface IInteractable
{
    // void Interact();
    // string GetDescription();
    IReadOnlyList<Interaction> GetInteractions();
    //void Interact(InteractionSlot type);
}

public interface IInteraction
{
    string Id { get; }
    string Description { get; }
    void Execute();
}

public class Interaction
{
    public InteractionSlot Slot { get; }
    public string Id { get; }
    public string Descriptiom { get; }

    private readonly Action execute;

    public Interaction(
        InteractionSlot slot,
        string id,
        string displayName,
        Action execute)
    {
        Slot = slot;
        Id = id;
        Descriptiom = displayName;
        this.execute = execute;
    }

    public void Execute()
    {
        execute();
    }
}