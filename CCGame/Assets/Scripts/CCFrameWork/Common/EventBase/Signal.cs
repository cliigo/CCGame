using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using CCFrameWork.Common.Pool;



namespace CCFrameWork.Common.EventBase
{
    public interface ISignal : IPoolObject
    {
        public void On(Action a);
        public void Off(Action a);
        public void Emit();

        public void OffByObj(Object obj);

        public bool IsEmpty();
    }
    
    public interface ISignal<A> : ISignal
    {
        public void On(Action<A> a);
        public void Off(Action<A> a);
        public void Emit(A a);
    }
    
    public interface ISignal<A,B> : ISignal
    {
        public void On(Action<A,B> a);
        public void Off(Action<A, B> a);
        public void Emit(A a, B b);
    }
    public interface ISignal<A,B, C> : ISignal
    {
        public void On(Action<A,B, C> a);
        public void Off(Action<A, B, C> a);
        public void Emit(A a, B b, C c);
    }

   

    /// <summary>
    ///  信号对象
    /// </summary>
    public class Signal : ISignal
    {
        public bool is_in_pool { get; set; }

        public List<Action> _action_list = new List<Action>(10);

        public virtual void On(Action action)
        {
            if (IsIn(action))
            {
                return;;
            }
            _action_list.Add(action);
        }

        public virtual void Off(Action action)
        {
            int index = GetIndex(action);
            if (index != -1)
            {
                _action_list.RemoveAt(index);
            }
        }

        public virtual bool IsEmpty()
        {
            return _action_list.Count == 0;
        }

        public virtual void Emit()
        {
            foreach (var one_aciton in _action_list)
            {
                one_aciton.Invoke();
            }
        }

        public virtual void Clear()
        {
            _action_list.Clear();
        }

        public virtual void OffByObj(Object obj)
        {
            int count = _action_list.Count;
            int index = 0;
            Action one_action;
            while (count > 0)
            {
                one_action = _action_list[index];
                if (one_action.Target == obj)
                {
                    _action_list.RemoveAt(index);;
                }
                else
                {
                    index++;
                }

                count--;
            }
        }

        public virtual bool IsIn(Action action)
        {
            return GetIndex(action) != -1;
        }

        public virtual int GetIndex(Action action)
        {
            for (int i = 0; i < _action_list.Count; i++)
            {
                Action one_action = _action_list[i];
                if (one_action.Target == action.Target && one_action.Method == action.Method)
                {
                    return i;
                }
            }

            return -1;
        }

        public void OnFetch()
        {
            
        }

        public void OnRecycle()
        {
            Clear();
        }
    }
    
    public class Signal<A> : ISignal<A>
    {
        public bool is_in_pool { get; set; }

        public List<Action<A>> _action_list = new List<Action<A>>(10);

        public virtual void On(Action<A> action)
        {
            if (IsIn(action))
            {
                return;;
            }
            _action_list.Add(action);

        }

        public virtual void Off(Action<A> action)
        {
            int index = GetIndex(action);
            if (index != -1)
            {
                _action_list.RemoveAt(index);
            }
        }

        public virtual void Emit(A a)
        {
            foreach (var one_aciton in _action_list)
            {
                one_aciton.Invoke(a);
            }
        }

        public virtual void Clear()
        {
            _action_list.Clear();
        }
        
        public virtual bool IsEmpty()
        {
            return _action_list.Count == 0;
        }

        public virtual void OffByObj(Object obj)
        {
            int count = _action_list.Count;
            int index = 0;
            Action<A> one_action;
            while (count > 0)
            {
                one_action = _action_list[index];
                if (one_action.Target == obj)
                {
                    _action_list.RemoveAt(index);;
                }
                else
                {
                    index++;
                }

                count--;
            }
        }

        public virtual bool IsIn(Action<A> action)
        {
            return GetIndex(action) != -1;
        }

        public virtual int GetIndex(Action<A> action)
        {
            for (int i = 0; i < _action_list.Count; i++)
            {
                Action<A> one_action = _action_list[i];
                if (one_action.Target == action.Target && one_action.Method == action.Method)
                {
                    return i;
                }
            }

            return -1;
        }

        public void OnFetch()
        {
            
        }

