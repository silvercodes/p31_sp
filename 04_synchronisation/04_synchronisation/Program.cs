// ==== Инструменты синхронизации ====

// 1. Простые методы блокировки (Thread.Sleep(...), Thread.Join(...), Task.Wait(...) .......)

// 2. Контроль критической секции (lock, Monitor{20нс}, Mutex{1000нс}, SpinLock, Semaphore{1000нс}, SemaphoreSlim .....)

// 3. Инструменты сигнализации (Monitor.Pulse(), .PulseAll(), .Wait(), AutoResetEvent, ManualResetEvent, CountdownResetEvent....)

// 4. Неблокирующие инструменты (MemoryBarrier, Interlocked, Thread.VolitileRead.....)


#region Блокировка lock / Monitor (эксклюзивная блокировка)

//Thread t = Thread.CurrentThread;
//bool isBlocked = (t.ThreadState & ThreadState.WaitSleepJoin) != 0;  // :-)
//Console.WriteLine(isBlocked);



// Условия разблокировки
// 1. Выполнение условий блокировки
// 2. Таймаут
// 3. Thread.Interupt();
// 4. Thread.Abort();





//new Thread(ThreadSafe.Run).Start();
//ThreadSafe.Run();


//static class ThreadSafe
//{
//    static int a = 10;
//    static int b = 20;
//    static object locker = new object();

//    public static void Run()
//    {
//        int c = 0;

//        // FIFO
//        lock (locker)
//        {
//            if (b != 0)
//            {
//                c = a / b;
//            }

//            b = 0;
//        }
//    }
//}





//new Thread(ThreadSafe.Run).Start();
//ThreadSafe.Run();

//class ThreadSafe
//{
//    static int a = 10;
//    static int b = 20;
//    static object locker = new object();

//    public static void Run()
//    {
//        int c = 0;

//        bool flag = false;

//        try
//        {
//            Monitor.Enter(locker, ref flag);              // Взятие блокировки (попытка)

//            if (b != 0)
//            {
//                c = a / b;
//            }

//            b = 0;
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine($"Error: {ex.Message}");
//        }
//        finally
//        {
//            if (flag)
//                Monitor.Exit(locker);           // Освобождение блокировки
//        }
//    }
//}








//object locker = new object();
//int val = 0;


//void Run()
//{
//    bool flag = false;

//    try
//    {
//        flag = Monitor.TryEnter(locker, 1000);

//        if (flag)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name} in section");
//            for (int i = 0; i < 10; ++i)
//            {
//                Console.WriteLine($"{Thread.CurrentThread.Name}: {val++}");
//                Thread.Sleep(200);
//            }
//        }
//        else
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name} is looser");
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error: {ex.Message}");
//    }

//    finally
//    {
//        if (flag)
//            Monitor.Exit(locker);
//    }
//}

//for (int i = 0; i < 3; ++i)
//{
//    Thread t = new Thread(Run)
//    {
//        Name = $"thread_{i}",
//    };
//    t.Start();
//}






// ==== deadlock

//object locker1 = new object();      // <--
//object locker2 = new object();      // <--

//new Thread(() =>
//{
//    lock(locker1)
//    {
//        Thread.Sleep(1000);

//        lock(locker2)
//        {
//            //
//            //
//        }
//    }
//}).Start();


//lock (locker2)
//{
//    Thread.Sleep(1000);

//    lock (locker1)
//    {
//        //
//        //
//    }
//}

#endregion


#region Mutex (эксклюзивная блокировка)

//int count = 0;
//Mutex mutex = new Mutex();


//void UseResource()
//{
//    if (mutex.WaitOne(500))
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name} take the mutex");

//        Thread.Sleep(200);
//        count++;

//        Console.WriteLine($"{Thread.CurrentThread.Name} done the work");

//        Console.WriteLine($"{Thread.CurrentThread.Name} release mutex");

//        mutex.ReleaseMutex();
//    }
//    else
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name} is looser");
//    }
//}

//void StartThreads()
//{
//    for (int i = 0; i < 5; ++i)
//    {
//        Thread t = new Thread(UseResource)
//        {
//            Name = $"thread_{i}",
//        };

//        t.Start();
//    }
//}

//StartThreads();


#endregion


#region Semaphore (НЕэксклюзивная блокировка)

//Semaphore semaphore = new Semaphore(0, 3);
//int executionTime = 0;
//object locker = new object();

//void Run(int id)
//{
//    Console.WriteLine($"Thread {id} started");

//    semaphore.WaitOne();                                      // попытка взять блокировку (пройти за семафор)

//    Console.WriteLine($"Thread {id} passed semaphore");

//    int time;
//    lock (locker)
//    {
//        executionTime += 200;
//        time = executionTime;
//    }

//    Thread.Sleep(time + 2000);

//    Console.WriteLine($"Thread {id} relesed semaphore");

//    semaphore.Release();                                    // освобождает 1 место
//}

//for (int i = 1; i <= 5; ++i)
//{
//    int x = i;
//    Thread t = new Thread(() => Run(x));
//    t.Start();
//}

//Thread.Sleep(3000);
//semaphore.Release(3);                                       // // освобождает 3 места


#endregion


#region Signaling

//Pingator pingator = new Pingator();
//ThreadManager tm1 = new ThreadManager("ping", pingator);
//ThreadManager tm2 = new ThreadManager("pong", pingator);

//class Pingator
//{
//    private object locker = new object();

