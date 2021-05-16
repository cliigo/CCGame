using System;
using System.Collections.Generic;
using CCFrameWork.Base.Game;
using CCFrameWork.Common.ECS;
using Unity.VisualScripting;
using UnityEngine;

namespace CCFrameWork.Common.UI
{
    public class ViewManager : Entity
    {
        public  static class EVENT_VIEW
        {
            public static string  OPEN = "open_view";
            public static string  CLOSE = "close_view";
        }

        // 分辨率
        public Vector2 resolution = new Vector2(1920,1080);
        
        public Dictionary<E_VIEW_LAYER, RectTransform> _view_layer_root_dict = new Dictionary<E_VIEW_LAYER, RectTransform>();
        public GameObject _root_node;
        public EventBase.EventBase event_obj = new EventBase.EventBase();
        public Dictionary<string, View> _view_dict = new Dictionary<string, View>();

        public  void Init(GameObject root_node)
        {
            _root_node = root_node;
            _init_view_layer();
        }


        public void AddView(string view_name, View view)
        {
            _view_dict[view_name] = view;
            event_obj.Emit<View>(EVENT_VIEW.OPEN, view);
        }

        public void RemoveView(string view_name)
        {
            if (_view_dict.TryGetValue(view_name, out View view))
            {
                _view_dict.Remove(view_name);
                event_obj.Emit(EVENT_VIEW.CLOSE);
            }
        }

        public RectTransform GetLayer(E_VIEW_LAYER view_layer)
        {
            return _view_layer_root_dict[view_layer];
        }

        public void _init_view_layer()
        {
            Array layer_array = Enum.GetValues(typeof(E_VIEW_LAYER));
            foreach (var layer in layer_array)
            {
                GameObject new_layer_object = new GameObject(layer.ToString());
                RectTransform rect_transform = new_layer_object.AddComponent<RectTransform>();
                rect_transform.sizeDelta = resolution;
                rect_transform.SetParent( _root_node.GetComponent<RectTransform>());
                rect_transform.localPosition = Vector3.zero;
                rect_transform.SetSiblingIndex((int) layer);
                _view_layer_root_dict[(E_VIEW_LAYER)layer] = rect_transform;

            }
        }


    }
}