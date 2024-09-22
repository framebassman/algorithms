namespace semaphore_db_cache
{
    // Интерфейс для абстракции доступа к базе данных
    public interface IDatabase
    {
        Task<MyData> GetDataAsync(string key);
    }

    // Пример реализации IDatabase (для демонстрации)
    public class Database : IDatabase
    {
        public async Task<MyData> GetDataAsync(string key)
        {
            // Симуляция задержки доступа к базе данных
            await Task.Delay(100); // Задержка 100 мс

            // Для примера возвращаем данные, если ключ не пустой
            if (!string.IsNullOrEmpty(key))
            {
                return new MyData
                {
                    Key = key,
                    Value = $"Value for {key}"
                };
            }

            return null;
        }
    }
}
