using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroBang.General
{
  public class SpecialQueue<T>
  {
    LinkedList<T> list = new LinkedList<T>();

    public SpecialQueue(List<T> externalList)
    {
      foreach (T t in externalList)
      {
        list.AddLast(t);
      }
    }

    public void Enqueue(T t)
    {
      list.AddLast(t);
    }

    public T Dequeue()
    {
      var result = list.First.Value;
      list.RemoveFirst();
      return result;
    }

    public T Peek()
    {
      return list.First.Value;
    }

    public bool Remove(T t)
    {
      return list.Remove(t);
    }

    public int Count { get { return list.Count; } }

    public T FirstAndRemove(Func<T, bool> pred)
    {
      T first = list.First(pred);
      if (first != null)
        Remove(first);
      return first;
    }

    public List<T> ConvertToList()
    {
      return list.ToList();
    }

  }
}
