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
                        Alis = details["Alýþ"]?.ToString(),
                        Satis = details["Satýþ"]?.ToString(),
                        Fark = details["Deðiþim"]?.ToString(),
                        Yonu = (details["Deðiþim"]?.ToString()?.StartsWith("-") ?? false) ? "down" : "up"
                    });
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", $"Veriler yüklenirken bir hata oluþtu: {ex.Message}", "Tamam");
        }
    }


    private void OnRefreshClicked(object sender, EventArgs e)
    {
        LoadKurlar();
    }
}