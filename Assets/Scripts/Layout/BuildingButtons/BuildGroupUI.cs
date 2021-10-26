using System.Collections.Generic;
using UnityEngine;

namespace BigfootSdk.SafeArea.BuildingButtons
{
    public class BuildGroupUI : MonoBehaviour
    {
        
        public List<BuildElementUI> tabButtons = new List<BuildElementUI>();
      

        //In case I need to sort the lists by GetSiblingIndex
        //objListOrder.Sort((x, y) => x.OrderDate.CompareTo(y.OrderDate));

        public Color tabIdleColor;
        public Color tabHoverColor;
        public Color tabSelectedColor;
        private BuildElementUI selectedTab;
        
        
        
        
        public void OnPointerEnter(BuildElementUI buttonBuild)
        {
        }

        public void OnPointerClick(BuildElementUI buttonBuild)
        {
       
        }

        public void OnPointerExit(BuildElementUI buttonBuild)
        {
       
        }
        
        
        
    }
}