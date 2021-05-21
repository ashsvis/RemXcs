using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DataEditor
{
    [Serializable]
    public class StackMemory
    {
        int stackDepth; // глубина стека
        List<byte[]> list = new List<byte[]>();
        public StackMemory(int depth)
        {
            stackDepth = depth;
            if (depth < 1) stackDepth = 1;
            list.Clear();
        }
        public void Push(MemoryStream stream)
        {
            if (list.Count > stackDepth) list.RemoveAt(0);
            list.Add(stream.ToArray());
        }
        public void Clear()
        {
            list.Clear();
        }
        public int Count
        {
            get { return (list.Count); }
        }
        public void Pop(MemoryStream stream)
        {
            if (list.Count > 0)
            {
                byte[] buff = list[list.Count - 1];
                stream.Write(buff, 0, buff.Length);
                list.RemoveAt(list.Count - 1);
            }
        }
    }
}
