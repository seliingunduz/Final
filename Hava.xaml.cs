using System.Collections.ObjectModel;

namespace Final;

public partial class Hava : ContentPage
{
    public ObservableCollection<SehirHavaDurumu> Sehirler { get; set; } = new ObservableCollection<SehirHavaDurumu>();

    public Hava()
    {
        InitializeComponent();
        CityCollectionView.ItemsSource = Sehirler;
    }

    // Þehir Ekleme Ýþlemi
    private async void OnAddCityClicked(object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("Þehir Ekle", "Þehir ismini girin:", "OK", "Cancel");
        if (!string.IsNullOrWhiteSpace(sehir))
        {
            // Þehir ismini uygun formata çevir
            sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
            sehir = sehir.Replace('Ç', 'C').Replace('Ð', 'G').Replace('Ý', 'I')
                          .Replace('Ö', 'O').Replace('Ü', 'U').Replace('Þ', 'S');

            // Yeni þehir ekle
            Sehirler.Add(new SehirHavaDurumu { Name = sehir });
        }
    }

    // Þehir Silme Ýþlemi
    private void OnDeleteCityClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is SehirHavaDurumu sehir)
        {
            Sehirler.Remove(sehir);
        }
    }
}

// Þehir Hava Durumu Modeli
public class SehirHavaDurumu
{
    public string Name { get; set; }
    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
}
