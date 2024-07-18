using UnityEngine;
using Leopotam.EcsLite;

public class StickmanInputSystem: IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter _stickman;
    private EcsFilter _stickmanInput;
    private EcsFilter _joystickInput;

    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();

        world.NewEntity().Get<StickmanInput>();

        _stickman = world.Filter<StickmanTag>().Inc<StickmanData>().End();
        _stickmanInput = world.Filter<StickmanInput>().End();
        _joystickInput = world.Filter<JoystickInput>().End();
    }

    public void Run(EcsSystems systems)
    {
        ref var joystick =ref _joystickInput.GetFromFirst<JoystickInput>();
        ref var input = ref _stickmanInput.GetFromFirst<StickmanInput>();
        ref var stickmanData = ref _stickman.GetFromFirst<StickmanData>();

        input.Movement = joystick.Direction3D * stickmanData.Speed;
    }
}
