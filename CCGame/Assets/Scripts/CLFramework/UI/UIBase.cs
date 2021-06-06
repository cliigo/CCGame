using CLFramework.ECS;
using CLFramework.Game;
using CLFramework.Pool;
using UnityEngine;

namespace CLFramework.UI
{
    public class LoadUIInfo : IPoolObject
    {
        public static CLPool pool = new CLPool(typeof(LoadUIInfo));
        
        public bool isInPool { get; set; }

        public string path;
        public string name;
        public RectTransform parent;
        public int Layer;
        public UIBase ui;
        

        public void OnFetch()
        {
            
        }

        public void OnRecycle()
        {
            
        }
    }

    public class UIBase : Entity
    {

        public GameObject game_object;
        public string name;
        
        private LoadUIInfo _load_info;
        public void Load(LoadUIInfo load_info)
        {
            _load_info = load_info;
            name = load_info.name;
            GameIns.res_loader.LoadGameObject(load_info.path, OnUILoaded);
        }
        
        public void Close()
        {
            if (!Vailed) return;
            GameIns.ui_manager.RemoveUI(name);
            GameIns.res_loader.DestroyRes(game_object);
            game_object = null;
        }

        public virtual void OnBeforeLoad(LoadUIInfo load_info)
        {
            
        }

        public virtual void OnLoaded()
        {
            
        }

        public virtual void OnClose()
        {
            
        }

        private void OnUILoaded(GameObject game_object)
        {
            if (!Vailed)
            {
                GameIns.res_loader.DestroyRes(game_object);
                _load_info = null;
                return;
            }
            this.game_object = game_object;
            RectTransform transform = game_object.GetComponent<RectTransform>();
            transform.SetParent(_load_info.parent);
            OnLoaded();
            
            GameIns.pool.Recycle(_load_info);
            _load_info = null;
        }

        

    }
}