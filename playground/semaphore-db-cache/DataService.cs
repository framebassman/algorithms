using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace semaphore_db_cache
{
    public class DataService
    {
        private readonly IMemoryCache _cache;
        private readonly IDatabase _database;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public DataService(IMemoryCache cache, IDatabase database)
        {
            _cache = cache;
            _database = database;

            // Настройка опций кэша (например, время жизни кэша)
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5), // Абсолютное время истечения
                SlidingExpiration = TimeSpan.FromMinutes(2), // Скользывающееся время истечения
                // Можно добавить другие опции, такие как приоритет или колбэки на удаление
            };
        }

        /// <summary>
        /// Получает данные по ключу, сначала пытаясь извлечь из кэша, затем из базы данных.
        /// </summary>
        /// <param name="key">Ключ для поиска данных.</param>
        /// <returns>Экземпляр MyData или null, если данные не найдены.</returns>
        public async Task<MyData> GetValueAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            // Попытка получить данные из кэша
            if (_cache.TryGetValue(key, out MyData cachedData))
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Данные найдены в кэше для ключа '{key}'.");
                return cachedData;
            }

            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Данные не найдены в кэше для ключа '{key}'. Обращаемся к базе данных...");

            // Если данных нет в кэше, извлекаем из базы данных
            MyData dbData = await _database.GetDataAsync(key);

            if (dbData != null)
            {
                // Сохраняем данные в кэше для последующих запросов
                _cache.Set(key, dbData, _cacheOptions);
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Данные сохранены в кэше для ключа '{key}'.");
            }
            else
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] Данные не найдены в базе данных для ключа '{key}'.");
            }

            return dbData;
        }
    }
}
