using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;


namespace Poe_Part3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Store tasks and log
        private List<TaskItem> taskList = new();
        private List<string> activityLog = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Handle Send button
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string input = user_question.Text.Trim();
            if (string.IsNullOrWhiteSpace(input))
                return;

            show_chats.Items.Add("You: " + input);
            user_question.Clear();

            string response = ProcessInput(input);
            show_chats.Items.Add("Bot: " + response);
            show_chats.Items.Add("");
        }

        //  NLP conversation logic
        private string ProcessInput(string input)
        {
            input = input.ToLower();

            // Greetings
            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
                return "Hello! 👋 How can I help you with cybersecurity today?";

            // Password advice
            if (input.Contains("password"))
                return "Always use strong, unique passwords and consider using a password manager to keep them safe.";

            // Phishing info
            if (input.Contains("phishing"))
                return "Phishing is a scam where attackers try to steal your info. Be careful with suspicious emails or links!";
            
            //ransomware
            if (input.Contains("ransomware"))
                return "💣 Ransomware locks your files and demands payment. Backup your data regularly and avoid opening suspicious files.";

            // privacy
            if (input.Contains("privacy"))
                return "🔏 Limit app permissions, disable unnecessary tracking, and use privacy-focused browsers or extensions.";

            // VPN explanation
            if (input.Contains("vpn"))
                return "🔒 A VPN encrypts your internet connection and hides your IP address. It’s great for privacy, especially on public Wi-Fi.";

            // Scam detection
            if (input.Contains("scam") || input.Contains("scammer"))
                return "🚨 Scammers often use urgency or threats. Don’t share personal info with unknown contacts and report any suspicious messages.";

            // Public Wi-Fi
            if (input.Contains("public wifi") || input.Contains("public network"))
                return "📶 Avoid logging into sensitive accounts on public Wi-Fi. Use a VPN for extra protection when browsing in public.";

            // Identity theft
            if (input.Contains("identity theft"))
                return "🆔 Be cautious with your personal info online. Use credit monitoring and never share ID numbers over insecure platforms.";

            // the two step 
            if (input.Contains("two factor authentication"))
                return "Enabling two-factor authentication helps protect your account even if your password gets stolen.";
  
            if (input.Contains("social engineering"))
                return "🧠 Social engineering tricks people into giving away secrets. Always verify requests before sharing personal data.";

            // Malware advice
            if (input.Contains("malware") || input.Contains("virus"))
                return "Make sure your antivirus software is up to date and avoid downloading files from untrusted sources.";

            if (input.Contains("safe browsing") || input.Contains("browse safely"))
                return "🌐 Avoid clicking on pop-ups, check URLs before clicking, and enable browser security settings.";

           

            // Add task reminder
            if (input.Contains("add task") || input.Contains("remind me") || input.Contains("set reminder"))
            {
                string taskTitle = "Unnamed Task";
                if (input.Contains("to"))
                {
                    int index = input.IndexOf("to");
                    taskTitle = input[(index + 2)..].Trim();
                }

                var task = new TaskItem
                {
                    Title = taskTitle,
                    Description = "Added by user",
                    ReminderDate = null,
                    IsCompleted = false
                };

                taskList.Add(task);
                activityLog.Add($"Task added: '{taskTitle}'");
                return $"Task added: '{taskTitle}'. Would you like to set a reminder?";
            }

            // Start quiz
            if (input.Contains("start quiz") || input.Contains("quiz"))
            {
                activityLog.Add("User started the quiz.");
                QuizWindow1 quiz = new QuizWindow1(LogActivity);
                quiz.ShowDialog();
                return "Starting cybersecurity quiz...";
            }

            // Show recent log
            if (input.Contains("log") || input.Contains("what have you done") || input.Contains("activity"))
            {
                return GetRecentLog();
            }

            // Default fallback
            return "Sorry, I didn't understand that. You can ask me about passwords, phishing, malware, or try commands like 'add task' or 'start quiz'.";
        }

        // Open Task Manager Window
        private void ManageTasks_Click(object sender, RoutedEventArgs e)
        {
            TaskManagerWindow manager = new TaskManagerWindow(taskList);
            manager.ShowDialog();
        }

        // Launch Quiz Window
        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            activityLog.Add("User started the quiz.");
            QuizWindow1 quiz = new QuizWindow1(LogActivity);
            quiz.ShowDialog();
        }

        // Show recent actions
        private void ShowLog_Click(object sender, RoutedEventArgs e)
        {
            string logSummary = GetRecentLog();
            show_chats.Items.Add("C: " + logSummary);
        }

        // Log from quiz or other window
        private void LogActivity(string log)
        {
            activityLog.Add(log);
        }

        // Return last 5 log entries
        private string GetRecentLog()
        {
            if (activityLog.Count == 0)
                return "No recent activity.";

            var last5 = activityLog.Count > 5
                ? activityLog.GetRange(activityLog.Count - 5, 5)
                : activityLog;

            return "Recent activity:\n" + string.Join("\n", last5);
        }
    }

}



