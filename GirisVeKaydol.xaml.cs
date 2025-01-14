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
                await DisplayAlert("Hata", "Tüm alanlarý doldurun.", "Tamam");
                return;
            }

            var (user, error) = await model.firebase.Register(txtName.Text, txtREmail.Text, txtRPassword.Text);
            if (user != null)
            {
                await DisplayAlert("Baþarýlý", "Kayýt baþarýlý.", "Tamam");
                await Navigation.PushAsync(new MainPage()); // Baþarýlý kayýt sonrasý yönlendirme
            }
            else
            {
                await DisplayAlert("Hata", $"Kayýt baþarýsýz: {error}", "Tamam");
            }
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(Password.Text))
            {
                await DisplayAlert("Hata", "E-posta ve þifre alanlarýný doldurun.", "Tamam");
                return;
            }

            var (user, error) = await model.firebase.Login(txtEmail.Text, Password.Text);
            if (user != null)
            {
                await DisplayAlert("Baþarýlý", "Giriþ baþarýlý.", "Tamam");
                Application.Current.MainPage = new AppShell(); // Giriþ sonrasý yönlendirme
            }
            else
            {
                await DisplayAlert("Hata", $"Giriþ baþarýsýz: {error}", "Tamam");
            }
        }

    }
}
