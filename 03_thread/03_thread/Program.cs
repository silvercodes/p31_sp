
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