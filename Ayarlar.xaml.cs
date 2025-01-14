namespace Final;

public partial class Ayarlar : ContentPage
{
    public Ayarlar()
    {
        InitializeComponent();

        // Switch durumunu kaydedilen temaya göre ayarla
        DarkModeSwitch.IsToggled = Preferences.Get("SelectedTheme", "Light") == "Dark";
    }

    private void OnDarkModeToggled(object sender, ToggledEventArgs e)
    {
        var theme = e.Value ? "Dark" : "Light";
        (Application.Current as App)?.ApplyTheme(theme);
    }
}
