using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public class ResourceTransferSystem: IEcsRunSystem
{
    private EcsFilterInject<Inc<ResourceTransferComponent>> _transfers;
    private EcsCustomInject<GameConfig> _config;

    public void Run(EcsSystems systems)
    {
        foreach (var entity in _transfers.Value)
        {
            ref var transfer = ref entity.Get<ResourceTransferComponent>();
            ref var storeTakeFrom = ref transfer.ResourceStoreFrom.Get<ResourceStoreComponent>();
            ref var storeTo = ref transfer.ResourceStoreTo.Get<ResourceStoreComponent>();

            if (transfer.NextTransferTime > Time.time) continue;
            if (storeTakeFrom.Empty) continue;
            if (storeTo.Full) continue;

            transfer.NextTransferTime = Time.time + _config.Value.ResourceTransferTime;

            transfer.ResourceStoreFrom.Get<ResourceRemoveEvent>();
            ref var addEvent = ref transfer.ResourceStoreTo.Get<ResourceAddEvent>();

            addEvent.ResourceEntity = storeTakeFrom.Resources.Peek();
            addEvent.Instant = false;
        }
    }
}