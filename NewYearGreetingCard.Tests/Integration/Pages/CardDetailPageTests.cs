using System;
using System.Net;
using System.Text.RegularExpressions;

using NewYearGreetingCard.Tests.TestData;

using Xunit;

namespace NewYearGreetingCard.Tests.Integration.Pages;

/// <summary>
/// 賀卡詳情頁整合測試。
/// </summary>
public sealed class CardDetailPageTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CardDetailPageTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_Returns200(int cardId)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={cardId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_ContainsCardContent(int cardId)
    {
        HttpClient followClient = _client;

        HttpResponseMessage response = await followClient.GetAsync($"/Cards/Detail?id={cardId}");
        string content = await response.Content.ReadAsStringAsync();

        Assert.Contains("card-detail", content);
        Assert.Contains("card-style-", content);
        Assert.Contains("<svg", content);
    }

    [Fact]
    public async Task DetailPage_InvalidId_RedirectsToCards()
    {
        HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={GreetingCardTestData.InvalidCardId}");

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Contains("/Cards", response.Headers.Location?.ToString() ?? "");
    }

    [Fact]
    public async Task DetailPage_MissingId_RedirectsToCards()
    {
        HttpResponseMessage response = await _client.GetAsync("/Cards/Detail");

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Contains("/Cards", response.Headers.Location?.ToString() ?? "");
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_ContainsGreetingMessages(int cardId)
    {
        HttpClient followClient = _client;

        HttpResponseMessage response = await followClient.GetAsync($"/Cards/Detail?id={cardId}");
        string content = await response.Content.ReadAsStringAsync();

        // Check for at least 5 messages (using class expected in T021)
        int messageCount = System.Text.RegularExpressions.Regex.Matches(content, "greeting-message-text").Count;
        Assert.True(messageCount >= 5, $"Expected at least 5 messages for card {cardId}, found {messageCount}");

        // Verify horse-year themed greetings (checking for the character '馬' which appears in all horse-year messages/categories)
        Assert.Contains("馬", content);
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_ContainsCopyButtonsForMessages(int cardId)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={cardId}");
        string content = await response.Content.ReadAsStringAsync();

        int messageCount = System.Text.RegularExpressions.Regex.Matches(content, "greeting-message-text").Count;
        int copyButtonCount = System.Text.RegularExpressions.Regex.Matches(content, "greeting-copy-button").Count;
        int copyDataCount = System.Text.RegularExpressions.Regex.Matches(content, "data-copy-text=").Count;

        Assert.True(copyButtonCount == messageCount, $"Expected copy button per message for card {cardId}, found {copyButtonCount} buttons and {messageCount} messages.");
        Assert.True(copyDataCount == messageCount, $"Expected data-copy-text per message for card {cardId}, found {copyDataCount} attributes and {messageCount} messages.");
    }

    public static TheoryData<int> ValidCardIds
    {
        get
        {
            TheoryData<int> data = new();
            foreach (int id in GreetingCardTestData.ValidCardIds)
            {
                data.Add(id);
            }

            return data;
        }
    }

    // === T027: Navigation Integration Tests ===

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_RendersAllCardStyleLinksInNavigation(int cardId)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={cardId}");
        string content = await response.Content.ReadAsStringAsync();

        foreach (int id in GreetingCardTestData.ValidCardIds)
        {
            Assert.Contains($"/Cards/Detail?id={id}", content);
        }

        // Verify the navigation list container exists
        Assert.Contains("card-navigation-list", content);
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_HighlightsCurrentCardWithActiveClass(int cardId)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={cardId}");
        string content = await response.Content.ReadAsStringAsync();

        // Look for the pattern: active class on the link for this card ID
        string activePattern = $"card-nav-link active";
        Assert.Contains(activePattern, content);

        // Also verify only one link has active class in the card-navigation-list
        int activeCount = System.Text.RegularExpressions.Regex.Matches(content, @"card-nav-link\s+active").Count;
        Assert.Equal(1, activeCount);
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_ContainsPreviousAndNextButtons(int cardId)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={cardId}");
        string content = await response.Content.ReadAsStringAsync();

        Assert.Contains("card-nav-prev", content);
        Assert.Contains("card-nav-next", content);
        Assert.Contains("上一張", content);
        Assert.Contains("下一張", content);
    }

    // === T032: Visual Style Verification Tests ===

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_RendersCorrectCssClassAndSvg(int cardId)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={cardId}");
        string content = await response.Content.ReadAsStringAsync();

        // FR-010: Each card uses a unique, expected CssClass for its style.
        string expectedClass = GetExpectedCssClassForId(cardId);
        string detailClassPattern = $@"class=""card-detail\s+[^""]*{expectedClass}[^""]*""";
        Assert.True(Regex.IsMatch(content, detailClassPattern), $"Expected {expectedClass} on card-detail for card {cardId}.");

        // FR-002: Each card includes horse art + CNY elements rendered via SVG.
        Assert.Contains("<svg", content);
        Assert.Contains("class=\"card-svg\"", content);
        Assert.Contains("horse-figure", content);
        Assert.Contains("cny-element", content);
    }

    [Fact]
    public async Task AllCards_HaveUniqueCssClasses()
    {
        // FR-010: 10 distinct styles (unique CssClass across all cards).
        HashSet<string> classes = new(StringComparer.Ordinal);
        foreach (int id in GreetingCardTestData.ValidCardIds)
        {
            HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={id}");
            string content = await response.Content.ReadAsStringAsync();
            string expectedClass = GetExpectedCssClassForId(id);

            Assert.Contains(expectedClass, content);
            classes.Add(expectedClass);
        }

        Assert.Equal(10, classes.Count);
    }

    [Theory]
    [MemberData(nameof(ValidCardIds))]
    public async Task DetailPage_ValidId_UsesImageDominantLayout(int cardId)
    {
        HttpResponseMessage response = await _client.GetAsync($"/Cards/Detail?id={cardId}");
        string content = await response.Content.ReadAsStringAsync();

        // FR-009: Card art is dominant; SVG should appear before the textual info block.
        int artIndex = content.IndexOf("card-art", StringComparison.Ordinal);
        int svgIndex = content.IndexOf("card-svg", StringComparison.Ordinal);
        int infoIndex = content.IndexOf("card-info", StringComparison.Ordinal);

        Assert.True(artIndex >= 0 && infoIndex > artIndex, $"Card {cardId} should render card-art before card-info.");
        Assert.True(svgIndex > artIndex && svgIndex < infoIndex, $"Card {cardId} should render SVG inside the art block before text.");
    }

    private string GetExpectedCssClassForId(int id)
    {
        return id switch
        {
            1 => "card-style-realistic",
            2 => "card-style-cute",
            3 => "card-style-scifi",
            4 => "card-style-inkwash",
            5 => "card-style-papercut",
            6 => "card-style-classical",
            7 => "card-style-minimalist",
            8 => "card-style-folkart",
            9 => "card-style-illustration",
            10 => "card-style-popart",
            _ => throw new ArgumentException("Unknown ID")
        };
    }
}
