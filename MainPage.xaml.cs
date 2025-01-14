
namespace Final
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }


      

        private async void OnNavigateToHaberler(object sender, EventArgs e) //butona tıklanınca renk seçm sayfasına yönlendirmek için
        {
            await Navigation.PushAsync(new Haber());
        }

        private async void OnNavigateToHavaDurumu(object sender, EventArgs e) //butona tıklanınca yapılacaklar sayfasına yönlendirmek için
        {
            await Navigation.PushAsync(new Hava());
        }
        private async void OnNavigateToYapilacaklar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Yapılacaklar());
        }

        private async void OnNavigateToAyarlar(object sender, EventArgs e) //butona tıklanınca yapılacaklar sayfasına yönlendirmek için
        {
            await Navigation.PushAsync(new Ayarlar());
        }

    }
}




