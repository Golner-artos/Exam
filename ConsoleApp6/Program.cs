using Microsoft.VisualBasic;
using System;
using System.Collections;

namespace ConsoleApp6
{

    class Program
    {
        static void Main(string[] args)
        {
            QuizSystem app = new QuizSystem();
            app.Start();
        }

    }
    class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime DateBirth { get; set; }
        public List<QuizResult> Results { get; set; } = new();
    }
    class QuizResult
    {
        public string Category { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
    class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public List<int> CorrectAnswers { get; set; }
    }
    class QuizSystem
    {
        private List<User> users = new();
        private List<Question> history = new();
        private User currentUser;

        public QuizSystem()
        {
            FillQuestions();
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Exit");

                switch (Console.ReadLine())
                {
                    case "1": Login(); break;
                    case "2": Register(); break;
                    case "3": return;
                }
            }
        }
        private void Register()
        {
            Console.Write("Login: ");
            string login = Console.ReadLine();

            if (users.Any(x => x.Login == login))
            {
                Console.WriteLine("Login already exists");
                Console.ReadKey();
                return;
            }
            Console.Write("Password: ");
            string pass = Console.ReadLine();

            Console.Write("Birth date: ");
            DateTime birth = DateTime.Parse(Console.ReadLine());

            users.Add(new User
            {
                Login = login,
                Password = pass,
                DateBirth = birth
            });

            Console.WriteLine("Registration successful");
            Console.ReadKey();
        }
        private void Login()
        {
            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Password: ");
            string pass = Console.ReadLine();

            currentUser = users.FirstOrDefault(
                x => x.Login == login && x.Password == pass);

            if (currentUser == null)
            {
                Console.WriteLine("Wrong login or password");
                Console.ReadKey();
                return;
            }

            UserMenu();
        }
        private void UserMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Start Quiz");
                Console.WriteLine("2. My Results");
                Console.WriteLine("3. Logout");

                switch (Console.ReadLine())
                {
                    case "1": StartQuiz(); break;
                    case "2": ShowResults(); break;
                    case "3": return;
                }
            }
        }
        private void StartQuiz()
        {
            int score = 0;

            foreach (var q in history.Take(5))
            {
                Console.Clear();
                Console.WriteLine(q.Text);

                for (int i = 0; i < q.Answers.Count; i++)
                    Console.WriteLine($"{i + 1}. {q.Answers[i]}");

                int answer = int.Parse(Console.ReadLine()) - 1;

                if (q.CorrectAnswers.Contains(answer))
                    score++;
            }
            currentUser.Results.Add(new QuizResult
            {
                Category = "History",
                Score = score,
                Date = DateTime.Now
            });
            Console.WriteLine($"Score: {score}");
            Console.ReadKey();
        }
        private void ShowResults()
        {
            foreach (var r in currentUser.Results)
                Console.WriteLine($"{r.Category}: {r.Score}");

            Console.ReadKey();
        }
        private void FillQuestions()
        {
            history.Add(new Question
            {
                Text = "Capital of France?",
                Answers = new() { "London", "Paris", "Berlin" },
                CorrectAnswers = new() { 1 }
            });
        }

        }
}
