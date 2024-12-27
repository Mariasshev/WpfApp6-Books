using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp6
{
    public partial class MainWindow : Window
    {
        private List<Book> Books = new();

        public MainWindow()
        {
            InitializeComponent();
            DisplayBooks();
        }

        private void DisplayBooks()
        {
            BookListControl.ItemsSource = null;
            BookListControl.ItemsSource = Books;

            BookSelectionComboBox.ItemsSource = null;
            BookSelectionComboBox.ItemsSource = Books;
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            var title = TitleTextBox.Text.Trim();
            var author = AuthorTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                MessageBox.Show("Название и автор не могут быть пустыми!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var existingBook = Books.FirstOrDefault(b => b.Title == title && b.Author == author);
            if (existingBook != null)
            {
                existingBook.Count++;
            }
            else
            {
                Books.Add(new Book(title, author, 1));
            }

            DisplayBooks();
        }

        private void IssueBookButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = BookSelectionComboBox.SelectedItem as Book;
            if (selectedBook == null)
            {
                MessageBox.Show("Выберите книгу для выдачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (selectedBook.Count > 0)
            {
                selectedBook.Count--;
                DisplayBooks();
            }
            else
            {
                MessageBox.Show("Нет доступных экземпляров для выдачи!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = BookSelectionComboBox.SelectedItem as Book;
            if (selectedBook == null)
            {
                MessageBox.Show("Выберите книгу для возврата!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            selectedBook.Count++;
            DisplayBooks();
        }
    }

    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public int Count { get; set; }

        public Book(string title, string author, int count)
        {
            Title = title;
            Author = author;
            Count = count;
        }
    }
}
