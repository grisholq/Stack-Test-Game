using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class ResourceStore : MonoBehaviour
{
    [SerializeField] private int _maxAmount;

    public bool Empty => _resources.Count == 0;
    public bool Full => _resources.Count >= _maxAmount;

    public event UnityAction<Resource> ResourceAdded;
    public event UnityAction ResourceRemoved;

    private Stack<Resource> _resources = new Stack<Resource>();

    public void AddResource(Resource resource)
    {
        _resources.Push(resource);
        ResourceAdded?.Invoke(resource);
    }

    public Resource RemoveResource()
    {
        ResourceRemoved?.Invoke();
        return _resources.Pop();
    }
}