using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCFrameWork.Common.Pool;
using CCFrameWork.Common.ResourcesMgr;
using CCFrameWork.Common.Scene;
using CCFrameWork.Common.UI;
using UnityEngine;

namespace CCFrameWork.Base.Game
{
    public static class Game
    {
        
        public  static  PoolMgr pool = new PoolMgr();

        public static Scene scene;
        public static ViewManager viewMgr;
        
        
        public static  ResourcesMgr resources = new ResourcesMgr();
        
        
        public static void Init(GameObject root)
        {
            viewMgr = new ViewManager();
            viewMgr.Init(root);
            scene = new Scene().InitSystem();
            
        }

        public static void Exit()
        {
        }


        public static void Update() 
        {
            if (scene != null)
            {
                scene.Update();
            }
        }

        public static void LateUpdate() 
        {
            if (scene != null)
            {
                scene.LateUpdate();
            }
        }
    }
}
