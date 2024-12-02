using System.Windows;
using System.Windows.Controls;
using TaskManagement.UI.Enumerators;
using TaskManagement.UI.Services;
using TaskManagement.UI.ViewModels;

namespace TaskManagement.UI.TaskUI
{
    /// <summary>
    /// Interaction logic for UpdateTaskUI.xaml
    /// </summary>
    public partial class UpdateTaskUI : Window
    {
        private TaskViewModel _taskViewModel;

        public UpdateTaskUI(TaskViewModel taskViewModel)
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;

            this._taskViewModel = taskViewModel;

            LoadValues();
        }

        private const string BaseUrl = "https://localhost:7174/api/Task";

        private void LoadValues()
        {
            txtTitle.Text = _taskViewModel.Title;
            txtDescription.Text = _taskViewModel.Description;
            dpCreatedAt.Value = _taskViewModel.CreatedAt;
            dpCompletedAt.Value = _taskViewModel.UpdatedAt;
            cmbStatus.SelectedIndex = (int)_taskViewModel.Status;

            if (_taskViewModel.Status != EnumTaskStatus.Completed)
            {
                txtCompleteAt.Visibility = Visibility.Hidden;
                dpCompletedAt.Visibility = Visibility.Hidden;
            }
        }

        private async void OnUpdateButtonClick(object sender, RoutedEventArgs e)
        {
            try
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

                var newTask = new TaskUpdateViewModel
                {
                    Id = _taskViewModel.Id,
                    Title = txtTitle.Text,
                    Description = txtDescription.Text,
                    CreatedAt = dpCreatedAt.Value.Value,
                    UpdatedAt = dpCompletedAt.Value is not null ? dpCompletedAt.Value.Value : null,
                    Status = GetSelectedStatus()
                };

                ApiService apiService = new ApiService();

                await apiService.PutAsync<TaskViewModel, TaskUpdateViewModel>(BaseUrl, newTask);

                MessageBox.Show($"Task atualizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
            catch
            {
                return;
            }
        }

        private void cmbStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStatus.SelectedItem is ComboBoxItem selectedItem &&
                selectedItem.Content.ToString() == "Completado")
            {
                dpCompletedAt.Visibility = Visibility.Visible;
                txtCompleteAt.Visibility = Visibility.Visible;
            }
            else
            {
                dpCompletedAt.Visibility = Visibility.Hidden;
                txtCompleteAt.Visibility = Visibility.Hidden;
            }
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
