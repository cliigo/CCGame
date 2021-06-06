using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CLFramework.Resources
{
    public class ResourceLoad
    {
        public void LoadGameObject(string path, Action<GameObject> load_call_back = null)
        {
            Addressables.LoadAssetAsync<GameObject>(path).Completed += (result) =>
            {
                if (result.IsDone)
                {
                    if (load_call_back != null)
                    {
                        Addressables.InstantiateAsync(result.Result).Completed += (ins_result)=>
                        {
                            load_call_back.Invoke(ins_result.Result);    
                        };
                        
                    }
                }
            };
        }

        public void DestroyRes(GameObject game_object)
        {
            Addressables.ReleaseInstance(game_object);
        }
    }
}