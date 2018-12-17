using NUnit.Framework;
using Serilog;
using System;
using System.IO;

namespace Selenium.Basic.NUnitLesson
{
    [SetUpFixture]
    public class SetUp
    {
        private readonly string destinationDirectory = @"C:\TestResults";
       // private readonly string myDirectory = @"C:\VitaliiKravets";

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {

            Directory.CreateDirectory(destinationDirectory);
            string folder = $@"{destinationDirectory}\ {DateTime.Now.ToString("yyyy/MM/dd_HH/mm/ss")}";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithProcessId()
                .WriteTo.File($@"{folder}\logs.txt", Serilog.Events.LogEventLevel.Information, outputTemplate: "[{Timestamp:yyyy/MM/dd HH:mm:ss}][{ProcessId}][{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File($@"{folder}\detailedLogs.txt", outputTemplate: "[{Timestamp:yyyy/MM/dd HH:mm:ss}][{ProcessId}][{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
            Log.Information("creating test setup and adding loging");
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            Log.Information("Teardown");

            Log.CloseAndFlush();
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}