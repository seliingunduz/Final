namespace Final;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new GirisVeKaydol());
        var savedTheme = Preferences.Get("SelectedTheme", "Light"); // Açık tema varsayılan
        ApplyTheme(savedTheme);

        
    }

    public void ApplyTheme(string themeKey)
    {
        if (themeKey == "Dark")
        {
            Resources["PrimaryTextColor"] = Resources["PrimaryTextColorDark"];
            Resources["SecondaryTextColor"] = Resources["SecondaryTextColorDark"];
            Resources["BackgroundColor"] = Resources["BackgroundColorDark"];
            Resources["CardBackgroundColor"] = Resources["CardBackgroundColorDark"];
            Resources["ButtonBackgroundColor"] = Resources["ButtonBackgroundColorDark"];
            Resources["EditButtonColor"] = Resources["EditButtonColorDark"];
            Resources["DeleteButtonColor"] = Resources["DeleteButtonColorDark"];
            Resources["ButtonTextColor"] = Resources["ButtonTextColorDark"];
            UserAppTheme = AppTheme.Dark;
        }
        else
        {
            Resources["PrimaryTextColor"] = Resources["PrimaryTextColor"];
            Resources["SecondaryTextColor"] = Resources["SecondaryTextColor"];
            Resources["BackgroundColor"] = Resources["BackgroundColor"];
            Resources["CardBackgroundColor"] = Resources["CardBackgroundColor"];
            Resources["ButtonBackgroundColor"] = Resources["ButtonBackgroundColor"];
            Resources["EditButtonColor"] = Resources["EditButtonColor"];
            Resources["DeleteButtonColor"] = Resources["DeleteButtonColor"];
            Resources["ButtonTextColor"] = Resources["ButtonTextColor"];
            UserAppTheme = AppTheme.Light;
        }

        Preferences.Set("SelectedTheme", themeKey); // Kullanıcı tercihini kaydet
    }
}
