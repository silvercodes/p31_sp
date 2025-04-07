
using System;
using System.Reflection;
using System.Runtime.Loader;

//AppDomain domain = AppDomain.CurrentDomain;

//Console.WriteLine($"{domain.FriendlyName}\t{domain.BaseDirectory}");
//Console.WriteLine("--------------------------");

//foreach (Assembly a in domain.GetAssemblies())
//    Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}\t{a.Location}");






// Статическая загрузка сборки (Информация о сборке MathLib.dll ---> метаданные анализируются)

//AppDomain domain = AppDomain.CurrentDomain;

//Console.WriteLine($"{domain.FriendlyName}\t{domain.BaseDirectory}");
//Console.WriteLine("--------------------------");

//foreach (Assembly a in domain.GetAssemblies())
//    Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}\t{a.Location}");

//Calculator calculator = new Calculator();
//Console.WriteLine(calculator.Sum(3, 4));





// Динамическая загрузка сборки

AppDomain domain = AppDomain.CurrentDomain;


Console.WriteLine("-----------BEFORE LOADING---------------");

foreach (Assembly a in domain.GetAssemblies())
    Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}\t{a.Location}");


var ctx = new AssemblyLoadContext("lib_ctx", true);
ctx.Unloading += ctx => Console.WriteLine("AssemblyContext unloaded!!!!!!!!!!!!!!!!!!!!!");

Assembly assembly = ctx.LoadFromAssemblyPath(Path.Combine(Directory.GetCurrentDirectory(), "MathLib.dll"));


Console.WriteLine("-----------AFTER LOADING---------------");

foreach (Assembly a in domain.GetAssemblies())
    Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}\t{a.Location}");


Type? type = assembly.GetType("MathLib.Calculator");

// static call
//MethodInfo? staticMethod = type.GetMethod("Factorial");
//int? factorial = (int?)staticMethod.Invoke(assembly, new object[] { 5 });
//Console.WriteLine($"Factorial = {factorial}");

// non-static call
MethodInfo? method = type.GetMethod("Sum");
object? calculator = Activator.CreateInstance(type);
int? sum = (int?)method.Invoke(calculator, new object[] { 5, 4 });
Console.WriteLine($"Sum = {sum}");


ctx.Unload();
GC.Collect();


Console.WriteLine("-----------AFTER UNLOADING---------------");

foreach (Assembly a in domain.GetAssemblies())
    Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}\t{a.Location}");






