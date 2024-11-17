using System.Collections.ObjectModel;
using AppParsers;

namespace YourNamespace
{
    public partial class MainPage : ContentPage
    {
        private string filePath = string.Empty;
        private readonly string xslPath = "/Users/katerina/Desktop/uni/oop/lab2/lab2/transform.xsl";
        private readonly string outputHtmlPath = "/Users/katerina/Desktop/uni/oop/MauiApp2/MauiApp2/output.html";

        private ObservableCollection<Scientist> _allScientists = new ObservableCollection<Scientist>();
        private ObservableCollection<Scientist> _filteredScientists = new ObservableCollection<Scientist>();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOpenXmlFileClicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.Default.PickAsync();
                if (result != null)
                {
                    // Перевірка розширення файлу
                    if (Path.GetExtension(result.FullPath).ToLower() != ".xml")
                    {
                        await DisplayAlert("Помилка", "Вибраний файл не є XML файлом.", "OK");
                        return;
                    }

                    filePath = result.FullPath;
                    await DisplayAlert("Успіх", $"Файл XML відкрито: {filePath}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Помилка", $"Не вдалося відкрити файл: {ex.Message}", "OK");
            }
        }

        private void OnSaxParseClicked(object sender, EventArgs e) => ParseFileIfExists(new SaxParserStrategy());
        private void OnDomParseClicked(object sender, EventArgs e) => ParseFileIfExists(new DomParserStrategy());
        private void OnLinqParseClicked(object sender, EventArgs e) => ParseFileIfExists(new LinqParserStrategy());
        
        private void ParseFileIfExists(IXmlParserStrategy parser)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                DisplayAlert("Помилка", "Будь ласка, виберіть XML файл перед парсингом.", "OK");
                return; 
            }

            DisplayResults(parser);
        }

        private void DisplayResults(IXmlParserStrategy parser)
        {
            try
            {
                var results = parser.Parse(filePath);
                _allScientists = new ObservableCollection<Scientist>(results);
                resultListView.ItemsSource = _allScientists;
                DisplayAlert("Успіх", "Парсинг успішно завершено!", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Помилка", $"Не вдалося виконати парсинг: {ex.Message}", "OK");
            }
        }

        private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string query = e.NewTextValue?.ToLower() ?? string.Empty;

            if (string.IsNullOrEmpty(query))
            {
                resultListView.ItemsSource = _allScientists;
                _filteredScientists = _allScientists;
                return;
            }

            var searchTerms = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            _filteredScientists = new ObservableCollection<Scientist>(
                _allScientists.Where(s =>
                    searchTerms.All(term =>
                        (s.FullName?.ToLower().Contains(term) ?? false) ||
                        (s.Faculty?.ToLower().Contains(term) ?? false) ||
                        (s.Department?.ToLower().Contains(term) ?? false) ||
                        (s.Degree?.ToLower().Contains(term) ?? false) ||
                        (s.Rank?.ToLower().Contains(term) ?? false)
                    )
                )
            );

            resultListView.ItemsSource = _filteredScientists;
        }


        private void OnTransformToHtmlClicked(object sender, EventArgs e)
        {
            try
            {
                if (!_filteredScientists.Any())
                {
                    DisplayAlert("Помилка", "Немає результатів для трансформації", "OK");
                    return;
                }

                var transformer = new XmlToHtmlTransformer();
                transformer.TransformFilteredResults(_filteredScientists, xslPath, outputHtmlPath);

                DisplayAlert("Успіх", $"HTML-файл успішно створено за адресою:\n{outputHtmlPath}", "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Помилка", $"Не вдалося виконати трансформацію: {ex.Message}", "OK");
            }
        }

        private void OnClearClicked(object sender, EventArgs e)
        {
            searchEntry.Text = string.Empty;
            resultListView.ItemsSource = null;
        }

        private async void OnExitClicked(object sender, EventArgs e)
        {
            bool confirmExit = await DisplayAlert("Підтвердження", "Ви дійсно хочете вийти?", "Так", "Ні");
            if (confirmExit)
            {
                Environment.Exit(0);
            }
        }
    }
}
