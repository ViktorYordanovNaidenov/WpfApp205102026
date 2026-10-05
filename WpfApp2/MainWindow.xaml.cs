using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Student> studentsList = new ObservableCollection<Student>();

        public MainWindow()
        {
            InitializeComponent();
            StudentsListBox.ItemsSource = studentsList;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs(out string name, out int grade))
                return;

            Student newStudent = new Student
            {
                Name = name,
                Grade = grade
            };

            studentsList.Add(newStudent);
            ClearInputs();
            StatusTextBlock.Text = $"Добавен е: {name}";
        }

        private void StudentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StudentsListBox.SelectedItem is Student selectedStudent)
            {
                NameBox.Text = selectedStudent.Name;
                GradeBox.Text = selectedStudent.Grade.ToString();
                SaveEditButton.IsEnabled = true;
                StatusTextBlock.Text = "Редактирайте данните и натиснете 'Запази редакция'.";
            }
            else
            {
                SaveEditButton.IsEnabled = false;
            }
        }

        private void SaveEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsListBox.SelectedItem is Student selectedStudent)
            {
                if (!ValidateInputs(out string name, out int grade))
                    return;

                selectedStudent.Name = name;
                selectedStudent.Grade = grade;

                StudentsListBox.Items.Refresh();

                StatusTextBlock.Text = $"Успешно обновени данни за {name}!";
                StudentsListBox.SelectedItem = null;
                ClearInputs();
            }
        }

        private bool ValidateInputs(out string name, out int grade)
        {
            name = NameBox.Text.Trim();
            grade = 0;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(GradeBox.Text))
            {
                MessageBox.Show("Моля, попълнете и двете полета!", "Грешка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(GradeBox.Text.Trim(), out grade))
            {
                MessageBox.Show("Оценката трябва да бъде цяло число!", "Грешка", MessageBoxButton.OK, MessageBoxImage.Error);
                GradeBox.Focus();
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            NameBox.Clear();
            GradeBox.Clear();
            StudentsListBox.SelectedItem = null;
        }
    }
}