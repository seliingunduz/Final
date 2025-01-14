namespace Final
{
    public partial class GirisVeKaydol : ContentPage
    {
        public GirisVeKaydol()
        {
            InitializeComponent();
        }

        private void KayitGirisEkrani(object sender, EventArgs e)
        {
            kayitekran.IsVisible = !kayitekran.IsVisible;
            loginekran.IsVisible = !loginekran.IsVisible;
        }


        private async void RegisterClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtREmail.Text) ||
                string.IsNullOrWhiteSpace(txtRPassword.Text))
            {
                await DisplayAlert("Hata", "T�m alanlar� doldurun.", "Tamam");
                return;
            }

            var (user, error) = await model.firebase.Register(txtName.Text, txtREmail.Text, txtRPassword.Text);
            if (user != null)
            {
                await DisplayAlert("Ba�ar�l�", "Kay�t ba�ar�l�.", "Tamam");
                await Navigation.PushAsync(new MainPage()); // Ba�ar�l� kay�t sonras� y�nlendirme
            }
            else
            {
                await DisplayAlert("Hata", $"Kay�t ba�ar�s�z: {error}", "Tamam");
            }
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(Password.Text))
            {
                await DisplayAlert("Hata", "E-posta ve �ifre alanlar�n� doldurun.", "Tamam");
                return;
            }

            var (user, error) = await model.firebase.Login(txtEmail.Text, Password.Text);
            if (user != null)
            {
                await DisplayAlert("Ba�ar�l�", "Giri� ba�ar�l�.", "Tamam");
                Application.Current.MainPage = new AppShell(); // Giri� sonras� y�nlendirme
            }
            else
            {
                await DisplayAlert("Hata", $"Giri� ba�ar�s�z: {error}", "Tamam");
            }
        }

    }
}
