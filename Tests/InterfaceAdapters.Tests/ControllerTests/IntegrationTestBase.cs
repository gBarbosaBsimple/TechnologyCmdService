using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace InterfaceAdapters.Tests;   // 👉 usa o mesmo namespace que usas nos testes

/// <summary>
/// Classe base com utilitários para testes de integração.
/// Recebe um HttpClient criado pelo WebApplicationFactory.
/// </summary>
public abstract class IntegrationTestBase
{
    protected readonly HttpClient Client;

    protected IntegrationTestBase(HttpClient client)
    {
        Client = client;
    }

    /* Helpers de conveniência ― ficam disponíveis nas subclasses */

    protected Task<HttpResponseMessage> GetAsync(string url)
        => Client.GetAsync(url);

    protected Task<HttpResponseMessage> DeleteAsync(string url)
        => Client.DeleteAsync(url);

    protected Task<HttpResponseMessage> PostAsync<T>(string url, T body)
        => Client.PostAsJsonAsync(url, body);

    protected Task<HttpResponseMessage> PutAsync<T>(string url, T body)
        => Client.PutAsJsonAsync(url, body);
}
