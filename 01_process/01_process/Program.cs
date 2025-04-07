
// Поток (Thread) (Размещение и выполнение кода)

// Процесс (Process)

// Адрессное прстранство (Memory scope)

// Ресурсы

// Приложение (Application)

// Сборка (Assembly)

// Модуль (Module)



// ======= Process ========
// 1. Memory scope
// 2. Исполняемый код
// 3. Системные дискрипторы
// 4. Контест системы безопасности
// 5. Идентификатор
// 6. Переменные окружения
// 7. Приоритет
// 8. Как минимум один поток выполнения






//using System.Diagnostics;

//Process[] processes = Process.GetProcesses();

//var prc = processes.OrderBy(p => p.Id);

//foreach (Process p in prc)
//    Console.WriteLine($"{p.Id} {p.ProcessName}");


//Console.ReadLine();






using System.Diagnostics;

Run();
void Run()
{
    string? input;

    while (true)
    {
        Console.WriteLine("1. Show all processes");
        Console.WriteLine("2. Get process info by ID");
        Console.WriteLine("3. Show threads");
        Console.WriteLine("4. Show modules");
        Console.WriteLine("5. Start process");
        Console.WriteLine("6. Kill process");

        input = Console.ReadLine();

        switch(input)
        {
            case "1":
                ShowAllProcesses();
                break;
            case "2":
                ShowProcessById();
                break;
            case "3":
                ShowThreads();
            break;
            case "4":
                ShowModules();
                break;
            case "5":
                StartProcess();
                break;
            case "6":
                KillProcess();
                break;
        }
    }

}

void ShowError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}

void ShowAllProcesses()
{
    Process[] processes = Process.GetProcesses();

    var prc = processes.OrderBy(p => p.Id);

    foreach (Process p in prc)
        Console.WriteLine($"{p.Id} {p.ProcessName}");
}

void ShowProcessById()
{
    Console.Write("Enter PID: ");
    string input = Console.ReadLine();

    try
    {
        int pid = int.Parse(input);

        Process p = Process.GetProcessById(pid);

        Console.WriteLine($"{p.Id}\t{p.ProcessName}\t{p.BasePriority}\t{p.StartTime}");

    }
    catch (Exception e)
    {
        ShowError(e.Message);
    }
}

void ShowThreads()
{
    Console.Write("Enter PID: ");
    string input = Console.ReadLine();

    try
    {
        int pid = int.Parse(input);

        Process p = Process.GetProcessById(pid);

        var threads = p.Threads;

        Console.WriteLine("Threads info:");

        foreach (ProcessThread t in threads)
            Console.WriteLine($"{t.Id}\t{t.StartTime.ToShortTimeString()}\t{t.PriorityLevel}");

    }
    catch (Exception e)
    {
        ShowError(e.Message);
    }
}

void ShowModules()
{
    Console.Write("Enter PID: ");
    string input = Console.ReadLine();

    try
    {
        int pid = int.Parse(input);

        Process p = Process.GetProcessById(pid);

        var modules = p.Modules;

        foreach(ProcessModule m in modules)
            Console.WriteLine($"{m.ModuleName}\t{m.ModuleMemorySize}");

    }
    catch (Exception e)
    {
        ShowError(e.Message);
    }
}

void StartProcess()
{
    // Process.Start("notepad");

    // Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", "https://wikipedia.org --incognito");

    // Process.Start(@"C:\Users\ThinkPad\Desktop\test\x64\Debug\test.exe");
}

void KillProcess()
{
    Console.Write("Enter PID: ");
    string input = Console.ReadLine();

    try
    {
        int pid = int.Parse(input);

        Process p = Process.GetProcessById(pid);

        p.Kill();

    }
    catch (Exception e)
    {
        ShowError(e.Message);
    }
}