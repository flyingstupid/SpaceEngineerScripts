public Program()
{
    Runtime.UpdateFrequency = UpdateFrequency.Update1 | UpdateFrequency.Update10;
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


bool entrance = false;
bool exit = false;
bool bay = false;

public void Main(string argument, UpdateType updateSource)
{
    IMyDoor alExit = GridTerminalSystem.GetBlockWithName("AL_Exit") as IMyDoor;
    IMyDoor alShipBay = GridTerminalSystem.GetBlockWithName("AL_Ship_Bay") as IMyDoor;
    IMyDoor alEntrence = GridTerminalSystem.GetBlockWithName("AL_Base_Entrence") as IMyDoor;
    IMyAirVent alAirVent = GridTerminalSystem.GetBlockWithName("AirLockVent") as IMyAirVent;

    bool closed = true;
    List<IMyTerminalBlock> ahd_list = new List<IMyTerminalBlock>();    
    GridTerminalSystem.SearchBlocksOfName("AHD_ShipBay", ahd_list);

    for(int i=0; i<ahd_list.Count; i++) 
    {
        IMyDoor door = ahd_list[i] as IMyDoor;
        if(door.Open) 
        {
            closed = false;
            break;    
        }
    }

    if(!alEntrence.Open && !alExit.Open && !alShipBay.Open)
    {
        alAirVent.GetActionWithName("OnOff_On").Apply(alAirVent);
        alAirVent.GetActionWithName("Depressurize_On").Apply(alAirVent);
        Echo("Vent Sucking");
    }
    else if(alEntrence.Open)
    {
        alAirVent.GetActionWithName("OnOff_Off").Apply(alAirVent);
        Echo("Vent Idle");
    }

    bool entranceChange = alEntrence.Open != entrance;
    bool exitChange = alExit.Open != exit;
    bool bayChange = alShipBay.Open != bay;

    if(entranceChange) 
    {    
        entrance = alEntrence.Open;
        if(entrance) 
        {
            alExit.GetActionWithName("Open_Off").Apply(alExit);
            if(!closed)
                alShipBay.GetActionWithName("Open_Off").Apply(alShipBay);
        }
    }

    if(exitChange) 
    {    
        exit = alExit.Open;
        if(exit) 
        {
            alEntrence.GetActionWithName("Open_Off").Apply(alEntrence);
            if(closed)
                alShipBay.GetActionWithName("Open_Off").Apply(alShipBay);
        }
    }

    if(bayChange) 
    {
        if(closed) 
        {
            alExit.GetActionWithName("Open_Off").Apply(alExit);
        } 
        else 
        {
            alEntrence.GetActionWithName("Open_Off").Apply(alEntrence);
        }
    }
}
