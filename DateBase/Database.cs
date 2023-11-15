using System;
using System.Collections.Generic;
using System.IO;

namespace DatabaseLibrary
{
    public class Database
    {
        private string filePath;

        public Database(string filePath)
        {
            this.filePath = filePath;
        }

        public void Add(int id, string name, string message)
        {
            string record = $"{id},{name},{message}";

            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(record);
            }
        }

        public List<string> GetByID(int id)
        {
            List<string> records = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    if (fields.Length >= 3 && fields[0] == id.ToString())
                    {
                        records.Add(line);
                    }
                }
            }

            return records;
        }

        public List<string> GetByName(string name)
        {
            List<string> records = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    if (fields.Length >= 3 && fields[1] == name)
                    {
                        records.Add(line);
                    }
                }
            }

            return records;
        }

        public void Update(int id, string newMessage)
        {
            List<string> updatedRecords = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    if (fields.Length >= 3 && fields[0] == id.ToString())
                    {
                        fields[2] = newMessage;
                        line = string.Join(",", fields);
                    }

                    updatedRecords.Add(line);
                }
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string record in updatedRecords)
                {
                    writer.WriteLine(record);
                }
            }
        }

        public void Delete(int id)
        {
            List<string> remainingRecords = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');

                    if (!(fields.Length >= 3 && fields[0] == id.ToString()))
                    {
                        remainingRecords.Add(line);
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string record in remainingRecords)
                {
                    writer.WriteLine(record);
                }
            }
        }
    }
}