namespace game_darksouls.Animation
{
    public interface IDeathAnimation
    {
        bool DeathAnimationComplete { get; }
        void PlayDeathAnimation();
    }
}
