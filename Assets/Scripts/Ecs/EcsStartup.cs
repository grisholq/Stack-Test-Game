using UnityEngine;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;
using Leopotam.EcsLite.Di;
using DG.Tweening;

class EcsStartup : MonoBehaviour
{
    [SerializeField] private GameConfig _config;

    private EcsWorld _world;
    private EcsSystems _systems;

    private void Start()
    {
        Application.targetFrameRate = 60;

        _world = new EcsWorld();
        _systems = new EcsSystems(_world, _config);
        _systems
            .Add(new JoystickInputSystem())
            .Add(new StickmanInputSystem())
            .Add(new StickmanMovementSystem())
            .Add(new StickmanAnimationSystem())
            .Add(new ResourceProductionSystem())
            .Add(new ResourceTransferSystem())  
            .Add(new ResourceManageSystem())
            .Add(new ResourceViewSystem())
            .AddCustomOneFrames()
            .Inject(_config)
            .ConvertScene()
            .Init();
    }

    private void Update()
    {
        _systems?.Run();
    }
}