# Tasks: 農曆馬年賀卡網站

**Input**: Design documents from `/specs/001-lunar-greeting-cards/`
**Prerequisites**: plan.md (required), spec.md (required), research.md, data-model.md, contracts/page-routes.md, quickstart.md

**Tests**: 本專案依 plan.md 憲章檢查採用 TDD（xUnit + Moq + WebApplicationFactory），包含測試任務。

**Organization**: 任務依 User Story 分組，支援獨立實作與測試。

## Format: `[ID] [P?] [Story] Description`

- **[P]**: 可平行執行（不同檔案、無相依性）
- **[Story]**: 任務所屬的 User Story（如 US1、US2、US3）
- 描述中包含精確檔案路徑

---

## Phase 1: Setup（專案初始化）

**Purpose**: 建立缺少的專案目錄結構與測試專案

- [X] T001 建立專案目錄結構 Models/、Data/、Services/、Pages/Cards/ 於 NewYearGreetingCard/ 下，以及 wwwroot/css/cards/ 目錄
- [X] T002 建立測試專案 NewYearGreetingCard.Tests/NewYearGreetingCard.Tests.csproj，加入 xUnit、Moq、Microsoft.AspNetCore.Mvc.Testing 相依套件，並建立 Unit/Models/、Unit/Services/、Integration/Pages/、TestData/ 目錄結構
- [X] T003 [P] 更新 NewYearGreetingCard/Pages/_ViewImports.cshtml 加入專案命名空間與 Models 參考，並在 NewYearGreetingCard/Pages/Shared/_Layout.cshtml 中新增 wwwroot/css/cards/card-styles.css 樣式表參考

---

## Phase 2: Foundational（基礎建設）

**Purpose**: 所有 User Story 共用的核心模型、服務、資料與測試基礎設施。此階段完成前不可開始任何 User Story。

**⚠️ CRITICAL**: 所有 User Story 均依賴此階段完成。

- [X] T004 [P] 建立 CardStyle 列舉（10 種風格：Realistic、Cute、SciFi、InkWash、PaperCut、Classical、Minimalist、FolkArt、Illustration、PopArt）於 NewYearGreetingCard/Models/CardStyle.cs，含 XML 文件註解
- [X] T005 [P] 建立 GreetingMessage record（Id、Text、Category 屬性）於 NewYearGreetingCard/Models/GreetingMessage.cs，含 XML 文件註解與 <example> 區段
- [X] T006 [P] 建立 GreetingCard record（Id、StyleName、Style、Description、CssClass、Messages 屬性）於 NewYearGreetingCard/Models/GreetingCard.cs，含 XML 文件註解與 <example> 區段
- [X] T007 建立 IGreetingCardService 介面（GetAllCards、GetCardById、GetMessagesByCardId 方法）於 NewYearGreetingCard/Services/IGreetingCardService.cs，含 XML 文件註解
- [X] T008 建立 GreetingCardService 實作（注入 ILogger，從 GreetingCardData 讀取資料）於 NewYearGreetingCard/Services/GreetingCardService.cs，含 XML 文件註解
- [X] T009 建立 GreetingCardData 靜態集合（10 張賀卡、50+ 句不重複祝賀詞，每張賀卡至少 5 句祝賀詞且至少 2 句專屬）於 NewYearGreetingCard/Data/GreetingCardData.cs，含 XML 文件註解
- [X] T010 在 NewYearGreetingCard/Program.cs 中註冊 IGreetingCardService 為 Singleton 至 DI 容器
- [X] T011 [P] 建立測試基礎設施：TestWebApplicationFactory 於 NewYearGreetingCard.Tests/Integration/TestWebApplicationFactory.cs，GreetingCardTestData 於 NewYearGreetingCard.Tests/TestData/GreetingCardTestData.cs

**Checkpoint**: 基礎建設完成 — 可開始 User Story 實作

---

## Phase 3: User Story 1 — 瀏覽賀卡集 (Priority: P1) 🎯 MVP

