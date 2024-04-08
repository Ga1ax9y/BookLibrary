using Project.Domain;
using Project.Domain.Entities;
using Project.Domain.Services;
namespace Project.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            bool flag = true;
            AuthManager authManager = new AuthManager();
            while (flag)
            {
                Console.Write("1. Регистрация\n2. Логин\nВыберите вариант: ");
                int log_choise = Convert.ToInt32(Console.ReadLine());
                


                //Выбор пользователя

                switch (log_choise)
                {
                    case 1:
                        {
                            Console.Write("Логин: ");
                            string login = Console.ReadLine();
                            Console.Write("Пароль: ");
                            string password = Console.ReadLine();

                            authManager.Registration(login, password);
                            break;

                        }
                    case 2:
                        {
                            while (true)
                            {
                                Console.Write("Логин: ");
                                string login;

                                login = Console.ReadLine();


                                Console.Write("Пароль: ");
                                string password = Console.ReadLine();

                                authManager.Login(login, password);
                                if (authManager.CurrentUser != null)
                                {
                                    break;
                                }
                                else Console.WriteLine("Пользователь не найден! Повторите попытку");
                            }
                            break;


                        }
                }
                //Выбор операции работы с библиотекой
                bool flag1 = true;
                while (flag1)
                {
                    UserService serv = new UserService(authManager);
                    BookService book_serv = new BookService(serv.GetUserCurrentBook());

                    string cbook_title = string.Empty;
                    if (serv.GetUserCurrentBook() != null)
                    {
                        cbook_title = serv.GetUserCurrentBook().Title;
                    }
                    Console.WriteLine("\n=======================");
                    Console.WriteLine($"Имя пользователя: {serv.GetCurrentUser().Login}\n" +
                        $"Выбранная книга: {cbook_title}");
                    Console.WriteLine("=======================");
                    Console.WriteLine("Операции с библиотекой:");
                    Console.WriteLine("1.Добавить книгу");
                    Console.WriteLine("2.Удалить книгу");
                    Console.WriteLine("3.Информация по книге по названию");
                    Console.WriteLine("4.Сортировать книги");
                    Console.WriteLine("5.Вывести все книги");
                    Console.WriteLine("6.Выбрать книгу");
                    Console.WriteLine("=======================");
                    Console.WriteLine("Операции с книгой:");
                    Console.WriteLine("7.Добавить закладку");
                    Console.WriteLine("8.Удалить закладку");
                    Console.WriteLine("9.Показать все закладки");
                    Console.WriteLine("=======================");
                    Console.WriteLine("10.Выйти из аккаунта");
                    Console.WriteLine("11. Выйти из программы");
                    Console.WriteLine("=======================");

                    Console.Write("Выберите варинат: ");
                    int operation_choise = Convert.ToInt32(Console.ReadLine());
                    LibraryService library_service = new LibraryService(serv.GetUserLibrary());
                    switch (operation_choise)
                    {
                        case 1:
                            {
                                Console.Write("Введите название книги: ");
                                string title = Console.ReadLine();
                                Console.Write("Введите автора книги: ");
                                string author = Console.ReadLine();
                                Console.Write("Введите путь к книге: ");
                                string path = Console.ReadLine();
                                library_service.AddBook(title, path, author);
                                break;
                            }
                        case 2:
                            {
                                if (serv.GetUserCurrentBook() != null)
                                    library_service.RemoveBook(serv.GetUserCurrentBook());

                                break;
                            }
                        case 3:
                            {
                                Console.Write("Введите название книги: ");
                                string User_title = Console.ReadLine();
                                Book book = library_service.FindBookByTitle(User_title);
                                Console.WriteLine($"Название книги: {book.Title}\n" +
                                    $"Автор: {book.Author}\nПуть к файлу: {book.Path}\n" +
                                    $"Дата добавления: {book.Date}");
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("1.Отсортировать по Дате\n2+. Отсортировать по названию ");
                                Console.Write("Выберите вариант");
                                int sort_choise = Convert.ToInt32(Console.ReadLine());
                                if (sort_choise == 1)
                                {
                                    library_service.SortByDate();
                                }
                                else
                                {
                                    library_service.SortByName();
                                }
                                break;
                            }
                        case 5:
                            {
                                int count = 1;
                                foreach (var book in serv.GetUserBookList())
                                {
                                    Console.WriteLine($"{count}.{book.Title}");
                                    count++;
                                }
                                break;
                            }
                        case 6:
                            {
                                int count = 1;
                                Console.WriteLine("Все имеющиеся книги:");
                                foreach (var book in serv.GetUserBookList())
                                {
                                    Console.WriteLine($"{count}.{book.Title}");
                                    count++;
                                }
                                Console.Write("Введите название выбранной книги");
                                string User_title = Console.ReadLine();
                                Book new_book = library_service.FindBookByTitle(User_title);
                                serv.SetCurrentBook(new_book);
                                break;

                            }
                        case 7:
                            {
                                Console.Write("Введите страницу на которой добавить закалдку: ");
                                int page = Convert.ToInt32(Console.ReadLine());
                                book_serv.AddBookMark(page);
                                Console.WriteLine("Добавлено!");
                                break;

                            }
                        case 8:
                            {
                                Console.Write("Введите страницу на какой удалить закалдку: ");
                                int page = Convert.ToInt32(Console.ReadLine());
                                book_serv.DeleteBookMark(page);
                                Console.WriteLine("Удалено!");
                                break;
                            }
                        case 9:
                            {
                                Console.WriteLine("Все закладки: ");
                                foreach (var page in book_serv.GetBookmarks())
                                {
                                    Console.WriteLine(page);
                                }
                                break;
                            }
                        case 10:
                            {

                                flag1 = false;
                                break;
                            }
                        case 11:
                            {
                                
                                flag = false;
                                flag1 = false;
                                break;
                            }

                    }

                }


            }

        }
        
    }
}
