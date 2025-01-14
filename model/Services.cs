using System.Net.Http;
using System.Threading.Tasks;

namespace Final
{
    public static class Services
    {
        public static async Task<string> GetAltinDovizGuncelKurlar()
        {
            HttpClient client = new HttpClient();
            string url = "https://finans.truncgil.com/today.json";
            using HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
