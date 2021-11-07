namespace Enemies
{
    public struct WinnerStats
    {
        public string id;
        public float hp;

        public WinnerStats(string id, float hp)
        {
            this.id = id;
            this.hp = hp;
        }
    }
}