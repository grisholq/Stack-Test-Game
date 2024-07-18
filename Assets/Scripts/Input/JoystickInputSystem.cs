using UnityEngine;
using Leopotam.EcsLite;

public class JoystickInputSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter _joystick;
    private EcsFilter _input;

    public void Init(EcsSystems systems)
    {
        _joystick = systems.GetWorld().Filter<JoysticRef>().End();
        _input = systems.GetWorld().Filter<JoystickInput>().End();
    }

    public void Run(EcsSystems systems)
    {
        ref var joystickRef = ref _joystick.GetFromFirst<JoysticRef>();
        ref var input = ref _input.GetFromFirst<JoystickInput>();

        input.Direction = joystickRef.Joystick.Direction.normalized;
        input.Direction3D = new Vector3(input.Direction.x, 0, input.Direction.y);
    }
}
