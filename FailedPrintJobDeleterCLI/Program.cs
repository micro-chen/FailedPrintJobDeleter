﻿using System;
using FailedPrintJobDeleter;

namespace FailedPrintJobDeleterCLI
{
    static class Program
    {
        static void Main(string[] args)
        {
            var config = new Config();
            config.Load();

            // setup logging, including console logging
            Util.SetupLogging();
            SetupConsoleLogging();

            // prepare everything
            var deleter = new JobDeleter(config);

            deleter.Start();

            Console.WriteLine("Starting. Press Enter or Escape to stop.");
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            Console.WriteLine("Stopping...");

            deleter.Stop();
        }

        static void SetupConsoleLogging()
        {
            var rootLogger = ((log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository()).Root;
            var consoleAppender = new log4net.Appender.ConsoleAppender
            {
                Threshold = log4net.Core.Level.Debug,
                Layout = new log4net.Layout.PatternLayout("%date [%thread] %-5level %logger [%property{NDC}] - %message%newline")
            };
            consoleAppender.ActivateOptions();
            rootLogger.AddAppender(consoleAppender);
        }
    }
}
