using System;
using DatabaseLibrary;
using HtmlAgilityPack;
using System.IO;
using System.Net;

namespace DatabaseExample
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseLibrary.Database database = new DatabaseLibrary.Database("database.rtf");

            // Добавление записи
            database.Add(1, "John", "Hello, World!");

            // Получение записи по ID
            var recordsByID = database.GetByID(1);
            foreach (var record in recordsByID)
            {
                Console.WriteLine(record);
            }

            // Получение записей по имени
            var recordsByName = database.GetByName("John");
            foreach (var record in recordsByName)
            {
                Console.WriteLine(record);
            }

            // Изменение сообщения по ID
            database.Update(1, "New message");

            // Удаление записи по ID
            database.Delete(1);

            // Путь к файлу database.rtf
            string filePath = "database.rtf";

            // Загрузка страницы форума
            string url = "https://php.ru/forum/threads/skolko-vremeni-polzovatel-provel-na-stranice.65380/";
            string html;
            using (WebClient client = new WebClient())
            {
                html = client.DownloadString(url);
            }

            // Создание объекта HtmlDocument для парсинга HTML
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Поиск всех сообщений на странице
            HtmlNodeCollection messages = doc.DocumentNode.SelectNodes("//div[contains(@class, 'message')]");

            // Проверка наличия сообщений
            if (messages != null)
            {
                // Открытие файла для записи
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    // Итерация по найденным сообщениям
                    foreach (HtmlNode message in messages)
                    {
                        // Получение ID сообщения
                        string messageId = message.GetAttributeValue("data-message-id", "");

                        // Получение логина пользователя
                        HtmlNode usernameNode = message.SelectSingleNode(".//a[contains(@class, 'username')]");
                        string username = usernameNode != null ? usernameNode.InnerText.Trim() : string.Empty;

                        // Получение текста сообщения
                        HtmlNode textNode = message.SelectSingleNode(".//div[contains(@class, 'messageText')]");
                        string text = textNode != null ? textNode.InnerText.Trim() : string.Empty;

                        // Запись информации в файл
                        writer.WriteLine($"Message ID: {messageId}");
                        writer.WriteLine($"Username: {username}");
                        writer.WriteLine($"Text: {text}");
                        writer.WriteLine();
                    }
                }

                Console.WriteLine("Парсинг и сохранение данных завершены.");
            }
            else
            {
                Console.WriteLine("Сообщения не найдены на странице.");
            }

            Console.ReadLine();
        }
    }
}
    
    

