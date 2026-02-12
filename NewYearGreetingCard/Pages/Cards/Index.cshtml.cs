using Microsoft.AspNetCore.Mvc.RazorPages;

using NewYearGreetingCard.Models;
using NewYearGreetingCard.Services;

namespace NewYearGreetingCard.Pages.Cards;

/// <summary>
/// 賀卡總覽頁面模型，顯示所有賀卡風格名稱清單與預覽連結。
/// </summary>
public class IndexModel : PageModel
{
    private readonly IGreetingCardService _cardService;

    public IndexModel(IGreetingCardService cardService)
    {
        _cardService = cardService;
    }

    /// <summary>所有賀卡清單。</summary>
    public IReadOnlyList<GreetingCard> Cards { get; private set; } = [];

    public void OnGet()
    {
        Cards = _cardService.GetAllCards();
    }
}
