using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BuildStats
{
    public BuildStats(int lvl, BuildingSO.BuildingType type)
    {
        this.type = type;
        this.lvl = lvl;
    }

    public BuildStats(int lvl)
    {
        this.lvl = lvl;
        this.type = BuildingSO.BuildingType.Trap;
    }
    
    public BuildingSO.BuildingType type;
    public int lvl;

}
