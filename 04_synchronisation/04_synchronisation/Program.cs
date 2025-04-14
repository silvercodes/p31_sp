// ==== Инструменты синхронизации ====

// 1. Простые методы блокировки (Thread.Sleep(...), Thread.Join(...), Task.Wait(...) .......)

// 2. Контроль критической секции (lock, Monitor{20нс}, Mutex{1000нс}, SpinLock, Semaphore{1000нс}, SemaphoreSlim .....)

// 3. Инструменты сигнализации (Monitor.Pulse(), .PulseAll(), .Wait(), AutoResetEvent, ManualResetEvent, CountdownResetEvent....)

// 4. Неблокирующие инструменты (MemoryBarrier, Interlocked, Thread.VolitileRead.....)



