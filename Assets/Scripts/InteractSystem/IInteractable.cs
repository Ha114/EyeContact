using System;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Primary,
    Secondary,
    Inspect
}

public interface IInteractable
{
    // void Interact();
    // string GetDescription();
    IReadOnlyList<Interaction> GetInteractions();
    void Interact(InteractionType type);
}

public interface IInteraction
{
    string Id { get; }
    string Description { get; }
    void Execute();
}

public class Interaction : IInteraction
{
    public string Id { get; }
    public string Description { get; }

    private readonly Action execute;

    public Interaction(string id, string description, Action execute)
    {
        Id = id;
        Description = description;
        this.execute = execute;
    }

    public void Execute()
    {
        execute();
    }
}