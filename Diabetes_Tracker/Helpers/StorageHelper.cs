using System;
using System.IO;

namespace Diabetes_Tracker.Helpers
{
    public static class StorageHelper
    {
        public static void WriteFile(string fileName, string text)
        {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docsPath, fileName);
            using (var writer = File.CreateText(path))
            {
                writer.WriteAsync(text);
            }
        }

        public static void Reset()
        {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var data = Path.Combine(docsPath, "log.json");

            if (File.Exists(data)) File.Delete(data);
        }

        public static string ReadFile(string fileName)
        {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docsPath, fileName);

            return File.ReadAllText(path);
        }

        public static bool FirstRun()
        {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(docsPath, "name.dat");

            return !File.Exists(path);
        }
    }
}