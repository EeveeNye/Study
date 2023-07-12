using System;
using Sirenix.OdinInspector;

namespace Gyvr.Mythril2D
{
    [Serializable]
    public struct SaveFileData
    {
        [LabelText("标题")] public string header;
        [LabelText("地图")] public string map;
        [LabelText("日志数据块")] public JournalDataBlock journal;
        [LabelText("物品栏数据块")] public InventoryDataBlock inventory;
        [LabelText("游戏标记数据块")] public GameFlagsDataBlock gameFlags;
        [LabelText("玩家数据块")] public PlayerDataBlock player;
    }
}