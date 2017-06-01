﻿#region using directives

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using POGOProtos.Data;
using POGOProtos.Inventory.Item;
using POGOProtos.Map.Fort;
using POGOProtos.Map.Pokemon;
using PoGoEmulator.CommandLineUtility;
using PoGoEmulator.Helpers;
using PoGoEmulator.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoGoEmulator.Logging;
using PoGoEmulator.Enums;
using PoGoEmulator.Models;
using PoGoEmulator.Machine;

#endregion


namespace PoGoEmulator.Forms
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        public static MainForm Instance;
        public static SynchronizationContext SynchronizationContext;
        private string[] args;

        public MainForm(string[] _args)
        {
            InitializeComponent();
            SynchronizationContext = SynchronizationContext.Current;
            Instance = this;
            args = _args;
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ErrorHandler);
        }

        private void ErrorHandler(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.ExceptionObject.ToString());
            ConsoleHelper.ShowConsoleWindow();
        }

        private static DateTime LastClearLog = DateTime.Now;

        public static void ColoredConsoleWrite(Color color, string text)
        {
            if (text.Length <= 0)
                return;

            if (Instance.InvokeRequired)
            {
                Instance.Invoke(new Action<Color, string>(ColoredConsoleWrite), color, text);
                return;
            }

            if (LastClearLog.AddMinutes(20) < DateTime.Now)
            {
                Instance.logTextBox.Text = string.Empty;
                LastClearLog = DateTime.Now;
            }

            Instance.logTextBox.SelectionColor = color;
            Instance.logTextBox.AppendText(text + $"\r\n");
            Instance.logTextBox.ScrollToCaret();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.splitContainer1.SplitterDistance = this.splitContainer1.Width / 100 * 45; // Splits left & right splitter panes @ 45%/55% of the window width
            this.splitContainer2.SplitterDistance = this.splitContainer2.Height / 100 * 45;// Always keeps the logger window @ 45%/55% of the window height
            this.Refresh(); // Force screen refresh before items are poppulated
            InitializeMap();
        }

        private void InitializeMap()
        {
            var lat = 40.7681770900937;
            var lng = -73.9814966214881;
            GMapControl1.MapProvider = GoogleMapProvider.Instance;
            GMapControl1.Manager.Mode = AccessMode.ServerOnly;
            GMapProvider.WebProxy = null;
            GMapControl1.Position = new PointLatLng(lat, lng);
            GMapControl1.DragButton = MouseButtons.Left;
            GMapControl1.MinZoom = 2;
            GMapControl1.MaxZoom = 18;
            GMapControl1.Zoom = 15;
        }

        public static PogoMachine machine;

        public static void Garbage()
        {
            #region Start GC Collector

            Task.Run(() =>
            {
                while (true)
                {
                    GC.Collect();
                    Thread.Sleep((int)Global.Cfg.GarbageTime.TotalMilliseconds);
                }
            });

            #endregion Start GC Collector
        }

        public Task StartServer()
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");

                Logger.AddLogger(new ConsoleLogger(LogLevel.Info));

#if DEBUG
                Logger.Write("ON", LogLevel.Debug);
#endif
                Garbage();
                Assets.ValidateAssets();

                Global.GameMaster = new GameMaster();

                Task run = Task.Factory.StartNew(() =>
                {
                    machine = new PogoMachine();
                    machine.Run();
                });
                string line = "";
                do
                {
                    line = Console.ReadLine();
                    switch (line)
                    {
                        case "help":
                            Logger.Write(" - help menu", LogLevel.Help);
                            break;
                            /*case "gui":
                                StartGui();
                                break;*/
                    }
                } while (line != "exit");
            }
            catch (Exception e)
            {
                Logger.Write(e);
            }
            machine?.Stop();
            Console.ReadLine();
            return null;
        }

        private void StartStopBotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (startStopBotToolStripMenuItem.Text != "Exit")
            {
                startStopBotToolStripMenuItem.Text = "Exit";
                Task.Run(StartServer);
                return;
            }
            Environment.Exit(0);
        }
    }
}

