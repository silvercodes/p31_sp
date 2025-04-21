
#region Создание задач

//Task t1 = new Task(() => Console.WriteLine("Vasia"));
//t1.Start();

//Task t2 = Task.Factory.StartNew(() => Console.WriteLine("Petya"));

//Task t3 = Task.Run(() => Console.WriteLine("Dima"));

//t1.Wait();
//t2.Wait();
//t3.Wait();

#endregion


#region Синхронное выполнение Task'a

Task t = new Task(() =>
{
    Console.WriteLine("start");
    Thread.Sleep(1000);
    Console.WriteLine("end");
});

// t.Start();               // async call   !!!!
t.RunSynchronously();       // sync call    !!!!


Console.WriteLine("Main end");
Console.ReadLine();

#endregion