//    public void Ping(bool run = true)
//    {
//        lock (locker)
//        {
//            if (!run)
//            {
//                Monitor.Pulse(locker);
//                return;
//            }
//            Console.WriteLine("PING");
//            Thread.Sleep(300);

//            Monitor.Pulse(locker);
//            Monitor.Wait(locker);
//        }
//    }

//    public void Pong(bool run = true)
//    {
//        lock (locker)
//        {
//            if (!run)
//            {
//                Monitor.Pulse(locker);
//                return;
//            }
//            Console.WriteLine("PONG");
//            Thread.Sleep(300);

//            Monitor.Pulse(locker);
//            Monitor.Wait(locker);
//        }
//    }
//}

//class ThreadManager
//{
//    private Thread thread;
//    private Pingator pingator;

//    public ThreadManager(string name, Pingator pingator)
//    {
//        thread = new Thread(Run)
//        {
//            Name = name,
//        };

//        this.pingator = pingator;

//        thread.Start();
//    }

//    public void Run()
//    {
//        if (thread.Name == "ping")
//        {
//            for (int i = 0; i < 10; ++i)
//                pingator.Ping(true);

//            pingator.Ping(false);
//        }
//        else
//        {
//            for (int i = 0; i < 10; ++i)
//                pingator.Pong(true);

//            pingator.Pong(false);
//        }
//    }
//}









//object locker = new object();

//void first()
//{
//    try
//    {
//        Monitor.Enter(locker);

//        for (int i = 1; i <= 10; i += 2)
//        {
//            Thread.Sleep(1000);
//            Console.WriteLine($" {i}");
//            Monitor.Pulse(locker);
//            Monitor.Wait(locker);
//        }

//    }
//    finally
//    {
//        Monitor.Exit(locker);
//    }
//}
//void second()
//{
//    try
//    {
//        Monitor.Enter(locker);

//        for (int i = 0; i <= 10; i += 2)
//        {
//            Thread.Sleep(200);
//            Console.WriteLine($" {i}");
//            Monitor.Pulse(locker);
//            Monitor.Wait(locker);
//        }

//    }
//    finally
//    {
//        Monitor.Exit(locker);
//    }
//}

//Thread t1 = new Thread(first);
//Thread t2 = new Thread(second);

//t2.Start();
//Thread.Sleep(1000);
//t1.Start();









//AutoResetEvent arev = new AutoResetEvent(false);
//EventWaitHandle arev2 = new EventWaitHandle(false, EventResetMode.AutoReset);

//SimpleWaitHandler.Run();
//static class SimpleWaitHandler
//{
//    static EventWaitHandle wh = new AutoResetEvent(false);

//    public static void Run()
//    {
//        new Thread(Work).Start();
//        Thread.Sleep(3000);
//        wh.Set();                       // Перевод wh в сигнальное состояние
//    }

//    static void Work()
//    {
//        Console.WriteLine("Work() waiting");
//        wh.WaitOne();                   // Ждем сигнального состояния
//        Console.WriteLine("Working....");
//    }
//}






//DualSignaling.Run();
//class DualSignaling
//{
//    static EventWaitHandle workerWh = new AutoResetEvent(false);
//    static EventWaitHandle mainWh = new AutoResetEvent(false);

//    static string? message;
//    static object locker = new object();


//    public static void Run()
//    {
//        new Thread(Worker).Start();

//        workerWh.WaitOne();

//        lock (locker)
//            message = "Vasia";
//        mainWh.Set();
//        workerWh.WaitOne();

//        lock (locker)
//            message = "Petya";
//        mainWh.Set();
//        workerWh.WaitOne();

//        lock (locker)
//            message = null;
//        mainWh.Set();
//    }

//    private static void Worker()
//    {
//        while (true)
//        {
//            workerWh.Set();
//            mainWh.WaitOne();

//            lock (locker)
//            {
//                if (message is null)
//                    return;

//                Thread.Sleep(1000);
//                Console.WriteLine($"From worker: {message}");
//            }
//        }
//    }
//}







//AutoResetEvent are = new AutoResetEvent(false);

//for (int i = 0; i < 5; ++i)
//{
//    Thread t = new Thread(Render)
//    {
//        Name = $"thread_{i}"
//    };

//    t.Start();
//}

//Thread.Sleep(3000);
//are.Set();

//void Render()
//{
//    are.WaitOne();
//    for (int i = 0; i < 10; ++i)
//    {
//        Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
//        Thread.Sleep(100);
//    }
//    are.Set();
//}





//ManualResetEvent mre = new ManualResetEvent(false);

//UserThread ut1 = new UserThread("first", mre);

//Console.WriteLine("Waiting...");
//mre.WaitOne();
//Console.WriteLine("Working...");
//mre.Reset();


//UserThread ut2 = new UserThread("second", mre);

//Console.WriteLine("Waiting...");
//mre.WaitOne();
//Console.WriteLine("Working...");
//mre.Reset();




//class UserThread
//{
//    private ManualResetEvent mre;
//    public Thread Thread { get; set; }

//    public UserThread(string name, ManualResetEvent mre)
//    {
//        this.mre = mre;

//        Thread = new Thread(Run)
//        {
//            Name = name,
//        };

//        Thread.Start();
//    }

//    private void Run()
//    {
//        Console.WriteLine($"Thread {Thread.CurrentThread.Name} started...");

//        for (int i = 0; i < 5; ++i)
//        {
//            Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
//            Thread.Sleep(200);
//        }

//        mre.Set();
//    }
//}

#endregion
