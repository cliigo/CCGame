using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEditor.Search;

namespace CLFramework.Loader
{
    public interface ILoader : IDestroy
    {
        public bool isEnd { get; set; }

        public void Init();

        public void Update();
        
        
    }

    /// <summary>
    ///  加载管理器
    /// </summary>
    public class LoaderMgr : CLObject
    {
        private readonly List<ILoader> _loader_list = new List<ILoader>();
        private bool _is_start = false;

        public Action On_All_Loaded;
        public void AddLoader(ILoader loader)
        {
            _loader_list.Add(loader);
        }

        public void Start()
        {
            _is_start = true;

            foreach (var loader in _loader_list)
            {
                loader.Init();
            }
        }

        public void Update()
        {
            if (!_is_start) return;
            int count = _loader_list.Count;
            int index = 0;
            while (count > 0)
            {
                ILoader loader = _loader_list[index];
                if (loader.isEnd)
                {
                    loader.Destroy();
                    _loader_list.RemoveAt(index);
                }
                else
                {
                    loader.Update();
                    index++;
                }
                count--;
            }

            if (_loader_list.Count == 0)
            {
                On_All_Loaded.Invoke();
                _is_start = false;
            }
        }
    }
}