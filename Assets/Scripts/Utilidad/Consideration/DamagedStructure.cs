using System.Collections.Generic;
using System.Linq;
using GridSystem;

namespace UtilityBehaviour.Considerations
{
    public class DamagedStructure:Consideration
    {
        private int damagedStructures;
        
        
        
        public override float ScoreConsideration(NPCController npc)
        {
            return SearchDamageStruct(GridBuildingSystem.Instance.BuildStruct.Values.ToList());
           
        }

        private float SearchDamageStruct(List<PlacedBuild> structs)
        {
           // List<PlacedBuild> damagedStructs= new List<PlacedBuild>();

            foreach (var sstruct in structs)
            {
                if (sstruct.isDamaged)
                {
                    return 1f;
                }
            }

            return 0f;

        }
    }
}