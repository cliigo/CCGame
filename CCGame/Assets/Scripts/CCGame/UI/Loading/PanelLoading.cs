using CCFrameWork.Base.Game;
using CCFrameWork.Common.Pipeline;
using CCFrameWork.Common.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace CCGame.UI.Loading
{
    public class PanelLoading : View
    {
        public override void InitViewConfig(ref ViewConfig view_config)
        {
            view_config.name = "PanelLoading";
            view_config.path = "Assets/Assets/UI/Preafb/Loading/PanelLoading.prefab";
            view_config.view_layer = E_VIEW_LAYER.TOP;
        }

        protected override void Start()
        {
            GetUIComponent<Progress>("progressBG").InitProgress();
            
        }

        public void OnProgress(float progress)
        {
            Progress progres_comp = GetUIComponent<Progress>("progressBG");
            if (progres_comp)
            {
                progres_comp.MoveToProgress(progress);    
            }
        }
        
    }
}