**Goal**: 使用者可瀏覽至少 10 張不同風格的馬年賀卡，每張賀卡展示馬主題圖案與農曆新年元素。

**Independent Test**: 開啟網站首頁 → 進入賀卡總覽 → 點擊任一賀卡 → 確認顯示馬年主題圖案與 CNY 元素 → 逐一瀏覽 10 張賀卡。

### Tests for User Story 1

> **先寫測試、確認失敗，再進行實作**

- [X] T012 [P] [US1] 撰寫 GreetingCardService 單元測試（GetAllCards 回傳 10+ 張、GetCardById 正確/不存在、GetMessagesByCardId 回傳 5+ 句）於 NewYearGreetingCard.Tests/Unit/Services/GreetingCardServiceTests.cs
- [X] T013 [P] [US1] 撰寫 GreetingCard 與 GreetingMessage 模型驗證測試（屬性驗證、record 相等性）於 NewYearGreetingCard.Tests/Unit/Models/GreetingCardTests.cs

### Implementation for User Story 1

- [X] T014 [US1] 更新首頁 Pages/Index.cshtml 與 Pages/Index.cshtml.cs，顯示年度資訊（西元 2026 年、農曆丙午年、生肖馬年）及進入賀卡總覽的入口連結（FR-011）
- [X] T015 [P] [US1] 建立賀卡總覽頁 Pages/Cards/Index.cshtml 與 Pages/Cards/Index.cshtml.cs，顯示所有賀卡風格名稱清單與預覽連結
- [X] T016 [P] [US1] 建立賀卡詳情頁 Pages/Cards/Detail.cshtml 與 Pages/Cards/Detail.cshtml.cs，支援 ?id={cardId} 參數，展示單張賀卡 CSS/SVG 藝術與基本版面配置，id 不存在時重導至 /Cards
- [X] T017 [US1] 設定農曆新年主題 CSS 變數（--cny-red、--cny-gold 等）與全域基礎樣式於 NewYearGreetingCard/wwwroot/css/site.css
- [X] T018 [US1] 建立 10 種賀卡風格的基礎 CSS（.card-style-realistic 至 .card-style-popart）與基礎 SVG 馬年圖案於 NewYearGreetingCard/wwwroot/css/cards/card-styles.css
- [X] T019 [US1] 撰寫賀卡詳情頁整合測試（頁面回應 200、包含賀卡內容、無效 id 重導）於 NewYearGreetingCard.Tests/Integration/Pages/CardDetailPageTests.cs

**Checkpoint**: User Story 1 完成 — 使用者可瀏覽 10 張賀卡，每張有基礎 CSS/SVG 視覺呈現

---

## Phase 4: User Story 2 — 瀏覽祝賀詞 (Priority: P1)

**Goal**: 每張賀卡頁面顯示至少 5 句搭配的祝賀詞，內容涵蓋通用祝福語與馬年主題祝賀詞。

**Independent Test**: 開啟任一賀卡詳情頁 → 確認祝賀詞列表已顯示 → 驗證至少 5 句 → 切換至不同賀卡確認祝賀詞有差異。

### Tests for User Story 2

- [X] T020 [P] [US2] 撰寫整合測試驗證賀卡詳情頁顯示祝賀詞（每張至少 5 句、包含馬年主題祝賀詞）於 NewYearGreetingCard.Tests/Integration/Pages/CardDetailPageTests.cs

### Implementation for User Story 2

- [X] T021 [US2] 建立 _GreetingMessages.cshtml Partial View（祝賀詞卡片式列表，顯示每句祝賀詞文字與分類標籤）於 NewYearGreetingCard/Pages/Shared/_GreetingMessages.cshtml
- [X] T022 [US2] 將 _GreetingMessages Partial View 整合至賀卡詳情頁，傳入當前賀卡的 Messages 屬性於 NewYearGreetingCard/Pages/Cards/Detail.cshtml

**Checkpoint**: User Story 1 + 2 完成 — 使用者可瀏覽賀卡並查看搭配的祝賀詞

---

## Phase 5: User Story 3 — 複製祝賀詞 (Priority: P2)

