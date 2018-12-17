using NUnit.Framework;
using System;
using System.IO;

namespace Selenium.Basic.NUnitLesson
{
    [SetUpFixture]
    public class SetUp
    {
        private readonly string myDirectory = @"C:\VitaliiKravets";
        private readonly string destinationDirectory = @"C:\TestResults";

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            Directory.CreateDirectory(myDirectory);

        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            Copy(myDirectory, destinationDirectory);
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