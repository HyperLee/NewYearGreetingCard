using NewYearGreetingCard.Models;

namespace NewYearGreetingCard.Data;

/// <summary>
/// 賀卡與祝賀詞的靜態資料集合。
/// </summary>
public static class GreetingCardData
{
    /// <summary>
    /// 全站賀卡清單，包含 10 張賀卡與 50+ 句不重複祝賀詞。
    /// </summary>
    public static readonly IReadOnlyList<GreetingCard> Cards =
    [
        new GreetingCard(
            Id: 1,
            StyleName: "寫實風",
            Style: CardStyle.Realistic,
            Description: "以寫實筆觸描繪駿馬奔騰的姿態，搭配漸層與細緻光影。",
            CssClass: "card-style-realistic",
            Messages:
            [
                new GreetingMessage(1, "駿馬奔騰迎新歲，寫實風專屬祝福！", "寫實風專屬"),
                new GreetingMessage(2, "金輝映馬影，寫實風專屬喜迎春。", "寫實風專屬"),
                new GreetingMessage(3, "馬到成功，新年快樂！", "馬年專屬"),
                new GreetingMessage(4, "萬事如意，歲歲平安。", "通用"),
                new GreetingMessage(5, "事業騰達，步步高升。", "事業")
            ]),
        new GreetingCard(
            Id: 2,
            StyleName: "可愛風",
            Style: CardStyle.Cute,
            Description: "圓潤粉彩的小馬與溫暖點綴，呈現療癒可愛的節慶氛圍。",
            CssClass: "card-style-cute",
            Messages:
            [
                new GreetingMessage(6, "小馬萌萌來拜年，可愛風專屬幸福。", "可愛風專屬"),
                new GreetingMessage(7, "粉彩小馬送笑顏，可愛風專屬好運。", "可愛風專屬"),
                new GreetingMessage(8, "龍馬精神，福氣滿滿。", "馬年專屬"),
                new GreetingMessage(9, "闔家歡樂，笑口常開。", "家庭"),
                new GreetingMessage(10, "平安喜樂，心想事成。", "通用")
            ]),
        new GreetingCard(
            Id: 3,
            StyleName: "科幻風",
            Style: CardStyle.SciFi,
            Description: "霓虹線條勾勒未來天馬，融合深色背景與科技光效。",
            CssClass: "card-style-scifi",
            Messages:
            [
                new GreetingMessage(11, "霓虹天馬啟新紀，科幻風專屬祝賀。", "科幻風專屬"),
                new GreetingMessage(12, "星際駿馬穿雲過，科幻風專屬好運。", "科幻風專屬"),
                new GreetingMessage(13, "馬躍星河，前程似錦。", "馬年專屬"),
                new GreetingMessage(14, "夢想升空，勇往直前。", "事業"),
                new GreetingMessage(15, "新歲安康，福運常伴。", "健康")
            ]),
        new GreetingCard(
            Id: 4,
            StyleName: "水墨風",
            Style: CardStyle.InkWash,
            Description: "水墨留白營造靜謐意境，以淡雅筆勢描繪奔馬。",
            CssClass: "card-style-inkwash",
            Messages:
            [
                new GreetingMessage(16, "墨韻躍馬迎春至，水墨風專屬雅祝。", "水墨風專屬"),
                new GreetingMessage(17, "留白畫馬添詩意，水墨風專屬祥和。", "水墨風專屬"),
                new GreetingMessage(18, "馬踏瑞雪，福至心田。", "馬年專屬"),
                new GreetingMessage(19, "清風明月，歲歲平安。", "健康"),
                new GreetingMessage(20, "家和萬事興，春暖花開。", "家庭")
            ]),
        new GreetingCard(
            Id: 5,
            StyleName: "剪紙風",
            Style: CardStyle.PaperCut,
            Description: "紅底鏤空的剪紙馬圖案，搭配傳統節慶紋樣。",
            CssClass: "card-style-papercut",
            Messages:
            [
                new GreetingMessage(21, "剪紙紅韻映馬影，剪紙風專屬吉祥。", "剪紙風專屬"),
                new GreetingMessage(22, "鏤空馬躍迎新歲，剪紙風專屬歡慶。", "剪紙風專屬"),
                new GreetingMessage(23, "一馬當先，喜氣盈門。", "馬年專屬"),
                new GreetingMessage(24, "招財進寶，福氣滿堂。", "財運"),
                new GreetingMessage(25, "笑迎新春，萬事大吉。", "通用")
            ]),
        new GreetingCard(
            Id: 6,
            StyleName: "古典風",
            Style: CardStyle.Classical,
            Description: "書法與印章元素交織，呈現典雅莊重的賀年氛圍。",
            CssClass: "card-style-classical",
            Messages:
            [
                new GreetingMessage(26, "墨香古韻賀新年，古典風專屬祝願。", "古典風專屬"),
                new GreetingMessage(27, "書法奔馬添雅趣，古典風專屬祥瑞。", "古典風專屬"),
                new GreetingMessage(28, "馬到成功，學業進步。", "馬年專屬"),
                new GreetingMessage(29, "福壽康寧，四季安泰。", "健康"),
                new GreetingMessage(30, "家門興旺，世代昌隆。", "家庭")
            ]),
        new GreetingCard(
            Id: 7,
            StyleName: "極簡風",
            Style: CardStyle.Minimalist,
            Description: "幾何線條與大量留白，凸顯極簡美感與清爽視覺。",
            CssClass: "card-style-minimalist",
            Messages:
            [
                new GreetingMessage(31, "極簡線馬迎新春，極簡風專屬清新。", "極簡風專屬"),
                new GreetingMessage(32, "留白雅馬帶靜謐，極簡風專屬祝福。", "極簡風專屬"),
                new GreetingMessage(33, "馬躍新程，心想事成。", "馬年專屬"),
                new GreetingMessage(34, "簡而不凡，日日順心。", "通用"),
                new GreetingMessage(35, "創意無限，事業長虹。", "事業")
            ]),
        new GreetingCard(
            Id: 8,
            StyleName: "年畫風",
            Style: CardStyle.FolkArt,
            Description: "年畫色彩鮮豔飽滿，呈現熱鬧喜慶的傳統年節風格。",
            CssClass: "card-style-folkart",
            Messages:
            [
                new GreetingMessage(36, "年畫駿馬添喜氣，年畫風專屬祝賀。", "年畫風專屬"),
                new GreetingMessage(37, "滿版紅金迎春到，年畫風專屬好運。", "年畫風專屬"),
                new GreetingMessage(38, "馬年納福，吉星高照。", "馬年專屬"),
                new GreetingMessage(39, "財源廣進，喜迎豐收。", "財運"),
                new GreetingMessage(40, "團圓美滿，幸福常在。", "家庭")
            ]),
        new GreetingCard(
            Id: 9,
            StyleName: "插畫風",
            Style: CardStyle.Illustration,
            Description: "手繪線條與溫暖色調，描繪溫馨的馬年故事感。",
            CssClass: "card-style-illustration",
            Messages:
            [
                new GreetingMessage(41, "手繪小馬漫步春，插畫風專屬溫馨。", "插畫風專屬"),
                new GreetingMessage(42, "暖色筆觸畫新歲，插畫風專屬祝願。", "插畫風專屬"),
                new GreetingMessage(43, "馬躍晨光，未來可期。", "馬年專屬"),
                new GreetingMessage(44, "健康常伴，笑顏常開。", "健康"),
                new GreetingMessage(45, "友情長存，溫暖相隨。", "通用")
            ]),
        new GreetingCard(
            Id: 10,
            StyleName: "普普風",
            Style: CardStyle.PopArt,
            Description: "高對比撞色與波點元素，展現活力十足的普普藝術。",
            CssClass: "card-style-popart",
            Messages:
            [
                new GreetingMessage(46, "波點駿馬嗨新年，普普風專屬熱力。", "普普風專屬"),
                new GreetingMessage(47, "撞色馬影躍舞台，普普風專屬潮福。", "普普風專屬"),
                new GreetingMessage(48, "馬上發財，活力滿載。", "馬年專屬"),
                new GreetingMessage(49, "好運連連，精彩不斷。", "通用"),
                new GreetingMessage(50, "事業飛躍，前途無量。", "事業")
            ])
    ];
}