**Goal**: 使用者點擊祝賀詞即可自動複製至剪貼簿，複製成功後顯示「已複製！」視覺回饋。

**Independent Test**: 在賀卡頁面點擊任一句祝賀詞 → 確認剪貼簿已複製該文字 → 頁面顯示「已複製！」提示 → 數秒後提示消失。

### Tests for User Story 3

> **先寫測試、確認失敗，再進行實作**

- [X] T023 [P] [US3] 撰寫整合測試驗證賀卡詳情頁包含複製按鈕 HTML 結構（每句祝賀詞旁有 data-copy-text 屬性與複製按鈕）於 NewYearGreetingCard.Tests/Integration/Pages/CardDetailPageTests.cs

### Implementation for User Story 3

- [X] T024 [US3] 實作剪貼簿複製函式 copyToClipboard（優先 navigator.clipboard.writeText，備援 execCommand + 「請按 Ctrl+C 複製」提示，處理快速連續點擊情境確保每次複製顯示最新回饋訊息）於 NewYearGreetingCard/wwwroot/js/site.js
- [X] T025 [US3] 為 _GreetingMessages.cshtml 中每句祝賀詞加入複製按鈕與點擊事件綁定，顯示「已複製！」視覺回饋提示於 NewYearGreetingCard/Pages/Shared/_GreetingMessages.cshtml
- [X] T026 [US3] 新增複製回饋提示的 CSS 樣式（toast/tooltip 動畫、自動消失效果）於 NewYearGreetingCard/wwwroot/css/site.css

**Checkpoint**: User Story 1 + 2 + 3 完成 — 使用者可瀏覽賀卡、查看祝賀詞並一鍵複製

---

## Phase 6: User Story 4 — 賀卡導航體驗 (Priority: P2)

**Goal**: 使用者透過頂部導航列在 10+ 張賀卡間便捷切換，導航介面簡潔直覺。

**Independent Test**: 在任一賀卡頁面 → 確認導航列列出所有賀卡風格名稱 → 點擊任一導航項目跳轉成功 → 當前賀卡高亮 → 使用前後頁按鈕切換。

### Tests for User Story 4

> **先寫測試、確認失敗，再進行實作**

- [X] T027 [P] [US4] 撰寫整合測試驗證導航列功能（所有賀卡風格名稱連結均渲染、當前賀卡高亮 active class、前後頁按鈕存在）於 NewYearGreetingCard.Tests/Integration/Pages/CardDetailPageTests.cs

### Implementation for User Story 4

- [X] T028 [US4] 建立 _CardNavigation.cshtml Partial View（顯示所有賀卡風格名稱連結，支援當前賀卡高亮 active class）於 NewYearGreetingCard/Pages/Shared/_CardNavigation.cshtml
- [X] T029 [US4] 將 _CardNavigation 整合至 _Layout.cshtml 頂部導航列，以下拉選單或水平連結列表呈現，確保導航列使用伺服器端渲染不依賴 JavaScript 初始化即可運作於 NewYearGreetingCard/Pages/Shared/_Layout.cshtml
- [X] T030 [US4] 在賀卡詳情頁新增「上一張」與「下一張」導航按鈕（最後一張可回到第一張）於 NewYearGreetingCard/Pages/Cards/Detail.cshtml
- [X] T031 [US4] 設定導航列 active 狀態樣式與賀卡導航佈局 CSS 於 NewYearGreetingCard/Pages/Shared/_Layout.cshtml.css

**Checkpoint**: User Story 1 + 2 + 3 + 4 完成 — 完整的賀卡瀏覽、祝賀詞顯示、複製與導航功能

---

## Phase 7: User Story 5 — 賀卡視覺美感體驗 (Priority: P3)

**Goal**: 10 張賀卡呈現明顯不同的藝術風格（寫實、可愛、科幻、水墨、剪紙、古典、極簡、年畫、插畫、普普），每張以馬為主角搭配農曆新年傳統元素，圖案為主、文字為輔。

