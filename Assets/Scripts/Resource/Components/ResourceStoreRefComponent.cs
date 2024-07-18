using Leopotam.EcsLite;
using System;
using UnityEngine;
using Voody.UniLeo.Lite;

[Serializable]
public struct ResourceStoreRefComponent
{
    public ConvertToEntity Converter;
    public int ResourceStore;
}