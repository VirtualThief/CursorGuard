﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using CursorGuard.Annotations;

namespace CursorGuard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IForegroundWindowMonitor monitor = new ForegroundWindowMonitor();
        private readonly IWindowProcessLocator processLocator = new WindowProcessLocator();

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindowModel Model { get; } = new MainWindowModel();

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            monitor.ForegroundWindowChanged += info =>
            {
                var processInfo = processLocator.GetProcessInfo(info);
                
                this.Model.ForegroundText = info.Handle.ToString();
                this.Model.Left = info.Left;
                this.Model.Top = info.Top;
                this.Model.Right = info.Right;
                this.Model.Bottom = info.Bottom;
                this.Model.ProcessId = processInfo.Id;
            };
            monitor.StartMonitoringAsync();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            monitor.Dispose();
        }
    }

    public class MainWindowModel : INotifyPropertyChanged
    {
        private string focusText;
        private string foregroundText;
        private int left;
        private int top;
        private int right;
        private int bottom;
        private int processId;

        public string FocusText
        {
            get { return focusText; }
            set
            {
                if (value == focusText) return;
                focusText = value;
                OnPropertyChanged();
            }
        }

        public string ForegroundText
        {
            get { return foregroundText; }
            set
            {
                if (value == foregroundText) return;
                foregroundText = value;
                OnPropertyChanged();
            }
        }

        public int Left
        {
            get { return left; }
            set
            {
                if (value == left) return;
                left = value;
                OnPropertyChanged();
            }
        }

        public int Top
        {
            get { return top; }
            set
            {
                if (value == top) return;
                top = value;
                OnPropertyChanged();
            }
        }

        public int Right
        {
            get { return right; }
            set
            {
                if (value == right) return;
                right = value;
                OnPropertyChanged();
            }
        }

        public int Bottom
        {
            get { return bottom; }
            set
            {
                if (value == bottom) return;
                bottom = value;
                OnPropertyChanged();
            }
        }

        public int ProcessId
        {
            get { return processId; }
            set
            {
                if (value == processId) return;
                processId = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
