using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public class OneFrameEventsSystem<T>: IEcsRunSystem  where T : struct
{
    private EcsFilterInject<Inc<T>> _oneFrame;

    public void Run(EcsSystems systems)
    {
        foreach (var entity in _oneFrame.Value)
        {
            entity.Del<T>();
        }
    }
}
