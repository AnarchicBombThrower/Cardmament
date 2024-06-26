using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileCapturePoint
{
    protected Player playerControlling;
    protected CapturePointWorld ourCapturePointWorld;

    public void unitAtPoint(UnitWorld unit)
    {
        playerCaptured(unit.getAllegiance());
    }

    private void playerCaptured(Player capturedBy)
    {
        playerNowControls(capturedBy);
        if (playerControlling != null) { playerNoLongerControls(playerControlling); }
        playerControlling = capturedBy;
    }

    private void setWorldRepresentation(CapturePointWorld to)
    {
        ourCapturePointWorld = to;
    }

    protected abstract void playerNowControls(Player player);

    protected abstract void playerNoLongerControls(Player player);
}
