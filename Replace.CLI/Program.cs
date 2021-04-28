using System;
using System.IO;
using System.Linq;

namespace Replace.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var directory = @"d:\projects\...";
            var from = "<From_this>";
            var to = "<To_that>";
            var ext = "edmx";

            var files = Directory.GetFiles(directory)
                .Where(f =>
                    !Path.GetExtension(f).Replace(".", "").Equals("exe", StringComparison.OrdinalIgnoreCase) &&
                    (string.IsNullOrWhiteSpace(ext) || Path.GetExtension(f).Replace(".", "").Equals(ext, StringComparison.OrdinalIgnoreCase))).ToArray();

            if (files.Length == 0)
            {
                Console.WriteLine("No files found");
                return;
            }

            foreach (var file in files)
            {
                var fileContent = File.ReadAllText(file);
                fileContent = fileContent.Replace(from, to);
                
                File.WriteAllText(file, fileContent);
                Console.WriteLine($"{file} changed");
            }
            
            Console.WriteLine("All done!");
        }
    }
}
