using System;
using UnityEngine.AddressableAssets;

namespace CCFrameWork.Common.ResourcesMgr
{
    public class ResourcesMgr
    {
        public void Load<T>(string path, Action<T> load_end_aciton)
        {
            Addressables.LoadAssetAsync<T>(path).Completed += (result) =>
            {
                load_end_aciton.Invoke(result.Result);
            };
        }
    }
}