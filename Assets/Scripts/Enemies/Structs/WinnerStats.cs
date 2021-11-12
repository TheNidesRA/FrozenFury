namespace Enemies
{
    public struct WinnerStats
    {
        public string id;
        public float hp;
        public float baseDmg;

        public WinnerStats(string id, float hp, float baseDmg)
        {
            this.id = id;
            this.hp = hp;
            this.baseDmg = baseDmg;
        }
    }
}