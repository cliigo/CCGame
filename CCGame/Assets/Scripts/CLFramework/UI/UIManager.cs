using System.Collections.Generic;
using System.Linq;
using CCFrameWork.Event;
using CLFramework.ECS;
using CLFramework.Game;
using UnityEngine;

namespace CLFramework.UI
{
    /// <summary>
    ///  将这个当作特殊的实例 ， 可以使用组件扩展共功能
    /// </summary>
    public class UIManager : Entity
    {
        public class EVENT
        {
            public static string OPEN_VIEW = "open_view";
            public static string CLOSE_VIEW = "close_view";
        }
        public EventBase event_obj = new EventBase();
        public GameObject root;
        
        
        private readonly Dictionary<string, UIBase> _ui_dict = new Dictionary<string, UIBase>();


        public void Init(GameObject root_node)
        {
            GameObject.DontDestroyOnLoad(root_node);
            this.root = root_node;
        }

        public LoadUIInfo OpenUIWithLoad<T>() where  T: UIBase
        {
            UIBase ui = GameIns.pool.Create<T>();
            LoadUIInfo ui_info = LoadUIInfo.pool.Create<LoadUIInfo>();
            ui_info.ui = ui;
            ui.OnBeforeLoad(ui_info);
            ui.Load(ui_info);
            return ui_info;
        }

        public T OpenUI<T>() where  T : UIBase
        {
            LoadUIInfo ui_load_info = OpenUIWithLoad<T>();
            AddUI(ui_load_info.name, ui_load_info.ui);
            return (T)ui_load_info.ui;
        }

        
        public void CloseUI(string ui_name)
        {
            if (_ui_dict.TryGetValue(ui_name, out UIBase ui))
            {
                if (ui.Vailed)
                {
                    ui.Close();
                }
                RemoveUI(ui_name);
            }
        }

        public void AddUI(string ui_name, UIBase ui)
        {
            _ui_dict[ui_name] = ui;
            event_obj.Emit(EVENT.OPEN_VIEW, ui_name);
        }

        public void RemoveUI(string ui_name)
        {
            if (_ui_dict.ContainsKey(ui_name))
            {
                _ui_dict.Remove(ui_name);
                event_obj.Emit(EVENT.CLOSE_VIEW, ui_name);
            }
        }


        public T GetUI<T>(string name) where  T : UIBase
        {
            if (_ui_dict.TryGetValue(name, out  UIBase ui))
            {
                if (ui.Vailed)
                {
                    return (T) ui;
                }
                else
                {
                    _ui_dict.Remove(name);
                    event_obj.Emit(EVENT.CLOSE_VIEW,name);
                }
            }
            return null;
        }
        
       
    }
}