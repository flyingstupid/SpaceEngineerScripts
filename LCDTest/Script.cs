public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update10;
}

public void Save()
{
    // Called when the program needs to save its state. Use
    // this method to save your state to the Storage field
    // or some other means. 
    // 
    // This method is optional and can be removed if not
    // needed.
}

int step = 0;
string allwork = "All work and no play makes Jack a dull boy.\n";

public void Main(string argument, UpdateType updateSource)
{
    IMyTextPanel screen = GridTerminalSystem.GetBlockWithName("LCDCompRoomT1") as IMyTextPanel;

    List<ITerminalAction> resultsList = new List<ITerminalAction>();    
    screen.GetActions(resultsList);


    int line = 18;
    screen.ShowPublicTextOnScreen();
    screen.WritePublicText("" + allwork[step%allwork.Length], step%(44*line)!=0);
    step++;


}
