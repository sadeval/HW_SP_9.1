using System;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

class Program
{
    public static string text = "Пример текста для анализа.";

    static void Main()
    {
        Console.WriteLine("Текущий текст: " + text);

        while (true)
        {
            Console.WriteLine("\nВведите операцию для выполнения с текстом (или 'exit' для выхода): ");
            Console.WriteLine("Пример: text.Length, text.Split(' ').Length (количество слов)");

            string? input = Console.ReadLine();

            if (input.ToLower() == "exit")
            {
                break;
            }

            try
            {
                // Создаем объект для передачи в скрипт
                var globals = new Globals { text = text };

                // Выполняем скрипт, передавая в него глобальные переменные
                var result = CSharpScript.EvaluateAsync(input,
                    ScriptOptions.Default
                        .WithReferences(AppDomain.CurrentDomain.GetAssemblies())
                        .WithImports("System", "System.Linq"),
                    globals: globals // Передаем глобальные переменные
                ).Result;

                Console.WriteLine("Результат: " + result);
            }
            catch (CompilationErrorException ex)
            {
                Console.WriteLine("Ошибка компиляции: " + string.Join(Environment.NewLine, ex.Diagnostics));
            }
        }
    }
}

// Класс для глобальных переменных, которые будут доступны в скрипте
public class Globals
{
    public string? text;
}
