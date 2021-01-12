using System;
using System.Collections;
using System.Reflection;
using System.Linq;
using Workers.core;
using Workers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace lzLoad
{   
    class Program
    {
        static Dictionary<string, IWorker> workersStore = new Dictionary<string, IWorker>();
        //static Hashtable workersStore = new Hashtable();
        private static readonly object balanceLock = new object();
        public static bool load(string worker)
        {
            Assembly assembly = Assembly.Load("Workers");
            Type tp = assembly.GetTypes().Where(t => t.Name == worker).FirstOrDefault();

            if ((tp != null) && (Attribute.GetCustomAttributes(tp).ToList().Any(a => a is ImportableAttribute)))
            {
                IWorker reflectedWorker = Activator.CreateInstance(tp) as IWorker;
                lock (balanceLock)
                {
                    if (!workersStore.ContainsKey(worker))
                    {
                        workersStore.Add(worker, reflectedWorker);
                        Console.WriteLine("Loaded: " + worker);
                    }
                }
                return true;
            }
            return false;
            
        }
        public static string proceed(string name, string param)
        {
            IWorker worker = null;
            if (!workersStore.ContainsKey(name))
            {
                if (load(name)) 
                {
                    worker = workersStore[name] as IWorker;
                    
                } else
                {
                    worker = new UndefinedWorker();
                }   
            } else
            {
                worker = workersStore[name] as IWorker;
            }
            return worker.act(param);
            //Console.WriteLine(worker.act(param));
        }
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<Task> tl = new List<Task>();
            for (int n=0;n<100; n++)
            {
                tl.Add(
                    Task.Run(() => {
                        for (int i = 0; i < 100; i++)
                        {
                            proceed("PropertyWorker", "");
                            proceed("PropertyWorker", "");
                            proceed("JobsWorker", "");
                            proceed("MotorsWorker", "");
                        }
                    })
                );
            }

            Task.WaitAll(tl.ToArray());
            sw.Stop();
            Console.WriteLine("Elapsed={0}", sw.Elapsed);
        }
    }
}
