using System.Collections;
using System.Collections.Generic;

namespace Griphon.Structure.Elements
{
    public class PageCollection: IList<Page>
    {
        public IEnumerator<Page> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Page item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(Page item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(Page[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Page item)
        {
            throw new System.NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(Page item)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(int index, Page item)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new System.NotImplementedException();
        }

        public Page this[int index]
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }
    }
}