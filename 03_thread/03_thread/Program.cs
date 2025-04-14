
#region Intro

//Thread t = new Thread(ShowPlus);
//t.Start();

//Console.WriteLine(t.IsAlive);

//for (int i = 0; i < 1000; ++i)
//    Console.Write('0');

//void ShowPlus()
//{
//    for(int i = 0; i < 1000; ++i)
//        Console.Write('+');
//}





//void Run()
//{
//    for(int i = 0; i < 5; ++i)
//        Console.Write(0);
//}

//new Thread(Run).Start();
//Run();




// =====ПотокоНЕБЕЗОПАСНЫЙ код

//bool done = false;

//void Run()
//{
//    if (! done)
//    {
//        done = true;
//        Console.WriteLine("*");
//        Console.WriteLine("DONE");
//    }
//}

//new Thread(Run).Start();
//Run();




// =====ПотокоНБЕЗОПАСНЫЙ код

//bool done = false;
//object locker = new object();

//void Run()
//{
//    lock(locker)
//    {
//        if (!done)
//        {
//            done = true;
//            Console.WriteLine("*");
//            Console.WriteLine("DONE");
//        }
//    }

//}

//new Thread(Run).Start();
//new Thread(Run).Start();
//new Thread(Run).Start();
//new Thread(Run).Start();
//Run();






//void Run()
//{
//    while(true)
//    {

//    }
//    for (int i = 0; i < 1000; ++i)
//    {
//        Console.Write('*');
//        Thread.Sleep(1);
//        // Thread.Sleep(TimeSpan.FromSeconds(1));
//    }
//    Console.WriteLine();
//}

//Thread t = new Thread(Run);
//t.Start();

//t.Join(500);

//Console.WriteLine("Main finished");

#endregion


#region Create / Start

// ======= Простые методы =======

//void Run()
//{
//    Console.WriteLine("hello");
//}

//Thread t1 = new Thread(Run);
//Thread t2 = new Thread(new ThreadStart(Run));

//t2.Start();




//Thread t = new Thread(() => Console.WriteLine("Vasia"));
//t.Start();


//string name = "Vasia";
//Thread t = new Thread(() => Console.WriteLine(name));
//t.Start();


//void Calc(int a, int b)
//{
//    Console.WriteLine($"a + b = {a + b}");
//}

//int a = 5;
//int b = 9;

//Thread t = new Thread(() => Calc(a, b));



//void Render(object? val)
//{
//    Console.WriteLine(val.ToString());
//}

//Thread t = new Thread(Render);
//t.Start("Petya");



//void RenderColoredMessage(string message, ConsoleColor color)
//{
//    Console.ForegroundColor = color;
//    Console.WriteLine(message);
//    Console.ResetColor();
//}

//string msg = "Hello";
//ConsoleColor color = ConsoleColor.Red;
//Thread t = new Thread(() => RenderColoredMessage(msg, color));
//t.Start();




// ======= "Подводный камень" с лямбдой =======

// :-(((
//for (int i = 0; i < 10; ++i)
//    new Thread(() => Console.WriteLine(i)).Start();

// :-))
//for (int i = 0; i < 10; ++i)
//{
//    int x = i;
//    new Thread(() => Console.WriteLine(x)).Start();
//}



//int i;
//List<Thread> threads = new List<Thread>();
//for (i = 0; i < 10; ++i)
//    threads.Add(new Thread(() => Console.WriteLine(i)));

//threads.ForEach(t => t.Start());




// ======= Отладка =======

//void Run()
//{
//    Console.WriteLine($"Message from {Thread.CurrentThread.Name}");
//    Console.WriteLine();
//}

//Thread.CurrentThread.Name = "main";
//Thread t = new Thread(Run)
//{
//    Name = "worker"
//};
//t.Start();
//Run();




//Thread t = new Thread(() => Console.WriteLine("hohoho"));

//if (args.Length > 0)
//    t.IsBackground = true;

//t.Start();

#endregion


#region try / catch

// :-(((
//void Run()
//{
//    throw new Exception("Test Exception");
//}

//try
//{
//    new Thread(Run).Start();
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"ERROR: {ex.Message}");
//}

// :-)))
//void Run()
//{

//    try
//    {
//        throw new Exception("Test Exception");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"ERROR: {ex.Message}");
//    }
//}

//new Thread(Run).Start();

#endregion


#region Tread pooling, TPL (Task Parallel Library)

//// Task     Parallel        Task<T>      ValueTask<> ......

//void Run()
//{
//    Console.WriteLine("Vasia");
//}

//Task task = Task.Factory.StartNew(Run);

////
////

//task.Wait();

//await task;





//using System.Net;

//string DownloadPage(string ulr)
//{
//    WebClient client = new WebClient();
//    return client.DownloadString(ulr);
//}

//// Console.WriteLine(DownloadPage(@"https://google.com"));

//string url = @"https://google.com";

//Task<string> task = Task.Factory.StartNew(() => DownloadPage(url));
////
////
//string content = task.Result;                                       // BLOCKING!!!
//Console.WriteLine(content);



#endregion