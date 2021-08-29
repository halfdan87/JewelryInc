using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBus
{
    internal delegate void GrabbingStateChangedEvent(object sender, BoolArgs args);
    internal event GrabbingStateChangedEvent GrabbingChanged;

    internal delegate void GemFittedEvent(object sender, GemArgs args);
    internal event GemFittedEvent GemFitted;

    internal delegate void GemCaughtEvent(object sender, GemArgs args);
    internal event GemCaughtEvent GemCaught;

    internal delegate void JewelryBoxedEvent(object sender, JewelryArgs args);
    internal event JewelryBoxedEvent JewelryBoxed;

    internal delegate void GemCreatedEvent();
    internal event GemCreatedEvent GemCreated;

    internal delegate void TimeUpEvent();
    internal event TimeUpEvent TimeUp;

    internal delegate void LevelClearedEvent();
    internal event LevelClearedEvent LevelCleared;

    internal void InvokeGrabbingChanged(object sender, bool grabbing)
    {
        GrabbingChanged?.Invoke(sender, new BoolArgs(grabbing));
    }

    internal void InvokeGemFitted(object sender, GemController gem)
    {
        GemFitted?.Invoke(sender, new GemArgs(gem));
    }
    internal void InvokeGemCaught(object sender, GemController gem)
    {
        GemCaught?.Invoke(sender, new GemArgs(gem));
    }
    internal void InvokeGemBoxed(BoxController sender, JewelryController jewelry)
    {
        JewelryBoxed?.Invoke(sender, new JewelryArgs(jewelry));
    }
    internal void InvokeGemCreated()
    {
        GemCreated?.Invoke();
    }

    internal void InvokeTimeUp()
    {
        TimeUp?.Invoke();
    }

    internal void InvokeLevelCleared()
    {
        LevelCleared?.Invoke();
    }
}


public class BoolArgs : EventArgs
{
    public BoolArgs(bool grabbing)
    {
        this.State = grabbing;
    }

    public bool State { get; }
}

public class GemArgs : EventArgs
{
    public GemArgs(GemController gem)
    {
        this.Gem = gem;
    }

    public GemController Gem { get; }
}

public class JewelryArgs : EventArgs
{
    public JewelryArgs(JewelryController jewelry)
    {
        this.Jewelry = jewelry;
    }

    public JewelryController Jewelry { get; }
}