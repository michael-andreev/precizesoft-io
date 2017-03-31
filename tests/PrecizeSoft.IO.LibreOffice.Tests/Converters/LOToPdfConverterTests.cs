using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PrecizeSoft.IO.Converters;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace PrecizeSoft.IO.Tests.Converters
{
    public class LOToPdfConverterTests: IClassFixture<LOFixture>
    {
        [Theory]
        [InlineData(@"..\..\..\..\samples\mini.docx", 2, 2, false)]
        [InlineData(@"..\..\..\..\samples\mini.docx", 2, 2, true)]
        [InlineData(@"..\..\..\..\samples\mini.docx", 10, 10, false)]
        [InlineData(@"..\..\..\..\samples\mini.docx", 10, 10, true)]
        [InlineData(@"..\..\..\..\samples\mini.docx", 30, 2, false)]
        [InlineData(@"..\..\..\..\samples\mini.docx", 30, 2, true)]
        //[InlineData(@"..\..\..\..\samples\mini.docx", 100, 2, false)]
        public void ParallelTest(string sourceFileName, int threadsCount, int iterationsCount, bool singleConverter)
        {
            string destinationDirectoryName = $"ParallelTest\\{threadsCount}-{iterationsCount}-{singleConverter}";
            DirectoryInfo destinationDirectory = new DirectoryInfo(destinationDirectoryName);
            string destinationFileNameTemplate = destinationDirectoryName + @"\{threadNumber}-{iterationNumber}.pdf";

            byte[] sourceFileBytes = File.ReadAllBytes(sourceFileName);
            string sourceFileExtension = Path.GetExtension(sourceFileName);

            if (destinationDirectory.Exists)
            {
                destinationDirectory.Delete(true);
            }
            destinationDirectory.Create();

            LOToPdfConverter converter = singleConverter ? new LOToPdfConverter() : null;

            Dictionary<Thread, ThreadParameters> threads = new Dictionary<Thread, ThreadParameters>();

            for (int i=0; i<threadsCount; i++)
            {
                Thread th = new Thread(new ParameterizedThreadStart(ParallelTestThread));

                ThreadParameters par = new ThreadParameters()
                {
                    IterationsCount = iterationsCount,
                    Converter = converter,
                    SourceFileBytes = sourceFileBytes,
                    SourceFileExtension = sourceFileExtension,
                    DestinationFileNameTemplate = destinationFileNameTemplate.Replace("{threadNumber}", (i + 1).ToString())
                };
                threads.Add(th, par);
            }

            foreach (var thread in threads)
                thread.Key.Start(thread.Value);

            foreach (var thread in threads)
                thread.Key.Join();

            Assert.True(destinationDirectory.GetFiles().Count() == threadsCount * iterationsCount);
        }

        public static void ParallelTestThread(object parameters)
        {
            Debug.WriteLine($"Thread started: {Thread.CurrentThread.ManagedThreadId}");

            ThreadParameters threadParams = (ThreadParameters)parameters;

            IFileConverter converter = threadParams.Converter ?? new LOToPdfConverter();

            Debug.WriteLine("Converter created: {Thread.CurrentThread.ManagedThreadId}");

            for (int i = 1; i <= threadParams.IterationsCount; i++)
            {
                File.WriteAllBytes(threadParams.DestinationFileNameTemplate.Replace("{iterationNumber}", i.ToString()),
                    converter.Convert(threadParams.SourceFileBytes, threadParams.SourceFileExtension));
            }
        }
    }
}
