using System;
using System.Collections.Generic;
using CCFrameWork.Base.Game;

namespace CCFrameWork.Common.EventBase
{
    public class EventBase
    {
        public static List<string> remove_key_list = new List<string>(10);
        
        public Dictionary<string, ISignal> _signal_dict = new Dictionary<string, ISignal>();
        

        public void AddEvent(string event_name, Action action)
        {
            if (!_signal_dict.TryGetValue(event_name, out ISignal signal))
            {
                signal = Game.pool.Create<Signal>();
            }
            signal.On(action);
        }

        public void AddEvent<A>(string event_name, Action<A> action)
        {
            if (!_signal_dict.TryGetValue(event_name, out ISignal signal))
            {
                signal = Game.pool.Create<Signal<A>>();
            }
            ((Signal<A>)signal).On(action);
        }
        
        public void AddEvent<A,B>(string event_name, Action<A,B> action)
        {
            if (!_signal_dict.TryGetValue(event_name, out ISignal signal))
            {
                signal = Game.pool.Create<Signal<A,B>>();
            }
            ((Signal<A,B>)signal).On(action);
        }
        
        public void AddEvent<A,B,C>(string event_name, Action<A,B,C> action)
        {
            if (!_signal_dict.TryGetValue(event_name, out ISignal signal))
            {
                signal = Game.pool.Create<Signal<A>>();
            }
            ((Signal<A,B,C>)signal).On(action);
        }

        public void RemoveEvent(string event_name, Action action)
        {
            if (_signal_dict.TryGetValue(event_name, out  ISignal signal))
            {
                signal.Off(action);
                if (signal.IsEmpty())
                {
                    Game.pool.Recycle(signal);
                    _signal_dict.Remove(event_name);
                }
            }
        }
        
        public void RemoveEvent<A>(string event_name, Action<A> action)
        {
            if (_signal_dict.TryGetValue(event_name, out  ISignal signal))
            {
                ((Signal<A>)signal).Off(action);
                
                if (signal.IsEmpty())
                {
                    Game.pool.Recycle(signal);
                    _signal_dict.Remove(event_name);
                }
            }
        }
        
        public void RemoveEvent<A, B>(string event_name, Action<A,B> action)
        {
            if (_signal_dict.TryGetValue(event_name, out  ISignal signal))
            {
                ((Signal<A,B>)signal).Off(action);
                
                if (signal.IsEmpty())
                {
                    Game.pool.Recycle(signal);
                    _signal_dict.Remove(event_name);
                }
            }
        }
        
        public void RemoveEvent<A,B,C>(string event_name, Action<A,B,C> action)
        {
            if (_signal_dict.TryGetValue(event_name, out  ISignal signal))
            {
                ((Signal<A,B,C>)signal).Off(action);
                
                if (signal.IsEmpty())
                {
                    Game.pool.Recycle(signal);
                    _signal_dict.Remove(event_name);
                }
            }
        }

        public void Emit(string event_name)
        {
            if (_signal_dict.TryGetValue(event_name, out ISignal signal))
            {
                signal.Emit();
            }
        }
        
        public void Emit<A>(string event_name, A a)
        {
            if (_signal_dict.TryGetValue(event_name, out ISignal signal))
            {
                ((Signal<A>)signal).Emit(a);
            }
        }
        
        public void Emit<A,B>(string event_name, A a, B b)
        {
            if (_signal_dict.TryGetValue(event_name, out ISignal signal))
            {
                ((Signal<A, B>)signal).Emit(a,b);
            }
        }
        
        public void Emit<A, B, C>(string event_name, A a, B b, C c)
        {
            if (_signal_dict.TryGetValue(event_name, out ISignal signal))
            {
                ((Signal<A, B ,C >)signal).Emit(a, b, c);
            }
        }

        public void RemoveObj(Object obj)
        {
            // 防止gc
            int remove_count = 0;
            foreach (var pair in _signal_dict)
            {
                ISignal signal = pair.Value;       
                signal.OffByObj(obj);
                if (signal.IsEmpty())
                {
                    remove_count++;
                    remove_key_list[remove_count] = pair.Key;
                    Game.pool.Recycle(signal);
                }
            }

            for (int idx = 0; idx < remove_count; idx++)
            {
                _signal_dict.Remove(remove_key_list[idx]);
            }
        }

        public void Clear()
        {
            foreach(var pair in _signal_dict) {
                Game.pool.Recycle(pair.Value);                
            }
            _signal_dict.Clear();
        }

    }
}