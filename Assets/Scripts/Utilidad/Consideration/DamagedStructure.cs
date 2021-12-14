using System.Collections.Generic;
using System.Linq;
using GridSystem;
using UnityEngine;

namespace UtilityBehaviour.Considerations
{
    [CreateAssetMenu(fileName = "DamagedStructure", menuName = "Consideration/DamagedStructure")]
    public class DamagedStructure : Consideration
    {
        private int damagedStructures;


        public override float ScoreConsideration(NPCController npc)
        {
//            Debug.Log("cALCULADO DAMAG2DASUBDAWUI");
            Score = SearchDamageStruct(GridBuildingSystem.Instance.BuildStruct.Values.ToList());
            return Score;
        }

        private float SearchDamageStruct(List<PlacedBuild> structs)
        {
            // List<PlacedBuild> damagedStructs= new List<PlacedBuild>();

            foreach (var sstruct in structs)
            {
                if (sstruct.isDamaged)
                {
                    Debug.Log("Devuelvo 1");
                    return 1f;
                }
            }

            Debug.Log("Devuelvo 0");
            return 0f;
        }
    }
}