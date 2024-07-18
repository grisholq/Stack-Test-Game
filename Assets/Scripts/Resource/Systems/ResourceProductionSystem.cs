using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public class ResourceProductionSystem: IEcsRunSystem, IEcsInitSystem
{
    private EcsFilterInject<Inc<ResourceStoreComponent, ResourceProduction>> _production;
    private EcsCustomInject<GameConfig> _configInject;

    private GameObject ResourcePrefab => _configInject.Value.ResourcePrefab.gameObject;

    public void Init(EcsSystems systems)
    {
        foreach (var entity in _production.Value)
        {
            ref var production = ref entity.Get<ResourceProduction>();
            production.NextProductionTime = Time.time + production.SecondsPerResourceProduction;
        }
    }

    public void Run(EcsSystems systems)
    {
        foreach (var entity in _production.Value)
        {
            ref var production = ref entity.Get<ResourceProduction>();
            ref var store = ref entity.Get<ResourceStoreComponent>();

            if (store.Full) return;

            if (production.Produced(Time.time))
            {
                ref var productionEvent = ref entity.Get<ResourceAddEvent>();
                production.SetNextProductionTime(Time.time);
                productionEvent.ResourceEntity = ResourcePrefab.InstantiateEntity();
                productionEvent.Instant = false;
                PositionResource(productionEvent.ResourceEntity, production);
            }
        }
    }

    private void PositionResource(int resourceEntity, ResourceProduction production)
    {
        var transform = resourceEntity.Get<TransformComponent>().Transform;
        transform.position = production.ProductionPoint.position;
    }
}
