using System.Collections.Generic;
using CLFramework.ECS;
using CLFramework.Pool;
using CLFramework.Resources;
using CLFramework.UI;
using Unity.VisualScripting;
using UnityEngine;

namespace CLFramework.Game
{
    public class GameIns
    {
        public  static CLPoolMgr pool = new CLPoolMgr();
        
        public static UIManager ui_manager = new UIManager();
        public static ResourceLoad res_loader = new ResourceLoad();
        
        
        public static List<World> world_list = new List<World>();
        
        

        public static void Init(GameObject ui_root)
        {
            
        }

        public static void AddWold(string name,World world)
        {
            world.name = name;
            world_list.Add(world);
        }

        public static World GetWorld(string name)
        {
            foreach (var world in world_list)
            {
                if (world.name == name)
                {
                    return world;
                }
            }

            return null;
        }

        public static void RemoveWorld(string name)
        {
            for (int idx = 0; idx < world_list.Count; idx++) 
            {
                if (world_list[idx].name == name)
                {
                    world_list.RemoveAt(idx);
                    return;
                }
            }
        }

        public static void Update()
        {

            foreach (var one_world in world_list)
            {
                one_world.Update();
            }
        }
    }
}