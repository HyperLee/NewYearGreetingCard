using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewYearGreetingCard.Pages;

/// <summary>
/// 首頁頁面模型，顯示年度資訊與賀卡入口。
/// </summary>
public class IndexModel : PageModel
{
    /// <summary>西元年份。</summary>
    public int Year => 2026;

    /// <summary>農曆年份名稱。</summary>
    public string LunarYearName => "丙午年";

    /// <summary>生肖年份名稱。</summary>
    public string ZodiacYearName => "生肖馬年";

    public void OnGet()
    {
    }
}
