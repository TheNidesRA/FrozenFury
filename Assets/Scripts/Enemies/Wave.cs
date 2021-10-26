using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(menuName = "Custom/Wave")]
    public class Wave : ScriptableObject
    {
        public List<Enemy> Enemies;
    }
}