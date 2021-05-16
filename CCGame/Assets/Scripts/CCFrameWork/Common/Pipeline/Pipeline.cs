using System;
using System.Collections.Generic;
using CCFrameWork.Base.Game;
using CCFrameWork.Common.ECS;
using CCFrameWork.Common.Pool;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine.PlayerLoop;

/**
 *  业务处理流水线
 */
    
namespace CCFrameWork.Common.Pipeline
{
    public interface IPipeLine<DoData>
    {
        
        public bool is_done { get; set; }
        public DoData data { get; set; }
        public void Start();

        public void Update();
        
    }
    

    public class PipelineMgr<DoData> : IUpdateTask, IPoolObject
    {
        
        public bool is_in_pool { get; set; }
        
        public DoData _do_data;

        

        // 进度回调
        public Action<float> pipeline_progress_action = null;

        

        public Queue<IPipeLine<DoData>> _pipelies = new Queue<IPipeLine<DoData>>();
        public IPipeLine<DoData> _cur_pipeline;
        public float _all_cnt = 0;
        public float _complete_cnt = 0;
        public Action _end_action;

        public PipelineMgr<DoData> Add(IPipeLine<DoData> pipeline)
        {
            _pipelies.Enqueue(pipeline);
            return this;
        }

        public void Start(ref DoData do_data, Action end_action = null)
        {
            _do_data = do_data;
            Game.scene.RegisterTask(this);
            _complete_cnt = -1;
            _all_cnt = _pipelies.Count;
            _end_action = end_action;
        }

        public void Update(float dt)
        {
            if (_cur_pipeline == null ||  _cur_pipeline.is_done)
            {
                if (_pipelies.Count > 0)
                {
                    if (pipeline_progress_action != null)
                    {
                        _complete_cnt++;
                        pipeline_progress_action.Invoke(_complete_cnt/_all_cnt);
                    }

                    _cur_pipeline = _pipelies.Dequeue();
                    _cur_pipeline.data = _do_data;
                    _cur_pipeline.Start();;
                }
                else
                {
                    Game.scene.DisRegisterTask(this);
                    if(_end_action !=  null) _end_action.Invoke();
                }
            }
            else
            {
                _cur_pipeline.Update();
            }
        }

        public void OnFetch()
        {
            
        }

        public void OnRecycle()
        {
            
            _cur_pipeline = null;
            _do_data = default(DoData);
            _pipelies.Clear();

        }
    }


   
}