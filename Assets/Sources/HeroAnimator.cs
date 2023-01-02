using Spine;
using Spine.Unity;
using UnityEngine;

public enum HeroState
{
    Idle,
    Walk,
    Slide
}

public class HeroAnimator : MonoBehaviour
{
    [SerializeField, SpineAnimation] private string _idleAnimation;
    [SerializeField, SpineAnimation] private string _walkAnimation;
    [SerializeField, SpineAnimation] private string _slideAnimation;

    [Space]

    [SerializeField] private SkeletonAnimation _skeletonAnimation;
    [SerializeField] private HeroBehaviour _heroBehaviour;

    private Spine.AnimationState _animationState;

    private void Awake()
    {
        _animationState = _skeletonAnimation.AnimationState;
    }

    private void Start()
    {
        print(_animationState.GetCurrent(0).Animation.Name);
    }

    private void Update()
    {
        TryFlip();

        if (_heroBehaviour.Controllable == false && _animationState.GetCurrent(0).Animation.Name != _slideAnimation)
        {
            _animationState.SetAnimation(0, _slideAnimation, true);
        }
        else if (_heroBehaviour.Controllable && _animationState.GetCurrent(0).Animation.Name != _idleAnimation && _heroBehaviour.Walking == false)
        {
            _animationState.SetAnimation(0, _idleAnimation, true);
        }
        else if (_heroBehaviour.Walking && _animationState.GetCurrent(0).Animation.Name != _walkAnimation && _heroBehaviour.Controllable)
        {
            _animationState.SetAnimation(0, _walkAnimation, true);
        }
        else if (_heroBehaviour.Walking == false && _animationState.GetCurrent(0).Animation.Name != _idleAnimation && _heroBehaviour.Controllable)
        {
            _animationState.SetAnimation(0, _idleAnimation, true);
        }
    }

    private void TryFlip()
    {
        if (_heroBehaviour.InputAxis == 0) return;

        _skeletonAnimation.skeleton.ScaleX = _heroBehaviour.InputAxis;
    }
}
