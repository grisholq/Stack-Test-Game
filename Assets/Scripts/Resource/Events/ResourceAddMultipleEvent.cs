using System;
using System.Collections.Generic;

[Serializable]
public struct ResourceAddMultipleEvent
{
    public List<int> ResourceEntities;
    public bool Instant;
}