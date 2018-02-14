public Program()
{
    //Runtime.UpdateFrequency = UpdateFrequency.Update10;
}

public void Save()
{
}

public void Main(string argument, UpdateType updateSource)
{
    List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
    GridTerminalSystem.GetBlocksOfType<IMyTerminalBlock>(blocks);

    for(int i = 0; i < blocks.Count; ++i)
    {
        if(!blocks[i].HasInventory)
            continue;

        int invCount = blocks[i].InventoryCount;
        for(int j = 0; j < invCount; ++j)
        {
            List<IMyInventoryItem> items = blocks[i].GetInventory(j).GetItems();
            for(int k = 0; k < items.Count; ++k)
            {
                Echo("" + items[k].GetDefinitionId() + " : " + items[k].Amount);
            }
        }

    }
}
