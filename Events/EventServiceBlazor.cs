using System.Net.Http.Json;
using static Events.Pages.Event;

public class EventServiceBlazor
{
    private readonly HttpClient _http;

    public EventServiceBlazor(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<EventDto>> GetAllEvents()
    {
        return await _http.GetFromJsonAsync<List<EventDto>>("api/Event");
    }
}
