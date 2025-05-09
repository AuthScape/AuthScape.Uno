using System;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Windows.Security.Authentication.Web;
using Windows.System;

namespace AuthScape.Uno.Client
{
    public class Authenticator
    {
        public static async Task Authenticate()
        {
            var uri = new Uri("https://10.0.2.2:44303/Identity/Account/Login?ReturnUrl=%2FIdentity%2FAccount%2FManage%2Findex");
            await Launcher.LaunchUriAsync(uri);
        }

        public async Task<string> AuthenticateAsync()
        {
            var codeVerifier = GenerateCodeVerifier();
            var codeChallenge = ComputeCodeChallenge(codeVerifier);

            var authUri = $"https://your-auth-server/connect/authorize?client_id=uno-app" +
                          $"&response_type=code&scope=openid profile" +
                          $"&redirect_uri=your-app://callback" +
                          $"&code_challenge={codeChallenge}&code_challenge_method=S256";

            var result = await WebAuthenticationBroker.AuthenticateAsync(
                WebAuthenticationOptions.None,
                new Uri(authUri),
                new Uri("your-app://callback"));

            return result.ResponseData;
        }

        public async Task<string> GetAccessTokenAsync(string authorizationCode, string codeVerifier)
        {
            using var client = new HttpClient();
            var tokenRequest = new Dictionary<string, string>
            {
                { "client_id", "uno-app" },
                { "grant_type", "authorization_code" },
                { "code", authorizationCode },
                { "redirect_uri", "your-app://callback" },
                { "code_verifier", codeVerifier }
            };

            var response = await client.PostAsync("https://your-auth-server/connect/token",
                new FormUrlEncodedContent(tokenRequest));

            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> CallApiAsync(string accessToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync("https://your-api-server/protected-endpoint");
            return await response.Content.ReadAsStringAsync();
        }

        public static string GenerateCodeVerifier()
        {
            using var rng = RandomNumberGenerator.Create();
            var bytes = new byte[32]; // PKCE recommends 32-128 bytes
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');
        }

        public static string ComputeCodeChallenge(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            return Convert.ToBase64String(hash)
                .Replace("+", "-")
                .Replace("/", "_")
                .TrimEnd('=');
        }
    }
}
