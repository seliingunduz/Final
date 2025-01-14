using System.Collections.ObjectModel;
using System.Net.Http;
using System.Xml.Linq;

namespace Final;

public partial class Haber : ContentPage
{
    public ObservableCollection<HaberItem> Haberler { get; set; } = new ObservableCollection<HaberItem>();

    public Haber()
    {
        InitializeComponent();
        NewsCollectionView.ItemsSource = Haberler;

        // Kategorileri Yükle
        foreach (var kategori in Kategori.liste)
        {
            var button = new Button
            {
                Text = kategori.Baslik,
                Style = (Style)Resources["CategoryButtonStyle"],
                CommandParameter = kategori
            };
            button.Clicked += OnCategorySelected;
            CategoryContainer.Children.Add(button);
        }
    }

    // Kategori Seçimi
    private async void OnCategorySelected(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Kategori kategori)
        {
            await LoadNews(kategori.Link);
        }
    }

    // Haberleri Yükleme
    private async Task LoadNews(string rssLink)
    {
        Haberler.Clear();
        try
        {
            using var client = new HttpClient();
            var response = await client.GetStringAsync(rssLink);

            var xdoc = XDocument.Parse(response);
            foreach (var item in xdoc.Descendants("item"))
            {
                Haberler.Add(new HaberItem
                {
                    Title = item.Element("title")?.Value,
                    Description = item.Element("description")?.Value,
                    PublishDate = DateTime.TryParse(item.Element("pubDate")?.Value, out var date) ? date : null,
                    Link = item.Element("link")?.Value
                });
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Haberler yüklenirken bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }

    // Haber Paylaþma
    private async void ShareClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is HaberItem haber)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = haber.Link,
                Title = haber.Title
            });
        }
    }

    // Ayrýntýlar Görüntüleme
    private async void ViewDetailsClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is HaberItem haber)
        {
            await Navigation.PushAsync(new Ayrýntý(haber.Link));
        }
    }


    // Haber Modeli
    public class HaberItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Link { get; set; }
    }

    // Kategori Modeli
    public class Kategori
    {
        public string Baslik { get; set; }
        public string Link { get; set; }

        public static List<Kategori> liste = new List<Kategori>
    {
        new Kategori { Baslik = "Manþet", Link = "https://www.trthaber.com/manset_articles.rss" },
        new Kategori { Baslik = "Son Dakika", Link = "https://www.trthaber.com/sondakika_articles.rss" },
        new Kategori { Baslik = "Gündem", Link = "https://www.trthaber.com/gundem_articles.rss" },
        new Kategori { Baslik = "Ekonomi", Link = "https://www.trthaber.com/ekonomi_articles.rss" },
        new Kategori { Baslik = "Spor", Link = "https://www.trthaber.com/spor_articles.rss" },
        new Kategori { Baslik = "Bilim Teknoloji", Link = "https://www.trthaber.com/bilim_teknoloji_articles.rss" },
        new Kategori { Baslik = "Güncel", Link = "https://www.trthaber.com/guncel_articles.rss" },
        new Kategori { Baslik = "Eðitim", Link = "https://www.trthaber.com/egitim_articles.rss" },
    };
    }
}