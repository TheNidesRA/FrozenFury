using UnityEngine;

namespace UtilityBehaviour
{
    public abstract class Consideration : ScriptableObject
    {
        public string Name;
        private float _score;

        public float Score
        {
            get { return _score; }
            set { _score = Mathf.Clamp01(value); }
        }


        public Consideration[] considerations;

        private void Awake()
        {
            Score = 0;
        }
        
        public abstract float ScoreConsideration(NPCController npc);
    }
}