        public void OnRecycle()
        {
            Clear();
        }

        public void On(Action a)
        {
            throw new NotImplementedException();
        }

        public void Off(Action a)
        {
            throw new NotImplementedException();
        }

        public void Emit()
        {
            throw new NotImplementedException();
        }
    }
    
    
     public class Signal<A, B> : ISignal<A, B>
    {
        public bool is_in_pool { get; set; }

        public List<Action<A,B>> _action_list = new List<Action<A,B>>(10);

        public virtual void On(Action<A,B> action)
        {
            if (IsIn(action))
            {
                return;;
            }
            _action_list.Add(action);

        }

        public virtual void Off(Action<A,B> action)
        {
            int index = GetIndex(action);
            if (index != -1)
            {
                _action_list.RemoveAt(index);
            }
        }

        public virtual void Emit(A a, B b)
        {
            foreach (var one_aciton in _action_list)
            {
                one_aciton.Invoke(a,b);
            }
        }

        public virtual void Clear()
        {
            _action_list.Clear();
        }

        public virtual bool IsEmpty()
        {
            return _action_list.Count == 0;
        }
        public virtual void OffByObj(Object obj)
        {
            int count = _action_list.Count;
            int index = 0;
            Action<A,B> one_action;
            while (count > 0)
            {
                one_action = _action_list[index];
                if (one_action.Target == obj)
                {
                    _action_list.RemoveAt(index);;
                }
                else
                {
                    index++;
                }

                count--;
            }
        }

        public virtual bool IsIn(Action<A,B> action)
        {
            return GetIndex(action) != -1;
        }

        public virtual int GetIndex(Action<A,B> action)
        {
            for (int i = 0; i < _action_list.Count; i++)
            {
                Action<A,B> one_action = _action_list[i];
                if (one_action.Target == action.Target && one_action.Method == action.Method)
                {
                    return i;
                }
            }

            return -1;
        }

        public void OnFetch()
        {
            
        }

        public void OnRecycle()
        {
            Clear();
        }

        public void On(Action a)
        {
            throw new NotImplementedException();
        }

        public void Off(Action a)
        {
            throw new NotImplementedException();
        }

        public void Emit()
        {
            throw new NotImplementedException();
        }
    }
     
      public class Signal<A, B, C> : ISignal<A, B, C>
    {
        public bool is_in_pool { get; set; }

        public List<Action<A,B,C>> _action_list = new List<Action<A,B,C>>(10);

        public virtual void On(Action<A,B,C> action)
        {
            if (IsIn(action))
            {
                return;;
            }
            _action_list.Add(action);

        }

        public virtual void Off(Action<A,B,C> action)
        {
            int index = GetIndex(action);
            if (index != -1)
            {
                _action_list.RemoveAt(index);
            }
        }

        public virtual void Emit(A a, B b, C c)
        {
            foreach (var one_aciton in _action_list)
            {
                one_aciton.Invoke(a,b, c);
            }
        }

        public virtual void Clear()
        {
            _action_list.Clear();
        }
        
        public virtual bool IsEmpty()
        {
            return _action_list.Count == 0;
        }

        public virtual void OffByObj(Object obj)
        {
            int count = _action_list.Count;
            int index = 0;
            Action<A,B,C> one_action;
            while (count > 0)
            {
                one_action = _action_list[index];
                if (one_action.Target == obj)
                {
                    _action_list.RemoveAt(index);;
                }
                else
                {
                    index++;
                }

                count--;
            }
        }

        public virtual bool IsIn(Action<A,B,C> action)
        {
            return GetIndex(action) != -1;
        }

        public virtual int GetIndex(Action<A,B,C> action)
        {
            for (int i = 0; i < _action_list.Count; i++)
            {
                Action<A,B,C> one_action = _action_list[i];
                if (one_action.Target == action.Target && one_action.Method == action.Method)
                {
                    return i;
                }
            }

            return -1;
        }

        public void OnFetch()
        {
            
        }

        public void OnRecycle()
        {
            Clear();
        }

        public void On(Action a)
        {
            throw new NotImplementedException();
        }

        public void Off(Action a)
        {
            throw new NotImplementedException();
        }

        public void Emit()
        {
            throw new NotImplementedException();
        }
    }
}