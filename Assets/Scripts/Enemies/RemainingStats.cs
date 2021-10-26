namespace Enemies
{
    public struct RemainingStats
    {
        public float hp;
        public float dmg;
        public float speed;

        public RemainingStats(float hp, float dmg, float speed)
        {
            this.hp = hp;
            this.dmg = dmg;
            this.speed = speed;
        }
    }
}