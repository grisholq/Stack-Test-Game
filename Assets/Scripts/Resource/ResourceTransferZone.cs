using UnityEngine;
using Voody.UniLeo.Lite;

public class ResourceTransferZone : MonoBehaviour
{
    [SerializeField] private TransferZoneType _zoneType;
    [SerializeField] private ConvertToEntity _pickUpStoreEntity;

    private int _entity;

    private void OnTriggerEnter(Collider other)
    {
        var entityContainer = other.GetComponentInParent<ConvertToEntity>();

        if (entityContainer == null) return;

        int? entity = entityContainer.TryGetEntity();

        if (entity.HasValue == false) return;

        if (entity.Value.Has<ResourceStoreRefComponent>() == false) return;

        _entity = entity.Value;

        ref var transfer = ref _entity.Get<ResourceTransferComponent>();

        switch (_zoneType)
        {
            case TransferZoneType.PickUp:
                transfer.ResourceStoreFrom = _pickUpStoreEntity.TryGetEntity().Value;
                transfer.ResourceStoreTo = _entity.Get<ResourceStoreRefComponent>().ResourceStore;
                break;
            case TransferZoneType.Store:
                transfer.ResourceStoreTo = _pickUpStoreEntity.TryGetEntity().Value;
                transfer.ResourceStoreFrom = _entity.Get<ResourceStoreRefComponent>().ResourceStore;
                break;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ConvertToEntity storeEntity))
        {
            var entityContainer = other.GetComponentInParent<ConvertToEntity>();

            if (entityContainer == null) return;

            int? entity = entityContainer.TryGetEntity();

            if (entity.HasValue == false) return;

            if (entity.Value.Has<ResourceStoreRefComponent>() == false) return;

            if(entity.Value == _entity)
            {
                entity.Value.Del<ResourceTransferComponent>();
            }
        }
    }
}

public enum TransferZoneType
{
    PickUp,
    Store
}