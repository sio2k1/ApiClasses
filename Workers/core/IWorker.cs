using System;
using System.Collections.Generic;
using System.Text;

namespace Workers.core
{
    public interface IWorker
    {
        string act(string parameters);
    }
}
