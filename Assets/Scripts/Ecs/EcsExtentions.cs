using UnityEngine;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

public static class EcsExtentions
{
    public static int First(this EcsFilter filter)
    {
        return filter.GetRawEntities()[0];
    }

    public static ref T GetFromFirst<T>(this EcsFilter filter) where T : struct
    {
        return ref filter.GetRawEntities()[0].Get<T>();
    }

    public static ref T Get<T>(this int entity, EcsWorld world = null) where T : struct
    {
        if(world == null)
        {
            world = WorldHandler.GetMainWorld();
        }

        if (world.GetPool<T>().Has(entity)) return ref world.GetPool<T>().Get(entity);

        return ref world.GetPool<T>().Add(entity);
    }

    public static bool Has<T>(this int entity, EcsWorld world = null) where T : struct
    {
        if (world == null)
        {
            world = WorldHandler.GetMainWorld();
        }

        return world.GetPool<T>().Has(entity);
    }

    public static void Del<T>(this int entity, EcsWorld world = null) where T : struct
    {
        if (world == null)
        {
            world = WorldHandler.GetMainWorld();
        }

        if (world.GetPool<T>().Has(entity))
        {
            world.GetPool<T>().Del(entity);
        }
    }

    public static int GetEntity(this GameObject gameObject)
    {
        return gameObject.GetComponentInChildren<ConvertToEntity>().ConvertInstantly();
    }

    public static int GetEntity(this Transform transform)
    {
        return transform.gameObject.GetComponentInChildren<ConvertToEntity>().ConvertInstantly();
    }

    public static int InstantiateEntity(this GameObject gameObject)
    {
        return Object.Instantiate(gameObject).GetEntity();
    }

    public static EcsSystems AddCustomOneFrames(this EcsSystems systems)
    {
        systems.Add(new OneFrameEventsSystem<ResourceRemoveEvent>());
        systems.Add(new OneFrameEventsSystem<ResourceAddEvent>());
        systems.Add(new OneFrameEventsSystem<ResourceAddMultipleEvent>());

        return systems;
    }
}