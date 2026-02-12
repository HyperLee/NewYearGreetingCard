using NewYearGreetingCard.Models;

using Xunit;

namespace NewYearGreetingCard.Tests.Unit.Models;

/// <summary>
/// GreetingCard 與 GreetingMessage 模型驗證測試。
/// </summary>
public sealed class GreetingCardTests
{
    [Fact]
    public void GreetingMessage_Properties_AreAssignedCorrectly()
    {
        GreetingMessage message = new(1, "馬到成功！", "馬年專屬");

        Assert.Equal(1, message.Id);
        Assert.Equal("馬到成功！", message.Text);
        Assert.Equal("馬年專屬", message.Category);
    }

    [Fact]
    public void GreetingMessage_RecordEquality_SameValues_AreEqual()
    {
        GreetingMessage message1 = new(1, "新年快樂！", "通用");
        GreetingMessage message2 = new(1, "新年快樂！", "通用");

        Assert.Equal(message1, message2);
        Assert.True(message1 == message2);
    }

    [Fact]
    public void GreetingMessage_RecordEquality_DifferentValues_AreNotEqual()
    {
        GreetingMessage message1 = new(1, "新年快樂！", "通用");
        GreetingMessage message2 = new(2, "萬事如意！", "通用");

        Assert.NotEqual(message1, message2);
        Assert.True(message1 != message2);
    }

    [Fact]
    public void GreetingCard_Properties_AreAssignedCorrectly()
    {
        List<GreetingMessage> messages = new()
        {
            new(1, "馬到成功！", "馬年專屬"),
            new(2, "新年快樂！", "通用")
        };

        GreetingCard card = new(
            Id: 1,
            StyleName: "寫實風",
            Style: CardStyle.Realistic,
            Description: "以寫實筆觸描繪駿馬。",
            CssClass: "card-style-realistic",
            Messages: messages);

        Assert.Equal(1, card.Id);
        Assert.Equal("寫實風", card.StyleName);
        Assert.Equal(CardStyle.Realistic, card.Style);
        Assert.Equal("以寫實筆觸描繪駿馬。", card.Description);
        Assert.Equal("card-style-realistic", card.CssClass);
        Assert.Equal(2, card.Messages.Count);
    }

    [Fact]
    public void GreetingCard_RecordEquality_SameValues_AreEqual()
    {
        IReadOnlyList<GreetingMessage> messages =
        [
            new GreetingMessage(1, "馬到成功！", "馬年專屬")
        ];

        GreetingCard card1 = new(1, "寫實風", CardStyle.Realistic, "描述", "card-style-realistic", messages);
        GreetingCard card2 = new(1, "寫實風", CardStyle.Realistic, "描述", "card-style-realistic", messages);

        Assert.Equal(card1, card2);
    }

    [Fact]
    public void GreetingCard_RecordEquality_DifferentId_AreNotEqual()
    {
        IReadOnlyList<GreetingMessage> messages =
        [
            new GreetingMessage(1, "馬到成功！", "馬年專屬")
        ];

        GreetingCard card1 = new(1, "寫實風", CardStyle.Realistic, "描述", "card-style-realistic", messages);
        GreetingCard card2 = new(2, "寫實風", CardStyle.Realistic, "描述", "card-style-realistic", messages);

        Assert.NotEqual(card1, card2);
    }

    [Fact]
    public void GreetingCard_RecordEquality_DifferentStyle_AreNotEqual()
    {
        IReadOnlyList<GreetingMessage> messages =
        [
            new GreetingMessage(1, "馬到成功！", "馬年專屬")
        ];

        GreetingCard card1 = new(1, "寫實風", CardStyle.Realistic, "描述", "card-style-realistic", messages);
        GreetingCard card2 = new(1, "可愛風", CardStyle.Cute, "描述", "card-style-cute", messages);

        Assert.NotEqual(card1, card2);
    }

    [Fact]
    public void CardStyle_HasExactly10Values()
    {
        CardStyle[] values = Enum.GetValues<CardStyle>();

        Assert.Equal(10, values.Length);
    }

    [Theory]
    [InlineData(CardStyle.Realistic)]
    [InlineData(CardStyle.Cute)]
    [InlineData(CardStyle.SciFi)]
    [InlineData(CardStyle.InkWash)]
    [InlineData(CardStyle.PaperCut)]
    [InlineData(CardStyle.Classical)]
    [InlineData(CardStyle.Minimalist)]
    [InlineData(CardStyle.FolkArt)]
    [InlineData(CardStyle.Illustration)]
    [InlineData(CardStyle.PopArt)]
    public void CardStyle_AllExpectedValues_Exist(CardStyle style)
    {
        Assert.True(Enum.IsDefined(style));
    }
}
