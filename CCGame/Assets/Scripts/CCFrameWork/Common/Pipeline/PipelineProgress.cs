using System;
using System.Collections.Generic;
using CCFrameWork.Base.Game;
using CCFrameWork.Common.ECS;

namespace CCFrameWork.Common.Pipeline
{
    public interface IPipelineProgress
    {
        public bool is_done { get; set; }
        
        public void Start();
        
        public void Excute();
    }

    public class PipelineProgressMgr : ITaskUpdate
    {
        public Queue<IPipelineProgress> _pipelines = new Queue<IPipelineProgress>();

        public Action _end_action;
        public Action<float> _progress_acionn;
        
        public IPipelineProgress _cur_pipeline;
        public float _complete_cnt = 0;
        public float _all_cnt = 0;

        public PipelineProgressMgr Add(IPipelineProgress pipeline)
        {
            _pipelines.Enqueue(pipeline);
            return this;
        }

        public void Start(Action end_action = null, Action<float> progress_aciton = null)
        {
            _end_action = end_action;
            _progress_acionn = progress_aciton;
            Game.scene.RegisterTask(this);
            _complete_cnt = -1;
            _all_cnt = _pipelines.Count;
        }

        public void Update(float dt)
        {
            if (_cur_pipeline == null || _cur_pipeline.is_done)
            {
                if (_progress_acionn != null)
                {
                    _progress_acionn.Invoke((++_complete_cnt) / _all_cnt);
                }

                if (_pipelines.Count > 0)
                {
                    _cur_pipeline = _pipelines.Dequeue();
                    _cur_pipeline.Start();
                }
                else
                {
                    Game.scene.DisRegisterTask(this);
                }

            }
            else
            {
                _cur_pipeline.Excute();
            }
        }
    }
}