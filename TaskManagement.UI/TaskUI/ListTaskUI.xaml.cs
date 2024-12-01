using System.Windows;
using System.Windows.Controls;
using TaskManagement.UI.Entities;
using TaskManagement.UI.Enumerators;
using TaskManagement.UI.Services;
using TaskManagement.UI.ViewModels;

namespace TaskManagement.UI.TaskUI
{
    /// <summary>
    /// Lógica interna para ListTaskUI.xaml
    /// </summary>
    public partial class ListTaskUI : Window
    {
        ApiService apiService;

        public ListTaskUI()
        {
            InitializeComponent();

            this.WindowState = WindowState.Maximized;

            apiService = new ApiService();

            GetAll();
        }

        private const string BaseUrl = "http://localhost:5196/api/Task";

        private async void OnFilterClick(object sender, RoutedEventArgs e)
        {
            var filter = new TaskFilterViewModel
            {
                Title = txtTitleFilter.Text,
                Description = txtDescriptionFilter.Text,
                Status = cmbStatusFilter.SelectedIndex != -1 ? (EnumTaskStatus)cmbStatusFilter.SelectedIndex + 1 : null,
                CreatedAt = dpCreatedAtFilter.Value,
                UpdatedAt = dpUpdatedAtFilter.Value
            };

            var tasks = await apiService.GetListAsync<TaskViewModel, TaskFilterViewModel>(filter, 1, BaseUrl);

            dataGridTarefas.ItemsSource = null;

            if (tasks.GenericData is not null)
                dataGridTarefas.ItemsSource = tasks.GenericData.Data;
        }


        private async Task GetAll()
        {
            var tasks = await apiService.GetListAsync<TaskViewModel, TaskFilterViewModel>(new TaskFilterViewModel() { Status = EnumTaskStatus.Pending }, 1, BaseUrl);

            dataGridTarefas.ItemsSource = tasks.GenericData.Data;
        }

        private void OnEditClick(object sender, RoutedEventArgs e)
        {            
            Button button = sender as Button;
            var task = button?.DataContext as TaskEntity;
            if (task != null)
            {                
                MessageBox.Show($"Editing task: {task.Title}");
            }
        }

        private async void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            var task = button?.DataContext as TaskViewModel;

            if (task != null)
            {
                var result = MessageBox.Show($"Você deseja realmente deletar essa tarefa: {task.Title}?", "Confirmar Deleleção", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    await apiService.DeleteAsync<TaskViewModel>(BaseUrl, task.Id);

                    MessageBox.Show($"Tarefa {task.Title} deletada.");

                    var itemsSource = dataGridTarefas.ItemsSource as IList<TaskViewModel>;

                    if (itemsSource != null)
                    {
                        if (task != null)
                        {
                            itemsSource.Remove(task);
                        }
                    }

                    dataGridTarefas.ItemsSource = null;
                    dataGridTarefas.ItemsSource = itemsSource;
                }
            }
        }
    }
}