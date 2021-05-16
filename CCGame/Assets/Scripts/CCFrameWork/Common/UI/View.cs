using System;
using System.Collections.Generic;
using CCFrameWork.Base.Game;
using CCFrameWork.Common.ECS;
using CCFrameWork.Common.Pipeline;
using Unity.VisualScripting;
using UnityEngine;

namespace CCFrameWork.Common.UI
{

    

    public struct ViewConfig
    {
        public string name; //界面名字
        public string path;// 界面路径

        public RectTransform parent; // 父节点
        public E_VIEW_LAYER view_layer; // 界面层级

        public View view; // 界面对象
        
    }

    public class LoadViewPipieline : IPipeLine<ViewConfig>
    {
        
        public bool is_done { get; set; }
        public ViewConfig data { get; set; }

        public void Start()
        {
            Game.resources.Load<GameObject>(data.path, OnResourceLoaded);
        }

        public void Update()
        {
            
        }

        public void OnResourceLoaded(GameObject prefab)
        {
            GameObject gameObject = GameObject.Instantiate<GameObject>(prefab);
            
            // 先进行隐藏方便后续操作
            gameObject.SetActive(false);
            
            // 绑定界面操作
            data.view.view_bind = gameObject.GetComponent<ViewBind>();
            data.view.game_object = gameObject;
            // 检测是否有父节点
            RectTransform parent = data.parent;
            RectTransform rect_transform = gameObject.GetComponent<RectTransform>();
            if (!rect_transform)
            {
                rect_transform = gameObject.AddComponent<RectTransform>();
            }
            

            if (parent == null)
            {
                rect_transform.SetParent(Game.viewMgr.GetLayer(data.view_layer));
            }
            else
            {
                rect_transform.SetParent(parent);
                rect_transform.SetSiblingIndex((int)data.view_layer);
            }
            rect_transform.localPosition = Vector3.zero;
            is_done = true;
        }
        
    }

    /// <summary>
    ///  动画流程
    /// </summary>
    public class ViewAniPipeline : IPipeLine<ViewConfig>
    {
        public bool is_done { get; set; }
        public ViewConfig data { get; set; }

        public void Start()
        {
            
        }

        public void Update()
        {
            
        }
    
    }

    public abstract class View : Entity
    {
        
        public static T Create<T>() where  T : View,new()
        {
            T create_view = new T();
            create_view.LoadView();
            return create_view;
        }

        public string name;
        
        public GameObject game_object = null;
        public ViewBind view_bind = null;
        
        public PipelineMgr<ViewConfig> _load_pipeline;

        public void LoadView()
        {
            
            _load_pipeline = Game.pool.Create<PipelineMgr<ViewConfig>>();
            // 开始添加
            _load_pipeline
                .Add(new LoadViewPipieline());
            
            ViewConfig view_config = new ViewConfig();
            view_config.view = this;
            InitViewConfig(ref view_config);
            name = view_config.name;
            _load_pipeline.Start(ref view_config, LoadViewEnd);
        }

        public abstract void InitViewConfig(ref  ViewConfig view_config);

        public void LoadViewEnd()
        {
            game_object.SetActive(true);
            Game.pool.Recycle(_load_pipeline);
            _load_pipeline = null;
            Game.viewMgr.AddView(name, this);
            Start();
        }


        protected virtual void Start()
        {
            
        }


        public GameObject GetUIObject(string ui_name)
        {
            if (view_bind)
            {
                return view_bind.GetUIObject(ui_name);
            }

            return null;
        }

        public T GetUIComponent<T>(string ui_name)
        {
            GameObject ui_object = GetUIObject(ui_name);
            if (ui_object != null)
            {
                return ui_object.GetComponent<T>();
            }

            return default(T);
        }


        public override void OnFetch()
        {
            base.OnFetch();
            game_object = null;
            view_bind = null;
        }
        
       
    }
}