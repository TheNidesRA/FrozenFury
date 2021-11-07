namespace Enemies
{
    public struct EnemyStats
    {
        public string id;
        public float hp;
        public float dmg;
        public float speed;
        public float armor;
        public float atackSpd;
        public float gold;

        public EnemyStats(string id, float hp, float dmg, float speed,float armor, float atackSpd, float gold)
        {
            this.id = id;
            this.hp = hp;
            this.dmg = dmg;
            this.speed = speed;
            this.armor = armor;
            this.atackSpd = atackSpd;
            this.gold = gold;
        }
    }
}