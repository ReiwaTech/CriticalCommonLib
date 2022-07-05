using Lumina.Data;
using Lumina.Excel;
using Lumina.Excel.GeneratedSheets;

namespace CriticalCommonLib.Sheets
{
    //Props to caraxi for sorting this mess out
    public class SpecialShopEx : SpecialShop
    {
        public override void PopulateData(RowParser parser, Lumina.GameData lumina, Language language) {
            base.PopulateData(parser, lumina, language);

            Entries = new Entry[60];
            
            for (var i = 0; i < Entries.Length; i++) {
                Entries[i] = new Entry {
                    Result = new[] {
                        new ResultEntry {
                            Item = new LazyRow<ItemEx>(lumina, parser.ReadColumn<int>(1 + i), language),
                            Count = parser.ReadColumn<uint>(61 + i),
                            SpecialShopItemCategory = new LazyRow<SpecialShopItemCategory>(lumina, parser.ReadColumn<int>(121 + i), language),
                            HQ = parser.ReadColumn<bool>(181 + i)
                        },
                        new ResultEntry {
                            Item = new LazyRow<ItemEx>(lumina, parser.ReadColumn<int>(241 + i), language),
                            Count = parser.ReadColumn<uint>(301 + i),
                            SpecialShopItemCategory = new LazyRow<SpecialShopItemCategory>(lumina, parser.ReadColumn<int>(361 + i), language),
                            HQ = parser.ReadColumn<bool>(421 + i)
                        }
                    },
                    Cost = new[] {
                        new CostEntry {
                            Item = new LazyRow<ItemEx>(lumina, parser.ReadColumn<int>(481 + i), language),
                            Count = parser.ReadColumn<uint>(541 + i),
                            HQ = parser.ReadColumn<bool>(601 + i),
                            Collectability = parser.ReadColumn<ushort>(661 + i)
                        },
                        new CostEntry {
                            Item = new LazyRow<ItemEx>(lumina, parser.ReadColumn<int>(721 + i), language),
                            Count = parser.ReadColumn<uint>(781 + i),
                            HQ = parser.ReadColumn<bool>(841 + i),
                            Collectability = parser.ReadColumn<ushort>(901 + i)
                        },
                        new CostEntry {
                            Item = new LazyRow<ItemEx>(lumina, parser.ReadColumn<int>(961 + i), language),
                            Count = parser.ReadColumn<uint>(1021 + i),
                            HQ = parser.ReadColumn<bool>(1081 + i),
                            Collectability = parser.ReadColumn<ushort>(1141 + i)
                        }
                    },
                    Quest = new LazyRow<Quest>(lumina, parser.ReadColumn<int>(1201 + i), language),
                    Unknown6 = parser.ReadColumn<int>(1261 + i),
                    AchievementUnlock = new LazyRow<Achievement>(lumina, parser.ReadColumn<int>(1321 + i), language),
                    Unknown8 = parser.ReadColumn<byte>(1381 + i),
                    PatchNumber = parser.ReadColumn<ushort>(1441 + i)
                };
            }
            UseCurrencyType = parser.ReadColumn<byte>(1501);
            QuestUnlock = new LazyRow<Quest>(lumina, parser.ReadColumn<uint>(1502), language);
            CompleteText = new LazyRow<DefaultTalk>(lumina, parser.ReadColumn<int>(1503), language);
            NotCompleteText = new LazyRow<DefaultTalk>(lumina, parser.ReadColumn<int>(1504), language);
            Unknown1505 = parser.ReadColumn<uint>(1505);
            Unknown1506 = parser.ReadColumn<bool>(1506);

        }

        public struct CostResultEntry
        {
            public ResultEntry Result;
            public CostEntry Cost;
        }

        public struct Entry {
            public ResultEntry[] Result;
            public CostEntry[] Cost;
            public LazyRow<Quest> Quest;
            public int Unknown6;
            public LazyRow<Achievement> AchievementUnlock;
            public int Unknown8;
            public ushort PatchNumber;
        }

        public struct ResultEntry {
            public LazyRow<ItemEx> Item;
            public uint Count;
            public LazyRow<SpecialShopItemCategory> SpecialShopItemCategory;
            public bool HQ;
        }

        public struct CostEntry {
            public LazyRow<ItemEx> Item;
            public uint Count;
            public bool HQ;
            public ushort Collectability;
        }

        public Entry[] Entries;
    }

}