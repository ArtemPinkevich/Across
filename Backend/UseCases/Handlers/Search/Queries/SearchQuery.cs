using MediatR;
using UseCases.Handlers.Search.Dto;

namespace UseCases.Handlers.Search.Queries;

// Пока решено ограничится только парамертрами Откуда, Куда и когда,
// а фильтры применять на фронтенде,
// так как даже в ати.су Москва-Астана с радиусами 500км находится всего лишь 28 грузов
public class SearchQuery : IRequest<SearchResultDto>
{
    public string FromAddress { get; set; }
    public string ToAddress { get; set; }

    // Строка ISO формата:
    // на js это moment().toISOString(true)
    // на C# это DateTime.Now.ToString("O")
    public string LoadDate { set; get; }
}
