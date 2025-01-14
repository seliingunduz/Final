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

    // �ehir Ekleme ��lemi
    private async void OnAddCityClicked(object sender, EventArgs e)
    {
        string sehir = await DisplayPromptAsync("�ehir Ekle", "�ehir ismini girin:", "OK", "Cancel");
        if (!string.IsNullOrWhiteSpace(sehir))
        {
            // �ehir ismini uygun formata �evir
            sehir = sehir.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
            sehir = sehir.Replace('�', 'C').Replace('�', 'G').Replace('�', 'I')
                          .Replace('�', 'O').Replace('�', 'U').Replace('�', 'S');

            // Yeni �ehir ekle
            Sehirler.Add(new SehirHavaDurumu { Name = sehir });
        }
    }

    // �ehir Silme ��lemi
    private void OnDeleteCityClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is SehirHavaDurumu sehir)
        {
            Sehirler.Remove(sehir);
        }
    }
}

// �ehir Hava Durumu Modeli
public class SehirHavaDurumu
{
    public string Name { get; set; }
    public string Source => $"https://www.mgm.gov.tr/sunum/tahmin-klasik-5070.aspx?m={Name}&basla=1&bitir=5&rC=111&rZ=fff";
}
