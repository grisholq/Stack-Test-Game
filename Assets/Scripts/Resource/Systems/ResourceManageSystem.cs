using UnityEngine;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public class ResourceManageSystem: IEcsInitSystem, IEcsRunSystem
{
    private EcsFilterInject<Inc<ResourceStoreRefComponent>> _storeRefs;
    private EcsFilterInject<Inc<ResourceStoreComponent>> _resourceStores;
    private EcsFilterInject<Inc<ResourceStoreComponent, ResourceAddEvent>> _resourceIncome;
    private EcsFilterInject<Inc<ResourceStoreComponent, ResourceAddMultipleEvent>> _resourceIncomeMultiple;
    private EcsFilterInject<Inc<ResourceStoreComponent, ResourceRemoveEvent>> _resourceRemove;
    private EcsCustomInject<GameConfig> _config;

    public void Init(EcsSystems systems)
    {
        foreach (var entity in _storeRefs.Value)
        {
            ref var storeRef = ref entity.Get<ResourceStoreRefComponent>();
            storeRef.ResourceStore = storeRef.Converter.TryGetEntity().Value;
        }

        List<int> resources = new List<int>();

        foreach (var entity in _resourceStores.Value)
        {
            ref var store = ref entity.Get<ResourceStoreComponent>();
            ref var addResourceEvent = ref entity.Get<ResourceAddMultipleEvent>();

            store.Resources = new Stack<int>();

            for (int i = 0; i < store.StartResources; i++)
            {
                var resourceEntity = _config.Value.ResourcePrefab.InstantiateEntity();
                resources.Add(resourceEntity);
            }

            addResourceEvent.Instant = true;
            addResourceEvent.ResourceEntities = resources;
            resources = new List<int>();
        }
    }

    public void Run(EcsSystems systems)
    {
        foreach (var entity in _resourceRemove.Value)
        {
            ref var store = ref entity.Get<ResourceStoreComponent>();
            store.Resources.Pop();
        }

        foreach (var entity in _resourceIncome.Value)
        {
            ref var store = ref entity.Get<ResourceStoreComponent>();
            var addEvent = entity.Get<ResourceAddEvent>();

            store.Resources.Push(addEvent.ResourceEntity);
        }

        foreach (var entity in _resourceIncomeMultiple.Value)
        {
            ref var store = ref entity.Get<ResourceStoreComponent>();
            var addEvent = entity.Get<ResourceAddMultipleEvent>();

            foreach (var resourceEntity in addEvent.ResourceEntities)
            {
                store.Resources.Push(resourceEntity);
            }
        }
    }
}