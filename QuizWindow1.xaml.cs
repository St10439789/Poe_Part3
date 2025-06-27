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

namespace Poe_Part3

    {
        public partial class QuizWindow1 : Window
        {
            // To log activities back to the main app or caller
            private readonly Action<string> logActivity;

            // Define Question as nested class
            private class Question
            {
                public string Text { get; set; }
                public List<string> Options { get; set; }
                public int CorrectIndex { get; set; }
            }

            private readonly List<Question> questions = new()
        {
            // Original questions
            new Question
            {
                Text = "What should you do if you receive a suspicious email?",
                Options = new List<string> { "Open it", "Reply", "Report it", "Ignore it" },
                CorrectIndex = 2
            },
            new Question
            {
                Text = "Which of these is a strong password?",
                Options = new List<string> { "123456", "password", "P@ssw0rd!", "abcdef" },
                CorrectIndex = 2
            },
            new Question
            {
                Text = "What is phishing?",
                Options = new List<string> { "A type of malware", "An attempt to steal info by pretending to be trustworthy", "A software update", "A firewall feature" },
                CorrectIndex = 1
            },

            // NEW Part 2 questions appended
            new Question
            {
                Text = "What is the safest way to manage your passwords?",
                Options = new List<string> { "Use the same password everywhere", "Write them on paper", "Use a password manager", "Include your name and birth year" },
                CorrectIndex = 2
            },
            new Question
            {
                Text = "What should you do if you suspect a scam message?",
                Options = new List<string> { "Ignore and delete your inbox", "Be cautious and verify the sender", "Reply with fake info", "Tell your friends to open it" },
                CorrectIndex = 1
            },
            new Question
            {
                Text = "How can you improve your online privacy?",
                Options = new List<string> { "Share everything publicly", "Ignore privacy settings", "Adjust social media and browser settings", "Use public Wi-Fi often" },
                CorrectIndex = 2
            },
            new Question
            {
                Text = "What does a VPN do?",
                Options = new List<string> { "Blocks ads", "Tracks your data", "Creates a secure connection", "Makes browsing slower" },
                CorrectIndex = 2
            },
            new Question
            {
                Text = "Which of these is a sign of phishing?",
                Options = new List<string> { "Well-written emails", "Spelling mistakes and suspicious URLs", "Known senders only", "Bank messages with full info" },
                CorrectIndex = 1
            },
            new Question
            {
                Text = "What is malware?",
                Options = new List<string> { "A hardware upgrade", "Harmful software that damages or steals info", "An antivirus program", "A firewall setting" },
                CorrectIndex = 1
            },
            new Question
            {
                Text = "What is ransomware?",
                Options = new List<string> { "A music app", "A VPN service", "Software that locks your files for payment", "A browser plugin" },
                CorrectIndex = 2
            },
            new Question
            {
                Text = "What is social engineering?",
                Options = new List<string> { "A coding language", "A virus type", "Tricking people to give up private info", "A way to design websites" },
                CorrectIndex = 2
            },
            new Question
            {
                Text = "What does sabotage mean in cybersecurity?",
                Options = new List<string> { "Helping IT teams", "Intentional damage to systems", "Fixing errors", "Creating software updates" },
                CorrectIndex = 1
            },
            new Question
            {
                Text = "How can you practice safe browsing?",
                Options = new List<string> { "Use public Wi-Fi often", "Avoid HTTPS websites", "Click all popups", "Use secure (HTTPS) sites and avoid shady websites" },
                CorrectIndex = 3
            }
        };

            private int currentQuestionIndex = 0;
            private int score = 0;
            private int? selectedOptionIndex = null;

            public QuizWindow1(Action<string> logActivity)
            {
                InitializeComponent();
                this.logActivity = logActivity ?? (_ => { }); // fallback no-op
                LoadQuestion();
            }

            private void LoadQuestion()
            {
                selectedOptionIndex = null;
                NextButton.IsEnabled = false;

                var current = questions[currentQuestionIndex];
                QuestionText.Text = current.Text;

                OptionsPanel.Children.Clear();

                for (int i = 0; i < current.Options.Count; i++)
                {
                    var btn = new RadioButton()
                    {
                        Content = current.Options[i],
                        Margin = new Thickness(0, 5, 0, 5),
                        Tag = i,
                        GroupName = "OptionsGroup",
                        FontSize = 16
                    };
                    btn.Checked += Option_Checked;
                    OptionsPanel.Children.Add(btn);
                }

                logActivity($"Loaded question {currentQuestionIndex + 1}: {current.Text}");
            }

            private void Option_Checked(object sender, RoutedEventArgs e)
            {
                var rb = sender as RadioButton;
                if (rb != null)
                {
                    selectedOptionIndex = (int)rb.Tag;
                    NextButton.IsEnabled = true;
                    logActivity($"Option selected: {rb.Content}");
                }
            }

            private void NextButton_Click(object sender, RoutedEventArgs e)
            {
                if (selectedOptionIndex == questions[currentQuestionIndex].CorrectIndex)
                {
                    score++;
                    logActivity("Correct answer!");
                }
                else
                {
                    logActivity("Wrong answer.");
                }

                currentQuestionIndex++;

                if (currentQuestionIndex >= questions.Count)
                {
                    ShowResult();
                }
                else
                {
                    LoadQuestion();
                }
            }

            private void ShowResult()
            {
                QuestionText.Visibility = Visibility.Collapsed;
                OptionsPanel.Visibility = Visibility.Collapsed;
                NextButton.Visibility = Visibility.Collapsed;

                ScoreText.Text = $"Quiz complete! Your score: {score} / {questions.Count}";
                ScoreText.Visibility = Visibility.Visible;

                logActivity($"Quiz finished. Final score: {score} out of {questions.Count}");
            }
        }
    }


