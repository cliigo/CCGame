using System;
using System.Collections.Generic;
using UnityEngine;

namespace CCFrameWork.Common.UI
{
    
    public class ViewBind : MonoBehaviour
    {
        [SerializeField] public List<GameObject> ui_list;
        
        public Dictionary<string,GameObject> _game_object_dict = new Dictionary<string, GameObject>();
       [NonSerialized] public int _ui_idex = 0;
        
        public GameObject GetUIObject(string ui_name)
        {
            if (!_game_object_dict.TryGetValue(ui_name, out  GameObject obj))
            {
                List<GameObject> ui_list = GetComponent<ViewBind>().ui_list;
                
                for (; _ui_idex < ui_list.Count ;_ui_idex ++ )
                {
                    GameObject ui_obj = ui_list[_ui_idex];
                    SetUIObject(ui_obj.name, ui_obj);
                    if (ui_obj.name == ui_name)
                    {
                        return ui_obj;
                    }
                }
                // 如果这里没有找到的话就只能便利找了 异常情况不考虑gc了
                
                Queue<Transform> temp_list = new Queue<Transform>(){};
                temp_list.Enqueue(transform);     
                while (temp_list.Count > 0)
                {
                    var temp_obj = temp_list.Dequeue();
                    if (temp_obj.name == ui_name)
                    {
                        // 只保存需要的
                        SetUIObject(temp_obj.name, temp_obj.gameObject);    
                    }

                    int child_count = temp_obj.childCount;
                    for (int i = 0; i < child_count; i++) 
                    {
                        temp_list.Enqueue(temp_obj.GetChild(i));   
                    }
                }
                //  更不可能走到这里了
                return null;
            }

            return obj;
        }

        public void SetUIObject(string ui_name, GameObject obj)
        {
            _game_object_dict[ui_name] = obj;
        }

    }
}