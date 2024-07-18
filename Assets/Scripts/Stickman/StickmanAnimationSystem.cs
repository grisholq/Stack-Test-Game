using UnityEngine;
using Leopotam.EcsLite;

public class StickmanAnimationSystem: IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter _stickman;
    private EcsFilter _stickmanInput;

    public void Init(EcsSystems systems)
    {
        var world = systems.GetWorld();

        _stickman = world.Filter<StickmanTag>().Inc<StickmanData>().Inc<StickmanAnimator>().End();
        _stickmanInput = world.Filter<StickmanInput>().End();
    }

    public void Run(EcsSystems systems)
    {
        var input = _stickmanInput.GetFromFirst<StickmanInput>();
        var stickmanData = _stickman.GetFromFirst<StickmanData>();
        var animator = _stickman.GetFromFirst<StickmanAnimator>().Animator;

        animator.SetBool("Run", input.HasMovement);
    }
}
