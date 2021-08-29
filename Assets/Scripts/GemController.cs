using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    public GemType type;
    internal bool IsNested { get; set; }

    internal bool Fits(GemType type)
    {
        return this.type == type;
    }
}

public enum GemType
{
    Ruby, Diamond, Emerald, Amethyst, Pearl
}