**Independent Test**: 逐一瀏覽 10 張賀卡 → 確認每張風格明顯不同 → 每張包含馬的意象 → 每張包含紅色/金色/燈籠/春聯等 CNY 元素 → 圖案佔主要面積。

### Tests for User Story 5

> **先寫測試、確認失敗，再進行實作**

- [X] T032 [P] [US5] 撰寫整合測試驗證每張賀卡套用對應的 CssClass、包含 SVG 元素、且 10 張賀卡的 CssClass 均不重複於 NewYearGreetingCard.Tests/Integration/Pages/CardDetailPageTests.cs

### Implementation for User Story 5

- [X] T033 [US5] 強化 Realistic 與 Cute 風格的 CSS/SVG 藝術（寫實漸層陰影馬、可愛圓潤粉彩馬）與 CNY 傳統元素於 NewYearGreetingCard/wwwroot/css/cards/card-styles.css
- [X] T034 [US5] 強化 SciFi 與 InkWash 風格的 CSS/SVG 藝術（霓虹未來感馬、水墨留白馬）與 CNY 傳統元素於 NewYearGreetingCard/wwwroot/css/cards/card-styles.css
- [X] T035 [US5] 強化 PaperCut 與 Classical 風格的 CSS/SVG 藝術（紅底鏤空馬、書法印章馬）與 CNY 傳統元素於 NewYearGreetingCard/wwwroot/css/cards/card-styles.css
- [X] T036 [US5] 強化 Minimalist、FolkArt、Illustration、PopArt 四種風格的 CSS/SVG 藝術與 CNY 傳統元素於 NewYearGreetingCard/wwwroot/css/cards/card-styles.css
- [X] T037 [US5] 完善所有 10 張賀卡在 Detail.cshtml 中的內嵌 SVG 馬年圖案，確保每張賀卡圖案為主、文字為輔的版面配置於 NewYearGreetingCard/Pages/Cards/Detail.cshtml
- [X] T038 [US5] 視覺品質驗證：確認 10 張賀卡涵蓋至少 5 種以上不同藝術風格（FR-010）、每張包含馬主題圖案與 CNY 元素（FR-002）、以圖案為主設計（FR-009）

**Checkpoint**: 所有 5 個 User Story 完成 — 全功能農曆馬年賀卡網站

---

## Phase 8: Polish & Cross-Cutting Concerns

**Purpose**: 跨 User Story 的品質改善與最終驗證

- [X] T039 [P] 程式碼清理與格式化：確保符合 .editorconfig 規範、PascalCase 命名、檔案範圍命名空間
- [X] T040 依 quickstart.md 執行完整驗證（dotnet build、dotnet run、dotnet test 全部通過）
- [X] T041 效能驗證：確認 FCP < 1.5 秒、LCP < 2.5 秒（plan.md 效能目標）

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: 無相依性 — 可立即開始
- **Foundational (Phase 2)**: 依賴 Setup 完成 — **阻塞所有 User Story**
- **User Story 1 (Phase 3)**: 依賴 Foundational 完成 — 可獨立完成與測試
- **User Story 2 (Phase 4)**: 依賴 Foundational 完成 — 需 US1 的 Detail.cshtml 頁面結構
- **User Story 3 (Phase 5)**: 依賴 US2 完成（需要 _GreetingMessages partial 來加入複製按鈕）
- **User Story 4 (Phase 6)**: 依賴 Foundational 完成 — 可與 US2/US3 平行進行
- **User Story 5 (Phase 7)**: 依賴 US1 完成（需要基礎 CSS/SVG 結構來強化）
- **Polish (Phase 8)**: 依賴所有 User Story 完成

### User Story Dependencies

- **US1 (P1)**: Foundational 完成後即可開始 — 無其他 Story 相依性
- **US2 (P1)**: 需 US1 的 Detail.cshtml 頁面 — 但可在 US1 的 T016 完成後開始
- **US3 (P2)**: 需 US2 的 _GreetingMessages partial — 須在 US2 完成後開始
- **US4 (P2)**: 需 _Layout.cshtml 與頁面結構 — 可與 US2/US3 平行進行
- **US5 (P3)**: 需 US1 的基礎 CSS/SVG — 可在 US1 完成後開始，與 US3/US4 平行

