using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using static System.Console;

public class OWCTools
{
    public long CountBytesFromFile(string filePath)
    {
        return new FileInfo(filePath).Length;
    }

    public string SepareteWordsFilePath(string filePath)
    {
        return Path.GetFileName(filePath);
    }

    public string ShowOptionsUsage()
    {
        const string usage_helper = @"Usage: ccwc [OPTION]... [FILE]...
    -c, --bytes     print the byte counts
    -m, --chars     print the character counts
    -l, --lines     print the newline counts
    -w, --words     print the word counts

    With no FILE, or when FILE is -, read standard input.";
        return usage_helper;
    }

    public long CountWords(string text)
    {
        var words = text.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }

    public long CountLines(string file)
    {
        return File.ReadLines(file).Count();
    }

    public long CountCharacters(string file)
    {
        string content = File.ReadAllText(file);
        return content.Length;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var tools = new OWCTools();
        bool isStdin = false;
        string input = "";
        string filePathForOutput = "";
        string option = "";
        var allowedOptions = new[] { 'l', 'w', 'c', 'm' };
        var requestedOptions = new List<char>();

        if (args.Length == 0)
        {
            WriteLine(tools.ShowOptionsUsage());
            return;
        }

        if (args[0].StartsWith("-"))
        {
            option = args[0];
            requestedOptions = option.TrimStart('-')
                                    .ToLower()
                                    .Distinct()
                                    .Where(c => allowedOptions.Contains(c))
                                    .ToList();

            if (args.Length > 1 && !args[1].StartsWith("-"))
            {
                filePathForOutput = args[1]; // O segundo argumento é o "nome do arquivo" para saída
                isStdin = true; // Assume a entrada stdin quando um nome é dado após a opção
                input = In.ReadToEnd(); // Lê da entrada padrão
            }
            else
            {
                isStdin = true;
                input = In.ReadToEnd();
            }
        }
        else
        {
            filePathForOutput = args[0];
            if (File.Exists(filePathForOutput))
            {
                input = File.ReadAllText(filePathForOutput);
            }
            else
            {
                WriteLine($"ccwc: {filePathForOutput}: No such file or directory");
                return;
            }
            requestedOptions.AddRange(new[] { 'l', 'w', 'c' });
        }

        if (requestedOptions.Count == 0)
            requestedOptions.AddRange(new[] { 'l', 'w', 'c' });

        var results = new Dictionary<char, long>();

        foreach (char opt in requestedOptions)
        {
            switch (opt)
            {
                case 'c':
                    results['c'] = System.Text.Encoding.UTF8.GetByteCount(input);
                    break;

                case 'l':
                    results['l'] = input.Count(c => c == '\n');
                    break;

                case 'w':
                    results['w'] = tools.CountWords(input);
                    break;

                case 'm':
                    results['m'] = System.Globalization.StringInfo.ParseCombiningCharacters(input).Length;
                    break;
            }
        }

        var order = new[] { 'l', 'w', 'c', 'm' };
        foreach (var key in order)
        {
            if (results.ContainsKey(key))
                Write($"{results[key],8}");
        }

        if (!string.IsNullOrEmpty(filePathForOutput))
            Write($" {filePathForOutput}");

        WriteLine();
    }
}