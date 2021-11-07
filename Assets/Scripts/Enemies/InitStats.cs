using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Custom/InitStats")]
    public class InitStats : ScriptableObject
    {
        public float initHp = 1;
        public float initDmg = 1;
        public float initSpd = 1;
        public float initArm = 1;
        public float initAtkSpd = 1;
        public float gold = 1;
    }
}