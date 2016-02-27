function Satellite(pX, pY, pSourceX, pSourceY, pDestinationX, pDestinationY, pSatelliteName, pAscentDirection, pOnStation, pSolarPanelsDeployed, pFuel, pPower, pPlanetShift) {
    this.X = pX;
    this.Y = pY;
    this.SourceX = pSourceX;
    this.SourceY = pSourceY;
    this.DestinationX = pDestinationX;
    this.DestinationY = pDestinationY;
    this.SatelliteName = pSatelliteName;
    this.AscentDirection = pAscentDirection;
    this.OnStation = pOnStation;
    this.SolarPanelsDeployed = pSolarPanelsDeployed;
    this.Fuel = pFuel;
    this.Power = pPower;
    this.PlanetShift = pPlanetShift;

    this.GetStatus = function () {
        var status = 'Satellite <br />'
                     + 'Name: ' + this.SatelliteName + ' <br />'
                     + 'X: ' + this.X + ' <br />'
                     + 'Y: ' + this.Y + ' <br />'
                     + 'SourceX: ' + this.SourceX + ' <br />'
                     + 'SourceY: ' + this.SourceY + ' <br />'
                     + 'DestinationX: ' + this.DestinationX + ' <br />'
                     + 'DestinationY: ' + this.DestinationY + ' <br />'
                     + 'onStation: ' + this.OnStation + ' <br />'
                     + 'solarPanelsDeployed: ' + this.SolarPanelsDeployed + ' <br />'
                     + 'fuel: ' + this.Fuel + ' <br />'
                     + 'power: ' + this.Power + ' <br />'
                     + 'AscentDirection: ' + this.AscentDirection + ' <br />'
                     + 'PlanetShift: ' + this.PlanetShift;
        return status;
    }

    this.MoveSatellite = function () {
        var satellite = GetSatellite(this.SatelliteName);

        var left = parseInt(satellite.offsetLeft);
        var top = parseInt(satellite.offsetTop);

        var difLeftSatellite = this.X - left;
        var difTopSatellite = this.Y - top;

        left = left + difLeftSatellite;
        top = top + difTopSatellite;

        satellite.style.left = left.toString() + 'px';
        satellite.style.top = top.toString() + 'px';
    }

    this.DeploySolarPanels = function () {
        // var satellite = GetSatellite(this.SatelliteName);


        // var el = document.createElement('canvas');
        //// G_vmlCanvasManager.initElement(el);
        // var ctx = el.getContext('2d');

        // //var ctx = satellite.getContext("2d");
        // //ctx.beginPath();
        // //ctx.moveTo(0, 0);
        // //ctx.lineTo(this.X + 50, this.Y + 50);

        // if (this.OnStation == true) 
        //     ctx.lineTo(this.X + 50, this.Y + 50);
        // else
        //     ctx.lineTo(0, 0);

        // ctx.stroke();
    }

    this.SetDisplay = function () {
        var satellite = GetSatellite(this.SatelliteName);

        if (this.OnStation == true)// || this.PlanetShift == true)
            satellite.style.background = "green";
        else
            satellite.style.background = "yellow";

        if (this.SatelliteName == 'East')
            document.getElementById("EastDisplay").innerHTML = this.GetStatus();
        else if (this.SatelliteName == 'NorthEast')
            document.getElementById("NorthEastDisplay").innerHTML = this.GetStatus();
        else if (this.SatelliteName == 'North')
            document.getElementById("NorthDisplay").innerHTML = this.GetStatus();
        else if (this.SatelliteName == 'NorthWest')
            document.getElementById("NorthWestDisplay").innerHTML = this.GetStatus();
        else if (this.SatelliteName == 'West')
            document.getElementById("WestDisplay").innerHTML = this.GetStatus();
        else if (this.SatelliteName == 'SouthWest')
            document.getElementById("SouthWestDisplay").innerHTML = this.GetStatus();
        else if (this.SatelliteName == 'South')
            document.getElementById("SouthDisplay").innerHTML = this.GetStatus();
        else if (this.SatelliteName == 'SouthEast')
            document.getElementById("SouthEastDisplay").innerHTML = this.GetStatus();
        else
            alert('Unknown Satellite');
    }

    function GetSatellite(name) {
        var satellite = null;

        if (name == 'East')
            satellite = document.getElementById("satelliteEast");
        else if (name == 'NorthEast')
            satellite = document.getElementById("satelliteNorthEast");
        else if (name == 'North')
            satellite = document.getElementById("satelliteNorth");
        else if (name == 'NorthWest')
            satellite = document.getElementById("satelliteNorthWest");
        else if (name == 'West')
            satellite = document.getElementById("satelliteWest");
        else if (name == 'SouthWest')
            satellite = document.getElementById("satelliteSouthWest");
        else if (name == 'South')
            satellite = document.getElementById("satelliteSouth");
        else if (name == 'SouthEast')
            satellite = document.getElementById("satelliteSouthEast");
        else
            throw 'ERROR: Unknown satellite name';

        return satellite;
    }
}