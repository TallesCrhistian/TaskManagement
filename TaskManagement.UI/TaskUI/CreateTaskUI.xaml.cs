using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskManagement.UI.Entities;
using TaskManagement.UI.Enumerators;
using TaskManagement.UI.Services;
using TaskManagement.UI.ViewModels;

namespace TaskManagement.UI.TaskUI
{
    /// <summary>
    /// Lógica interna para CreateTaskUI.xaml
    /// </summary>
    public partial class CreateTaskUI : Window
    {
        public CreateTaskUI()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;                  
        }

        private async void OnSaveButtonClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Titulo é obrigatório.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (dpCreatedAt == null)
            {
                MessageBox.Show("Data de criação é obrigatória.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var newTask = new TaskCreateViewModel
            {
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                CreatedAt = dpCreatedAt.Value.Value,
                UpdatedAt = dpCompletedAt.Value is not null ? dpCompletedAt.Value.Value : null,
                Status = GetSelectedStatus()
            };            

            ApiService apiService = new ApiService();

            await apiService.PostAsync<TaskViewModel, TaskCreateViewModel>("http://localhost:5196/api/Task", newTask);

            MessageBox.Show($"Task criada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

            txtTitle.Clear();
            txtDescription.Clear();
            cmbStatus.SelectedIndex = -1;
        }

        private EnumTaskStatus GetSelectedStatus()
        {
            if (cmbStatus.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Pendente":
                        return EnumTaskStatus.Pending;
                    case "Em Progresso":
                        return EnumTaskStatus.InProgress;
                    case "Completado":
                        return EnumTaskStatus.Completed;
                    default:
                        return EnumTaskStatus.Pending;
                }
            }
            return EnumTaskStatus.Pending;
        }
    }
}
