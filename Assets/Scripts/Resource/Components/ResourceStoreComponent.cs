using System;
using System.Collections.Generic;

[Serializable]
public struct ResourceStoreComponent
{
    public int MaxResources;
    public int StartResources;
    public int FreeSpace => MaxResources - Resources.Count;
    public bool Full => Resources.Count >= MaxResources;
    public bool Empty => Resources.Count == 0;
    public Stack<int> Resources;
}