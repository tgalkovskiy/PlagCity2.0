using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distrids : MonoBehaviour
{
    public MainScript MainScript;
    //Bridges
    public int BridgesPeople = 100;
    public int BridgesViol = 0;
    public int BridgesDeath = 0;
    //Castriver
    public int CastRiverPeople = 100;
    public int CastRiverViol = 0;
    public int CastRiverDeath = 0;
    //Cliff
    public int CliffPeople = 100;
    public int CliffViol = 0;
    public int CliffDeath = 0;
    //Coin
    public int CoinBrookPeople = 100;
    public int CoinBrookViol = 0;
    public int CoinBrookDeath = 0;
    //ColdWell
    public int ColdWellPeople = 100;
    public int ColdWellViol = 0;
    public int ColdWellDeath = 0;
    //Darkfield
    public int DarkfieldPeople = 100;
    public int DarkfieldViol = 0;
    public int DarkfieldDeath = 0;
    //Farmside
    public int FarmsidePeople = 100;
    public int FarmsideViol = 0;
    public int FarmsideDeath = 0;
    //Farside
    public int FarsidePeople = 100;
    public int FarsideViol = 0;
    public int FarsideDeath = 0;
    //GrandSea
    public int GrandSeaPeople = 100;
    public int GrandSeaViol = 0;
    public int GrandSeaDeath = 0;
    //GrandStream
    public int GrandStreamPeople = 100;
    public int GrandStreamViol = 0;
    public int GrandStreamDeath = 0;
    //GreatLend
    public int GreatLendPeople = 100;
    public int GreatLendViol = 0;
    public int GreatLendDeath = 0;
    //NorthMine
    public int NorthMinePeople = 100;
    public int NorthMineViol = 0;
    public int NorthMineDeath = 0;
    //Portland
    public int PortlandPeople = 100;
    public int PortlandViol = 0;
    public int PortlandDeath = 0;
    //Seaside
    public int SeasidePeople = 100;
    public int SeasideViol = 0;
    public int SeasideDeath = 0;
    //SouthWood
    public int SouthWoodPeople = 100;
    public int SouthWoodViol = 0;
    public int SouthWoodDeath = 0;
    //SteamPark
    public int SteamParkPeople = 100;
    public int SteamParkViol = 0;
    public int SteamParkDeath = 0;
    //Sunland
    public int SunlandPeople = 100;
    public int SunlandViol = 0;
    public int SunlandDeath = 0;
    //UnderWood
    public int UnderWoodPeople = 100;
    public int UnderWoodViol = 0;
    public int UnderWoodDeath = 0;
    //Union
    public int UnionPeople = 100;
    public int UnionViol = 0;
    public int UnionDeath = 0;
    //WestRiver
    public int WestRiverPeople = 100;
    public int WestRiverViol = 0;
    public int WestRiverDeath = 0;
    //WestSide
    public int WestSidePeople = 100;
    public int WestSideViol = 0;
    public int WestSideDeath = 0;

    public void Bridges()
    {
        if(BridgesViol < 100)
        {
            //прирост больных 
            BridgesViol += 1 + BridgesViol / 4;
            //ограничение в численность района
            BridgesViol = BridgesViol > BridgesPeople ? BridgesPeople : BridgesViol;
            //активация красной зоны района
            if(BridgesViol > 90)
            {
                MainScript.DistrictRed[0].SetActive(true);
            }
            else
            {
                MainScript.DistrictRed[0].SetActive(false);
            }
        }
        //прирост умирающих 
        if(BridgesViol > 70 && BridgesDeath < 100)
        {
            BridgesDeath += 1 + BridgesDeath / 6;
        }
        if(BridgesViol > 50)
        {
            //Заражение след. района 
            CastRiver();
        }

    }

    public void CastRiver()
    {
        if (CastRiverViol < 100)
        {
            //прирост больных 
            CastRiverViol += 1 + CastRiverViol / 4;
            //ограничение в численность района
            CastRiverViol = CastRiverViol > CastRiverPeople ? CastRiverPeople : CastRiverViol;
        }
        //активация красной зоны района
        if (CastRiverViol > 90)
        {
            MainScript.DistrictRed[1].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[1].SetActive(false);
        }
        //прирост умирающих
        if (CastRiverViol > 70 && CastRiverDeath < 100)
        {
            CastRiverDeath += 1 + CastRiverDeath / 6;
        }
        if (CastRiverViol > 50)
        {
            //Заражение след. района 
            Cliff();
        }
    }
    public void Cliff()
    {
        if (CliffViol < 100)
        {
            //прирост больных 
            CliffViol += 1 + CliffViol / 4;
            //ограничение в численность района
            CliffViol = CliffViol > CliffPeople ? CliffPeople : CliffViol;
        }
        //активация красной зоны района
        if (CliffViol > 90)
        {
            MainScript.DistrictRed[2].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[2].SetActive(false);
        }
        //прирост умирающих
        if (CliffViol > 70 && CliffDeath < 100)
        {
            CliffDeath += 1 + CliffDeath / 6;
        }
        if (CliffViol > 50)
        {
            //Заражение след. района 
            CoinBrook();
        }
    }
    public void CoinBrook()
    {
        if (CoinBrookViol < 100)
        {
            //прирост больных 
            CoinBrookViol += 1 + CoinBrookViol / 4;
            //ограничение в численность района
            CoinBrookViol = CoinBrookViol > CoinBrookPeople ? CoinBrookPeople : CoinBrookViol;
        }
        //активация красной зоны района
        if (CoinBrookViol > 90)
        {
            MainScript.DistrictRed[3].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[3].SetActive(false);
        }
        //прирост умирающих
        if (CoinBrookViol > 70 && CoinBrookDeath < 100)
        {
            CoinBrookDeath += 1 + CoinBrookDeath / 6;
        }
        if (CoinBrookViol > 50)
        {
            //Заражение след. района 
            ColdWell();
        }
    }
    public void ColdWell()
    {
        if (ColdWellViol < 100)
        {
            //прирост больных 
            ColdWellViol += 1 + ColdWellViol / 4;
            //ограничение в численность района
            ColdWellViol = ColdWellViol > ColdWellPeople ? ColdWellPeople : ColdWellViol;
        }
        //активация красной зоны района
        if (ColdWellViol > 90)
        {
            MainScript.DistrictRed[4].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[4].SetActive(false);
        }
        //прирост умирающих
        if (ColdWellViol > 70 && ColdWellDeath < 100)
        {
            ColdWellDeath += 1 + ColdWellDeath / 6;
        }
        if (ColdWellViol > 50)
        {
            //Заражение след. района 
            Darkfield();
        }
    }
    public void Darkfield()
    {
        if (DarkfieldViol < 100)
        {
            //прирост больных 
            DarkfieldViol += 1 + DarkfieldViol / 4;
            //ограничение в численность района
            DarkfieldViol = DarkfieldViol > DarkfieldPeople ? DarkfieldPeople : DarkfieldViol;
        }
        //активация красной зоны района
        if (DarkfieldViol > 90)
        {
            MainScript.DistrictRed[5].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[5].SetActive(false);
        }
        //прирост умирающих
        if (DarkfieldViol > 70 && DarkfieldDeath < 100)
        {
            DarkfieldDeath += 1 + DarkfieldDeath / 6;
        }
        if (DarkfieldViol > 50)
        {
            //Заражение след. района 
            Farmside();
        }
    }
    public void Farmside()
    {
        if (FarmsideViol < 100)
        {
            //прирост больных 
            FarmsideViol += 1 + FarmsideViol / 4;
            //ограничение в численность района
            FarmsideViol = FarmsideViol > FarmsidePeople ? FarmsidePeople : FarmsideViol;
        }
        //активация красной зоны района
        if (FarmsideViol > 90)
        {
            MainScript.DistrictRed[6].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[6].SetActive(false);
        }
        //прирост умирающих
        if (FarmsideViol > 70 && FarmsideDeath < 100)
        {
            FarmsideDeath += 1 + FarmsideDeath / 6;
        }
        if (FarmsideViol > 50)
        {
            //Заражение след. района 
            Farside();
        }
    }
    public void Farside()
    {
        if (FarsideViol < 100)
        {
            //прирост больных 
            FarsideViol += 1 + FarsideViol / 4;
            //ограничение в численность района
            FarsideViol = FarsideViol > FarsidePeople ? FarsidePeople : FarsideViol;
        }
        //активация красной зоны района
        if (FarsideViol > 90)
        {
            MainScript.DistrictRed[7].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[7].SetActive(false);
        }
        //прирост умирающих
        if (FarsideViol > 70 && FarsideDeath < 100)
        {
            FarsideDeath += 1 + FarsideDeath / 6;
        }
        if (FarsideViol > 50)
        {
            //Заражение след. района 
            GrandSea();
        }
    }
    public void GrandSea()
    {
        if (GrandSeaViol < 100)
        {
            //прирост больных 
            GrandSeaViol += 1 + GrandSeaViol / 4;
            //ограничение в численность района
            GrandSeaViol = GrandSeaViol > GrandSeaPeople ? GrandSeaPeople : GrandSeaViol;
        }
        //активация красной зоны района
        if (GrandSeaViol > 90)
        {
            MainScript.DistrictRed[8].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[8].SetActive(false);
        }
        //прирост умирающих
        if (GrandSeaViol > 70 && GrandSeaDeath < 100)
        {
            GrandSeaDeath += 1 + GrandSeaDeath / 6;
        }
        if (GrandSeaViol > 50)
        {
            //Заражение след. района 
            //GrandStream();
            GreatLend();
        }
    }
    public void GrandStream()
    {
        if (GrandStreamViol < 100)
        {
            //прирост больных 
            GrandStreamViol += 1 + GrandStreamViol / 4;
            //ограничение в численность района
            GrandStreamViol = GrandStreamViol > GrandStreamPeople ? GrandStreamPeople : GrandStreamViol;
        }
        //активация красной зоны района
        if (GrandStreamViol > 90)
        {
            MainScript.DistrictRed[9].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[9].SetActive(false);
        }
        //прирост умирающих
        if (GrandStreamViol > 70 && GrandStreamDeath < 100)
        {
            GrandStreamDeath += 1 + GrandStreamDeath / 6;
        }
        if (GrandStreamViol > 50)
        {
            //Заражение след. района 
            GreatLend();
        }
    }
    public void GreatLend()
    {
        if (GreatLendViol < 100)
        {
            //прирост больных 
            GreatLendViol += 1 + GreatLendViol / 4;
            //ограничение в численность района
            GreatLendViol = GreatLendViol > GreatLendPeople ? GreatLendPeople : GreatLendViol;
        }
        //активация красной зоны района
        if (GreatLendViol > 90)
        {
            MainScript.DistrictRed[10].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[10].SetActive(false);
        }
        //прирост умирающих
        if (GreatLendViol > 70 && GreatLendDeath < 100)
        {
            GreatLendDeath += 1 + GreatLendDeath / 6;
        }
        if (GreatLendViol > 50)
        {
            //Заражение след. района 
            NorthMine();
        }
    }
    public void NorthMine()
    {
        if (NorthMineViol < 100)
        {
            //прирост больных 
            NorthMineViol += 1 + NorthMineViol / 4;
            //ограничение в численность района
            NorthMineViol = NorthMineViol > NorthMinePeople ? NorthMinePeople : NorthMineViol;
        }
        //активация красной зоны района
        if (NorthMineViol > 90)
        {
            MainScript.DistrictRed[11].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[11].SetActive(false);
        }
        //прирост умирающих
        if (NorthMineViol > 70 && NorthMineDeath < 100)
        {
            NorthMineDeath += 1 + NorthMineDeath / 6;
        }
        if (NorthMineViol > 50)
        {
            //Заражение след. района 
            Portland();
        }
    }
    public void Portland()
    {
        if (PortlandViol < 100)
        {
            //прирост больных 
            PortlandViol += 1 + PortlandViol / 4;
            //ограничение в численность района
            PortlandViol = PortlandViol > PortlandPeople ? PortlandPeople : PortlandViol;
        }
        //активация красной зоны района
        if (PortlandViol > 90)
        {
            MainScript.DistrictRed[12].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[12].SetActive(false);
        }
        //прирост умирающих
        if (PortlandViol > 70 && PortlandDeath < 100)
        {
            PortlandDeath += 1 + PortlandDeath / 6;
        }
        if (PortlandViol > 50)
        {
            //Заражение след. района 
            Seaside();
        }
    }
    public void Seaside()
    {
        if (SeasideViol < 100)
        {
            //прирост больных 
            SeasideViol += 1 + SeasideViol / 4;
            //ограничение в численность района
            SeasideViol = SeasideViol > SeasidePeople ? SeasidePeople : SeasideViol;
        }
        //активация красной зоны района
        if (SeasideViol > 90)
        {
            MainScript.DistrictRed[13].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[13].SetActive(false);
        }
        //прирост умирающих
        if (SeasideViol > 70 && SeasideDeath < 100)
        {
            SeasideDeath += 1 + SeasideDeath / 6;
        }
        if (SeasideViol > 50)
        {
            //Заражение след. района 
            SouthWood();
        }
    }
    public void SouthWood()
    {
        if (SouthWoodViol < 100)
        {
            //прирост больных 
            SouthWoodViol += 1 + SouthWoodViol / 4;
            //ограничение в численность района
            SouthWoodViol = SouthWoodViol > SouthWoodPeople ? SouthWoodPeople : SouthWoodViol;
        }
        //активация красной зоны района
        if (SouthWoodViol > 90)
        {
            MainScript.DistrictRed[14].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[14].SetActive(false);
        }
        //прирост умирающих
        if (SouthWoodViol > 70 && SouthWoodDeath < 100)
        {
            SouthWoodDeath += 1 + SouthWoodDeath / 6;
        }
        if (SouthWoodViol > 50)
        {
            //Заражение след. района 
            SteamPark();
        }
    }
    public void SteamPark()
    {
        if (SteamParkViol < 100)
        {
            //прирост больных 
            SteamParkViol += 1 + SteamParkViol / 4;
            //ограничение в численность района
            SteamParkViol = SteamParkViol > SteamParkPeople ? SteamParkPeople : SteamParkViol;
        }
        //активация красной зоны района
        if (SteamParkViol > 90)
        {
            MainScript.DistrictRed[15].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[15].SetActive(false);
        }
        //прирост умирающих
        if (SteamParkViol > 70 && SteamParkDeath < 100)
        {
            SteamParkDeath += 1 + SteamParkDeath / 6;
        }
        if (SteamParkViol > 50)
        {
            //Заражение след. района 
            //Sunland();
            UnderWood();
        }
    }
    public void Sunland()
    {
        if (SunlandViol < 100)
        {
            //прирост больных 
            SunlandViol += 1 + SunlandViol / 4;
            //ограничение в численность района
            SunlandViol = SunlandViol > SunlandPeople ? SunlandPeople : SunlandViol;
        }
        //активация красной зоны района
        if (SunlandViol > 90)
        {
            MainScript.DistrictRed[16].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[16].SetActive(false);
        }
        //прирост умирающих
        if (SunlandViol > 70 && SunlandDeath < 100)
        {
            SunlandDeath += 1 + SunlandDeath / 6;
        }
        if (SunlandViol > 50)
        {
            //Заражение след. района 
            UnderWood();
        }
    }
    public void UnderWood()
    {
        if (UnderWoodViol < 100)
        {
            //прирост больных 
            UnderWoodViol += 1 + UnderWoodViol / 4;
            //ограничение в численность района
            UnderWoodViol = UnderWoodViol > UnderWoodPeople ? UnderWoodPeople : UnderWoodViol;
        }
        //активация красной зоны района
        if (UnderWoodViol > 90)
        {
            MainScript.DistrictRed[17].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[17].SetActive(false);
        }
        //прирост умирающих
        if (UnderWoodViol > 70 && UnderWoodDeath < 100)
        {
            UnderWoodDeath += 1 + UnderWoodDeath / 6;
        }
        if (UnderWoodViol > 50)
        {
            //Заражение след. района 
            Union();
        }
    }
    public void Union()
    {
        if (UnionViol < 100)
        {
            //прирост больных 
            UnionViol += 1 + UnionViol / 4;
            //ограничение в численность района
            UnionViol = UnionViol > UnionPeople ? UnionPeople : UnionViol;
        }
        //активация красной зоны района
        if (UnionViol > 90)
        {
            MainScript.DistrictRed[18].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[18].SetActive(false);
        }
        //прирост умирающих
        if (UnionViol > 70 && UnionDeath < 100)
        {
            UnionDeath += 1 + UnionDeath / 6;
        }
        if (UnionViol > 50)
        {
            //Заражение след. района 
            //WestRiver();
            WestSide();
        }
    }
    public void WestRiver()
    {
        if (WestRiverViol < 100)
        {
            //прирост больных 
            WestRiverViol += 1 + WestRiverViol / 4;
            //ограничение в численность района
            WestRiverViol = WestRiverViol > WestRiverPeople ? WestRiverPeople : WestRiverViol;
        }
        //активация красной зоны района
        if (WestRiverViol > 90)
        {
            MainScript.DistrictRed[19].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[19].SetActive(false);
        }
        //прирост умирающих
        if (WestRiverViol > 70 && WestRiverDeath < 100)
        {
            WestRiverDeath += 1 + WestRiverDeath / 6;
        }
        if (WestRiverViol > 50)
        {
            //Заражение след. района 
            WestSide();
        }
    }
    public void WestSide()
    {
        if (WestSideViol < 100)
        {
            //прирост больных 
            WestSideViol += 1 + WestSideViol / 4;
            //ограничение в численность района
            WestSideViol = WestSideViol > WestSidePeople ? WestSidePeople : WestSideViol;
        }
        //активация красной зоны района
        if (WestSideViol > 90)
        {
            MainScript.DistrictRed[20].SetActive(true);
        }
        else
        {
            MainScript.DistrictRed[20].SetActive(false);
        }
        //прирост умирающих
        if (WestSideViol > 70 && WestSideDeath < 100)
        {
            WestSideDeath += 1 + WestSideDeath / 6;
        }
    }
}
