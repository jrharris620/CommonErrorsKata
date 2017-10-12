using CommonErrorsKata.Shared;
using System.IO;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonErrorsKata
{
    public partial class CommonErrorsForm : Form
    {
        private const int MinRightAnswers = 15;

        private readonly AnswerQueue<TrueFalseAnswer> answerQueue;
        private readonly string[] files;
        private readonly SynchronizationContext synchronizationContext;
        private int i = 100;
        private string currentBaseName = null;
        private readonly string[] possibleAnswers = null;
        private readonly string[] fileNames = null;

        public CommonErrorsForm()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
            files = Directory.GetFiles(Environment.CurrentDirectory +  @"..\..\ErrorPics");
            possibleAnswers = new string[] { "Missing File", "null instance", "divide by zero" };
            fileNames = new string[] {"object_ref.png", "object_ref_not_set.png", "divide_by_zero.png"};

            lstAnswers.DataSource = possibleAnswers;
            answerQueue = new AnswerQueue<TrueFalseAnswer>(MinRightAnswers);
            AskNextQuestion();
            lstAnswers.Click += LstAnswers_Click;
            StartTimer();
        }
        private async void StartTimer()
        {
            await Task.Run(() =>
            {
                for (i = 100; i > 0; i--)
                {
                    UpdateProgress(i);
                    Thread.Sleep(50);
                }
                Message("Need to be quicker on your feet next time!  Try again...");
            });
        }

        private void LstAnswers_Click(object sender, EventArgs e)
        {
            i = 100;
            var currentImageName = currentBaseName.Split(' ').First();

            var correctIndex = Array.IndexOf(fileNames, currentImageName);

            if (correctIndex < 0 || correctIndex >= possibleAnswers.Length)
            {
                return;
            }

            var correctAnswer = possibleAnswers[correctIndex];

            Console.WriteLine(sender);

            var isCorrectAnswer = lstAnswers.SelectedItem.ToString() == correctAnswer;
            answerQueue.Enqueue(new TrueFalseAnswer(isCorrectAnswer));


            //TODO:  Figure out what is a valid answer.
            answerQueue.Enqueue(new TrueFalseAnswer(true));
            AskNextQuestion();
        }

        private void AskNextQuestion()
        {
            if (answerQueue.Count == MinRightAnswers && answerQueue.Grade >= 98)
            {
                MessageBox.Show("Congratulations you've defeated me!");
                Application.Exit();
                return;
            }
            label1.Text = answerQueue.Grade.ToString() + "%";
            var file = files.GetRandom();
            currentBaseName= Path.GetFileName(file);
            pbImage.ImageLocation = file;
        }

        public void UpdateProgress(int value)
        {
            synchronizationContext.Post(new SendOrPostCallback(x => {
                progress.Value = value;
            }), value);
        }
        public void Message(string value)
        {
            synchronizationContext.Post(new SendOrPostCallback(x => {
                MessageBox.Show(value);
            }), value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
