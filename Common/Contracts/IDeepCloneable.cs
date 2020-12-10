using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Contracts
{
    public interface IDeepCloneable<T> where T : class
    {
        T DeepCopy();
    }
}
