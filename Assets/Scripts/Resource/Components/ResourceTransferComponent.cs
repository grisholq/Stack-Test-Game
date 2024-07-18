using System;
using UnityEngine;

[Serializable]
public struct ResourceTransferComponent
{
    public int ResourceStoreFrom;
    public int ResourceStoreTo;

    public float NextTransferTime;
}