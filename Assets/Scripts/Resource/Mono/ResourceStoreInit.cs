using UnityEngine;

public class ResourceStoreInit : MonoBehaviour
{
    [SerializeField] private int _startResources;
    [SerializeField] private ResourceStore _store;

    private void Start()
    {
        for (int i = 0; i < _startResources; i++)
        {
            var instance = Resource.SpawnResource();
            instance.transform.position = transform.position;
            _store.AddResource(instance);        
        }
    }
}