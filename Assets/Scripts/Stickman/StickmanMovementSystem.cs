using UnityEngine;
using Leopotam.EcsLite;

public class StickmanMovementSystem: IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter _stickman;
    private EcsFilter _stickmanInput;

    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();

        _stickman = world.Filter<StickmanTag>().Inc<StickmanData>().Inc<RigidbodyComponent>().Inc<TransformComponent>().End();
        _stickmanInput = world.Filter<StickmanInput>().End();
    }

    public void Run(EcsSystems systems)
    {
        var input = _stickmanInput.GetFromFirst<StickmanInput>();
        var stickmanData = _stickman.GetFromFirst<StickmanData>();
        var rigidbody = _stickman.GetFromFirst<RigidbodyComponent>().Rigidbody;
        var stickmanView = stickmanData.View;

        if (input.NoMovement)
        {
            rigidbody.velocity = Vector3.zero;
            return;
        }  

        rigidbody.velocity = input.Movement;

        var inputNorm = input.Movement2D.normalized;

        float angle = -1 * Mathf.Atan2(inputNorm.y, inputNorm.x);
        angle *= Mathf.Rad2Deg;

        var eulers = stickmanView.eulerAngles;
        eulers.y = angle + stickmanData.RotationOffset;
        stickmanView.eulerAngles = eulers;
    }
}
