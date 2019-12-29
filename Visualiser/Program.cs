using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;

namespace Visualiser
{
    static class Program
    {
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
        public class Options
        {
            [Option('f', "filePath", Required = true, HelpText = "Set the file to save to.")]
            public string FilePath { get; private set; }

            [Option('r', "resPow", Required = false, Default = 12,
                HelpText = "The power to raise 2 to to calculate the resolution.")]
            public int ResPow { get; private set; }

            [Option('t', "trackTime", Required = false, Default = false,
                HelpText = "Whether to stop the time it takes to generate an image.")]
            public bool TrackTime { get; set; }
        }

        static void Main(string[] args)
        {
            // File path to save image to
            string file;

            // image parameters
            int width = 0;
            int height = 0;

            // Time Tracking
            bool trackTime = false;
            Stopwatch stopwatch = new Stopwatch();

            Parser.Default.ParseArguments<Options>(args).WithParsed<Options>(o =>
            {
                file = o.FilePath;

                width = (int) Math.Pow(2, o.ResPow);
                height = (int) Math.Pow(2, o.ResPow);

                trackTime = o.TrackTime;
            });

            Console.WriteLine($"Generating {width}x{height} Mandelbrot canvas.");

            if (trackTime)
            {
                stopwatch.Start();
            }

            MandelbrotGenerator mandelbrotGenerator = new MandelbrotGenerator(width, height);

            Image img = mandelbrotGenerator.GetCanvas();

            if (trackTime)
            {
                stopwatch.Stop();
                Console.WriteLine($"Rendered canvas in {stopwatch.Elapsed}!");
            }
            else
            {
                Console.WriteLine($"Rendered canvas!");
            }

            img.Save(file, ImageFormat.Png);
            Console.WriteLine($"Saved image to '{file}'.");
        }
    }
}
