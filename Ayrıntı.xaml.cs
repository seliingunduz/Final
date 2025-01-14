namespace Final
{
    public partial class Ayrıntı : ContentPage
    {
        public Ayrıntı(string url)
        {
            InitializeComponent();

            // WebView'e URL'yi yükleme
            NewsWebView.Source = url;
        }
    }
}
