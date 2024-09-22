using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace semaphore_db_cache
{

    // Пример модели данных
    public class MyData
    {
        public string Key { get; set; }
        public string Value { get; set; }
        // Дополнительные свойства
    }

    // Пример использования DataService с ограничением количества потоков с помощью SemaphoreSlim
    public class Program
    {
        static async Task Main(string[] args)
        {
            // Настройка кэша
            IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

            // Создание экземпляра базы данных
            IDatabase database = new Database();

            // Создание экземпляра сервиса данных
            DataService dataService = new DataService(memoryCache, database);

            // Ключи для запроса
            List<string> keys = new List<string>
            {
                "key1",
                "key2",
                "key3",
                "key4",
                "key5",
                "key1", // Повторный запрос для демонстрации кэша
                "key2",
                "key6",
                "key7",
                "key8"
            };

            // Максимальное количество одновременных задач
            int maxConcurrency = 3;

            // Создание SemaphoreSlim для ограничения количества одновременных задач
            using (Semaphore semaphore = new Semaphore(maxConcurrency, maxConcurrency))
            {
                List<Task> tasks = new List<Task>();

                foreach (var key in keys)
                {
                    // Ожидание возможности войти в семафор
                    semaphore.WaitOne();

                    // Запуск задачи
                    var task = Task.Run(async () =>
                    {
                        try
                        {
                            // Получение данных
                            MyData data = await dataService.GetValueAsync(key);
                            if (data != null)
                            {
                                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Получено: Key = {data.Key}, Value = {data.Value}");
                            }
                            else
                            {
                                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Данные не найдены для ключа '{key}'.");
                            }
                        }
                        finally
                        {
                            // Освобождение семафора
                            semaphore.Release();
                        }
                    });

                    tasks.Add(task);
                }

                // Ожидание завершения всех задач
                await Task.WhenAll(tasks);
            }

            Console.WriteLine("Все задачи завершены.");
        }
    }
}
