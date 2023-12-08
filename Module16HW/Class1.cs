using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module16HW
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Просмотр содержимого директории");
                Console.WriteLine("2. Создание файла/директории");
                Console.WriteLine("3. Удаление файла/директории");
                Console.WriteLine("4. Копирование файла/директории");
                Console.WriteLine("5. Перемещение файла/директории");
                Console.WriteLine("6. Чтение из файла");
                Console.WriteLine("7. Запись в файл");
                Console.WriteLine("8. Логирование действий");
                Console.WriteLine("0. Выход");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListDirectoryContents();
                        break;
                    case "2":
                        CreateFileOrDirectory();
                        break;
                    case "3":
                        DeleteFileOrDirectory();
                        break;
                    case "4":
                        CopyFileOrDirectory();
                        break;
                    case "5":
                        MoveFileOrDirectory();
                        break;
                    case "6":
                        ReadFromFile();
                        break;
                    case "7":
                        WriteToFile();
                        break;
                    case "8":
                        LogActions();
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Повторите попытку.");
                        break;
                }
            }
        }

        static void ListDirectoryContents()
        {
            Console.Write("Введите путь к директории: ");
            string path = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("Список файлов:");
                foreach (var file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }

                Console.WriteLine("\nСписок директорий:");
                foreach (var directory in directories)
                {
                    Console.WriteLine(Path.GetFileName(directory));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CreateFileOrDirectory()
        {
            Console.Write("Введите полный путь для создания файла/директории: ");
            string path = Console.ReadLine();

            Console.Write("Выберите тип (F - файл, D - директория): ");
            char type = char.ToUpper(Console.ReadKey().KeyChar);

            try
            {
                if (type == 'F')
                {
                    File.Create(path).Close();
                    Console.WriteLine("\nФайл создан успешно.");
                }
                else if (type == 'D')
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("\nДиректория создана успешно.");
                }
                else
                {
                    Console.WriteLine("\nНеверный тип. Создание отменено.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }
        }

        static void DeleteFileOrDirectory()
        {
            Console.Write("Введите полный путь к файлу/директории для удаления: ");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine("\nФайл удален успешно.");
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine("\nДиректория удалена успешно.");
                }
                else
                {
                    Console.WriteLine("\nФайл/директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }
        }

        static void CopyFileOrDirectory()
        {
            Console.Write("Введите полный путь к файлу/директории для копирования: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите полный путь для копии файла/директории: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, destinationPath);
                    Console.WriteLine("\nФайл скопирован успешно.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    DirectoryCopy(sourcePath, destinationPath, true);
                    Console.WriteLine("\nДиректория скопирована успешно.");
                }
                else
                {
                    Console.WriteLine("\nФайл/директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }
        }

        static void MoveFileOrDirectory()
        {
            Console.Write("Введите полный путь к файлу/директории для перемещения: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите полный путь для перемещения файла/директории: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, destinationPath);
                    Console.WriteLine("\nФайл перемещен успешно.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, destinationPath);
                    Console.WriteLine("\nДиректория перемещена успешно.");
                }
                else
                {
                    Console.WriteLine("\nФайл/директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }
        }

        static void ReadFromFile()
        {
            Console.Write("Введите полный путь к файлу для чтения: ");
            string filePath = Console.ReadLine();

            try
            {
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    Console.WriteLine($"\nСодержимое файла:\n{content}");
                }
                else
                {
                    Console.WriteLine("\nФайл не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }
        }

        static void WriteToFile()
        {
            Console.Write("Введите полный путь к файлу для записи: ");
            string filePath = Console.ReadLine();

            Console.WriteLine("Введите текст для записи в файл (Ctrl + Z для завершения):");
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    string line;
                    while ((line = Console.ReadLine()) != null)
                    {
                        writer.WriteLine(line);
                    }
                }

                Console.WriteLine("\nЗапись в файл выполнена успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nОшибка: {ex.Message}");
            }
        }

        static void LogActions()
        {
            // Добавь здесь код для ведения лога действий.
            // Можешь использовать StreamWriter для записи в файл лога.
        }

        // Метод для рекурсивного копирования директорий
        static void DirectoryCopy(string sourceDir, string destDir, bool copySubDirs)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDir);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDir, file.Name);
                file.CopyTo(tempPath, true);
            }

            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDir, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }
    }

}
