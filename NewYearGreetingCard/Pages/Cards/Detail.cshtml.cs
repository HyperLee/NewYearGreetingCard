using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using NewYearGreetingCard.Models;
using NewYearGreetingCard.Services;

namespace NewYearGreetingCard.Pages.Cards;

/// <summary>
/// 賀卡詳情頁面模型，支援 ?id={cardId} 參數，展示單張賀卡。
/// id 不存在時重導至 /Cards。
/// 提供前後頁導航（wrap-around：最後一張→第一張、第一張→最後一張）。
/// </summary>
public class DetailModel : PageModel
{
    private readonly IGreetingCardService _cardService;

    public DetailModel(IGreetingCardService cardService)
    {
        _cardService = cardService;
    }

    /// <summary>當前賀卡。</summary>
    public GreetingCard? Card { get; private set; }

    /// <summary>上一張賀卡的識別碼（wrap-around）。</summary>
    public int PreviousCardId { get; private set; }

    /// <summary>下一張賀卡的識別碼（wrap-around）。</summary>
    public int NextCardId { get; private set; }

    /// <summary>所有賀卡清單，供導航列使用。</summary>
    public IReadOnlyList<GreetingCard> AllCards { get; private set; } = [];

    public IActionResult OnGet(int? id)
    {
        if (id is null)
        {
            return RedirectToPage("/Cards/Index");
        }

        Card = _cardService.GetCardById(id.Value);

        if (Card is null)
        {
            return RedirectToPage("/Cards/Index");
        }

        AllCards = _cardService.GetAllCards();
        int currentIndex = -1;

        for (int i = 0; i < AllCards.Count; i++)
        {
            if (AllCards[i].Id == Card.Id)
            {
                currentIndex = i;
                break;
            }
        }

        // Wrap-around navigation: last → first, first → last
        int previousIndex = (currentIndex - 1 + AllCards.Count) % AllCards.Count;
        int nextIndex = (currentIndex + 1) % AllCards.Count;
        PreviousCardId = AllCards[previousIndex].Id;
        NextCardId = AllCards[nextIndex].Id;

        return Page();
    }
}
