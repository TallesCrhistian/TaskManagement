﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManagement.UI.TaskUI;

namespace TaskManagement.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;
        }

        private void OnListTasksClick(object sender, RoutedEventArgs e)
        {
            ListTaskUI listTask = new ListTaskUI();

            listTask.ShowDialog();
        }

        private void OnCreateTaskClick(object sender, RoutedEventArgs e)
        {
            CreateTaskUI createTaskUI = new CreateTaskUI();

            createTaskUI.ShowDialog();
        }
    }
}