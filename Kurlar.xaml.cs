using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace Final;

public partial class Kurlar : ContentPage
{
    public ObservableCollection<KurItem> KurlarListesi { get; set; } = new ObservableCollection<KurItem>();

    public Kurlar()
    {
        InitializeComponent();
        KurlarCollectionView.ItemsSource = KurlarListesi;
        LoadKurlar();
    }

    private async void LoadKurlar()
    {
        try
        {
            string jsonData = await Services.GetAltinDovizGuncelKurlar();
            JObject parsedData = JObject.Parse(jsonData);

            KurlarListesi.Clear();

            foreach (var item in parsedData)
            {
                if (item.Value is JObject details)
                {
                    KurlarListesi.Add(new KurItem
                    {
                        Tur = item.Key,
                        Alis = details["Al��"]?.ToString(),
                        Satis = details["Sat��"]?.ToString(),
                        Fark = details["De�i�im"]?.ToString(),
                        Yonu = (details["De�i�im"]?.ToString()?.StartsWith("-") ?? false) ? "down" : "up"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Veriler y�klenirken bir hata olu�tu: {ex.Message}", "Tamam");
        }
    }


    private void OnRefreshClicked(object sender, EventArgs e)
    {
        LoadKurlar();
    }
}