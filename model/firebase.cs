using Firebase.Auth;
using Firebase.Auth.Providers;

namespace Final.model

{
    class firebase
    {
        static string project_id = "final-a773b";
        static string Apikey = "AIzaSyDdxvNwal7-iPvmrOuhcuEDL8h01N-_OtE";
        static string AuthDomain = $"{project_id}.firebaseapp.com";
        static FirebaseAuthConfig config = new FirebaseAuthConfig()
        {
            ApiKey = Apikey,
            AuthDomain = AuthDomain,
            Providers = new[] { new EmailProvider() }
        };
        public static async Task<(User User, string Error)> Login(string email, string pass)
        {
            try
            {
                var client = new FirebaseAuthClient(config);
                var res = await client.SignInWithEmailAndPasswordAsync(email, pass);
                return (res.User, null);
            }
            catch (FirebaseAuthException ex)
            {
                return (null, ex.Reason.ToString()); // Hata türünü döndür
            }
            catch (Exception ex)
            {
                return (null, ex.Message); // Genel hata mesajını döndür
            }
        }

        public static async Task<(User User, string Error)> Register(string dispname, string email, string pass)
        {
            try
            {
                var client = new FirebaseAuthClient(config);
                var res = await client.CreateUserWithEmailAndPasswordAsync(email, pass, dispname);
                return (res.User, null);
            }
            catch (FirebaseAuthException ex)
            {
                return (null, ex.Reason.ToString()); // Hata türünü döndür
            }
            catch (Exception ex)
            {
                return (null, ex.Message); // Genel hata mesajını döndür
            }
        }

    }
}