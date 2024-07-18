using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;

public class ResourceStoreView : MonoBehaviour
{
    //[SerializeField] private ResourceMovementType _movementType;
    [SerializeField] private ResourceStore _store;
    [SerializeField] private Transform _stackBottom;
    [SerializeField] private float _resourcesStackSpace;

    private Stack<Resource> _resources = new Stack<Resource>();

    private void Awake()
    {
        _store.ResourceAdded += AddResourceToTop;
        _store.ResourceRemoved += RemoveResourceFromTop;
    }

    private void AddResourceToTop(Resource resource)
    {
        _resources.Push(resource);

        resource.transform.SetParent(transform);
        var position = GetCurrentPosition();
        var localPosition = transform.InverseTransformPoint(position);
        HandleMovement(resource, localPosition);
    }

    private void HandleMovement(Resource resource, Vector3 localPosition)
    {
        resource.transform.DOLocalJump(localPosition, 1, 1, 1);
        resource.transform.DOLocalRotate(Vector3.zero, 1);
    }

    private void RemoveResourceFromTop()
    {
        _resources.Pop();
    }

    private Vector3 GetCurrentPosition()
    {
        return _stackBottom.position + Vector3.up * _resourcesStackSpace * (_resources.Count - 1);
    }

    private Vector3 GetLocalPosition()
    {
        return Vector3.up * _resourcesStackSpace * (_resources.Count - 1);
    }
}