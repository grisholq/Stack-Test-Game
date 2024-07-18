using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using DG.Tweening;

public class ResourceViewSystem: IEcsRunSystem
{
    private EcsFilterInject<Inc<ResourceStoreComponent, ResourceStoreViewComponent>> _storesViews;
    private EcsFilterInject<Inc<ResourceStoreComponent, ResourceStoreViewComponent, ResourceAddEvent>> _resourceAdd;
    private EcsFilterInject<Inc<ResourceStoreComponent, ResourceStoreViewComponent, ResourceAddMultipleEvent>> _resourceAddMultiple;
    private EcsCustomInject<GameConfig> _gameConfigInject;

    public void Run(EcsSystems systems)
    {
        foreach (var entity in _resourceAdd.Value)
        {
            ref var view = ref entity.Get<ResourceStoreViewComponent>();
            ref var store = ref entity.Get<ResourceStoreComponent>();
            var addEvent = entity.Get<ResourceAddEvent>();

            PositionResource(addEvent.ResourceEntity, store, view, addEvent.Instant);
        }

        foreach (var entity in _resourceAddMultiple.Value)
        {
            ref var view = ref entity.Get<ResourceStoreViewComponent>();
            ref var store = ref entity.Get<ResourceStoreComponent>();
            var addEvent = entity.Get<ResourceAddMultipleEvent>();

            PositionResourceMultiple(addEvent.ResourceEntities, store, view, addEvent.Instant);
        }
    }

    private void PositionResource(int resourceEntity, ResourceStoreComponent store, ResourceStoreViewComponent view, bool instant = false)
    {
        var resourceTransform = resourceEntity.Get<TransformComponent>().Transform;

        resourceTransform.parent = view.ResourcesParent;
        var position = view.StoreBase.localPosition + Vector3.up * _gameConfigInject.Value.ResourceHeight * store.Resources.Count;

        if (instant)
        {
            resourceTransform.transform.localPosition = position;
            resourceTransform.localEulerAngles = Vector3.zero;
            return;
        }

        AnimateResourceMove(resourceEntity, resourceTransform, position);
    }

    public void PositionResourceMultiple(List<int> resources, ResourceStoreComponent store, ResourceStoreViewComponent view, bool instant = false)
    {
        for (int i = 0; i < resources.Count; i++)
        {
            var resourceEntity = resources[i];
            var resourceTransform = resourceEntity.Get<TransformComponent>().Transform;
            int startPositionIndex = store.Resources.Count - resources.Count;
            int currentPositionIndex = startPositionIndex + i + 1;

            resourceTransform.parent = view.ResourcesParent;
            var position = view.StoreBase.localPosition + Vector3.up * _gameConfigInject.Value.ResourceHeight * currentPositionIndex;

            if (instant)
            {
                resourceTransform.transform.localPosition = view.StoreBase.localPosition + Vector3.up * _gameConfigInject.Value.ResourceHeight * currentPositionIndex;
                resourceTransform.localEulerAngles = Vector3.zero;
            }
            else AnimateResourceMove(resourceEntity, resourceTransform, position);
        }
    }

    private void AnimateResourceMove(int resourceEntity, Transform resource, Vector3 targetPosition)
    {
        ref var tweens = ref resourceEntity.Get<ResourceTweenAnimations>();
        tweens.ClearAllTweens();

        var sequence = DOTween.Sequence();
        sequence.Append(resource.DOLocalMove(targetPosition + Vector3.up, _gameConfigInject.Value.ResourceMoveTime / 2));
        sequence.Append(resource.DOLocalMove(targetPosition, _gameConfigInject.Value.ResourceMoveTime / 2));
        sequence.Play();
        var rotateTween = resource.DOLocalRotate(Vector3.zero, _gameConfigInject.Value.ResourceMoveTime);

        tweens.AddTween(sequence);
        tweens.AddTween(rotateTween);
    }
}
