using System;
using System.Collections.Generic;
using System.Text;
using Workers.core;

namespace Workers
{
    [Importable]
    class MotorsWorker : IWorker
    {
        public string act(string parameters)
        {
            return $" Called: MotorsWorkerAct with params:{parameters}";
        }
    }

    [Importable]
    class JobsWorker : IWorker
    {

        public string act(string parameters)
        {
            return $" Called: JobsWorkerAct with params:{parameters}";
        }
    }
    [Importable]
    class PropertyWorker : IWorker
    {
        public string act(string parameters)
        {
            return $" Called: PropertyWorkerAct with params:{parameters}";
        }
    }
    [Importable]
    public class UndefinedWorker : IWorker
    {
        public string act(string parameters)
        {
            return $" Called undefinedAct";
        }
    }
}
