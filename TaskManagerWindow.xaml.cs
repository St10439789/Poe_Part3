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
using Microsoft.VisualBasic;


namespace Poe_Part3
{
    /// <summary>
    /// Interaction logic for TaskManagerWindow.xaml
    /// </summary>
    public partial class TaskManagerWindow : Window
    {
 private List<TaskItem> tasks;

        public TaskManagerWindow(List<TaskItem> existingTasks)
        {
            InitializeComponent();
            tasks = existingTasks;
            RefreshTaskList();
        }

        // Refresh the ListBox display of tasks
        private void RefreshTaskList()
        {
            taskListBox.Items.Clear();
            foreach (var task in tasks)
            {
                string reminder = task.ReminderDate.HasValue
                    ? $" (Reminder: {task.ReminderDate.Value.ToShortDateString()})"
                    : "";
                string completed = task.IsCompleted ? "[✓] " : "";
                taskListBox.Items.Add($"{completed}{task.Title}{reminder}");
            }
        }

        // Add a new task when button clicked
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = Interaction.InputBox("Enter task title:", "Add Task");
            if (string.IsNullOrWhiteSpace(title))
                return;

            string description = Interaction.InputBox("Enter task description:", "Add Task");

            DateTime? reminder = null;
            string reminderInput = Interaction.InputBox("Enter reminder date (YYYY-MM-DD) or leave blank:", "Reminder");
            if (DateTime.TryParse(reminderInput, out DateTime parsedDate))
                reminder = parsedDate;

            tasks.Add(new TaskItem
            {
                Title = title,
                Description = description,
                ReminderDate = reminder,
                IsCompleted = false
            });

            RefreshTaskList();
        }

        // Mark selected task as complete
        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task to complete.");
                return;
            }

            tasks[taskListBox.SelectedIndex].IsCompleted = true;
            RefreshTaskList();
        }

        // Delete selected task
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a task to delete.");
                return;
            }

            tasks.RemoveAt(taskListBox.SelectedIndex);
            RefreshTaskList();
        }
    }
}
