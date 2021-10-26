using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Custom/InitStats")]
    public class InitStats : ScriptableObject
    {
        [SerializeField] public float InitHp = 1;
        [SerializeField] public float InitDmg = 1;
        [SerializeField] public float InitSpd = 1;
        [SerializeField] public float InitArm = 1;
        [SerializeField] public float InitAtkSpd = 1;
    }
}