### Within Each User Story

- 測試先寫並確認失敗，再進行實作
- Models → Services → Data（已在 Foundational 完成）
- Pages/UI → CSS/SVG 藝術
- 核心實作 → 整合測試
- Story 完成後再進入下一個優先級

### Parallel Opportunities

- Setup 中 T003 可與 T001、T002 平行
- Foundational 中 T004/T005/T006 可平行（不同檔案）；T011 可與 T007-T010 平行
- US1 中 T012/T013 可平行（不同測試檔案）；T015/T016 可平行（不同頁面檔案）
- US2 中 T020 可與 T012/T013 平行（不同測試檔案）
- US3 中 T023 可與 US1/US2 測試任務平行（不同測試檔案）
- US4 中 T027 可與 US2/US3 平行進行（不同檔案群組）
- US5 中 T032 可與 US3/US4 平行進行（不同檔案群組）
- US4 可與 US2/US3 平行進行（不同檔案群組）
- US5 可與 US3/US4 平行進行（不同檔案群組）

---

## Parallel Example: User Story 1

```bash
# 先啟動所有測試任務（平行）：
Task T012: "撰寫 GreetingCardService 單元測試" in Tests/Unit/Services/GreetingCardServiceTests.cs
Task T013: "撰寫 GreetingCard 模型驗證測試" in Tests/Unit/Models/GreetingCardTests.cs

# 確認測試失敗後，啟動頁面實作（平行）：
Task T015: "建立賀卡總覽頁" in Pages/Cards/Index.cshtml
Task T016: "建立賀卡詳情頁" in Pages/Cards/Detail.cshtml

# 頁面完成後，依序完成：
Task T014: "更新首頁" in Pages/Index.cshtml
Task T017: "設定 CNY 主題 CSS" in wwwroot/css/site.css
Task T018: "建立 10 種賀卡基礎 CSS/SVG" in wwwroot/css/cards/card-styles.css
Task T019: "撰寫整合測試" in Tests/Integration/Pages/CardDetailPageTests.cs
```

---

## Implementation Strategy

### MVP First（僅 User Story 1）

1. 完成 Phase 1: Setup
2. 完成 Phase 2: Foundational（**CRITICAL — 阻塞所有 Story**）
3. 完成 Phase 3: User Story 1
4. **STOP and VALIDATE**: 獨立測試 US1 — 瀏覽 10 張基礎賀卡
5. 可部署 / 展示 MVP

### Incremental Delivery

1. Setup + Foundational → 基礎就緒
2. 加入 US1 → 獨立測試 → 部署/展示（**MVP！**）
3. 加入 US2 → 獨立測試 → 賀卡搭配祝賀詞
4. 加入 US3 → 獨立測試 → 一鍵複製祝賀詞
5. 加入 US4 → 獨立測試 → 完整導航體驗
6. 加入 US5 → 獨立測試 → 精緻視覺美感
7. 每個 Story 獨立增值，不影響前序 Story

### Parallel Team Strategy

多人協作：

1. 團隊共同完成 Setup + Foundational
2. Foundational 完成後：
   - Developer A: US1（賀卡頁面 + 基礎 CSS/SVG）
   - Developer B: US4（導航 — Foundational 完成即可開始）
3. US1 完成後：
   - Developer A: US2 + US3（祝賀詞 + 複製）
   - Developer B: US5（視覺強化）
4. 所有 Story 獨立完成並整合

---

## Notes

- [P] 任務 = 不同檔案、無相依性，可平行執行
- [Story] 標籤對應 spec.md 中的 User Story 編號
- 每個 User Story 可獨立完成與測試
- 測試先寫、確認失敗再實作
- 每完成一個任務或邏輯群組即 commit
- 在每個 Checkpoint 停下來獨立驗證 Story
- 避免：模糊任務、同檔案衝突、破壞獨立性的跨 Story 相依
