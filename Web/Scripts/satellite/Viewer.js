var statusUpdates = [];

function RunDisplay() {
    InitializeViewer();
    while (statusUpdates.length < 1) {
        GetData();
    }
}

//TODO - make dynamic
function InitializeViewer() {
    var combined = '';

    var statDisplay = '<table class="SatelliteTable">';
    statDisplay = statDisplay + '<tr>';
    statDisplay = statDisplay + '        <td>';
    statDisplay = statDisplay + '            <div id="EastDisplay" name="EastDisplay" class="SatelliteDisplay">East waiting for updates...</div>';
    statDisplay = statDisplay + '        </td>';
    statDisplay = statDisplay + '        <td>';
    statDisplay = statDisplay + '            <div id="NorthEastDisplay" name="NorthEastDisplay" class="SatelliteDisplay">North East waiting for updates...</div>';
    statDisplay = statDisplay + '        </td>';
    statDisplay = statDisplay + '        <td>';
    statDisplay = statDisplay + '            <div id="NorthDisplay" name="NorthDisplay" class="SatelliteDisplay">North waiting for updates...</div>';
    statDisplay = statDisplay + '        </td>';
    statDisplay = statDisplay + '        <td>';
    statDisplay = statDisplay + '            <div id="NorthWestDisplay" name="NorthWestDisplay" class="SatelliteDisplay">North West waiting for updates...</div>';
    statDisplay = statDisplay + '        </td>';
    statDisplay = statDisplay + '        <td>';
    statDisplay = statDisplay + '            <div id="WestDisplay" name="WestDisplay" class="SatelliteDisplay">West waiting for updates...</div>';
    statDisplay = statDisplay + '        </td>';
    statDisplay = statDisplay + '        <td>';
    statDisplay = statDisplay + '            <div id="SouthWestDisplay" name="SouthWestDisplay" class="SatelliteDisplay">South West waiting for updates...</div>';
    statDisplay = statDisplay + '        </td>';
    statDisplay = statDisplay + '        <td>';
    statDisplay = statDisplay + '            <div id="SouthDisplay" name="SouthDisplay" class="SatelliteDisplay">South waiting for updates...</div>';
    statDisplay = statDisplay + '        </td>';
    statDisplay = statDisplay + '        <td>';
    statDisplay = statDisplay + '            <div id="SouthEastDisplay" name="SouthEastDisplay" class="SatelliteDisplay">South East waiting for updates...</div>';
    statDisplay = statDisplay + '        </td>';
    statDisplay = statDisplay + '    </tr>';
    statDisplay = statDisplay + '</table>';

    var planetSatellites = '<div class="Planet">Planet</div>';
    planetSatellites = planetSatellites + '<div id="satelliteEast" name="satelliteEast" class="Satellite">E</div>';
    planetSatellites = planetSatellites + '<div id="satelliteNorthEast" name="satelliteNorthEast" class="Satellite">NE</div>';
    planetSatellites = planetSatellites + '<div id="satelliteNorth" name="satelliteNorth" class="Satellite">N</div>';
    planetSatellites = planetSatellites + '<div id="satelliteNorthWest" name="satelliteNorthWest" class="Satellite">NW</div>';
    planetSatellites = planetSatellites + '<div id="satelliteWest" name="satelliteWest" class="Satellite">W</div>';
    planetSatellites = planetSatellites + '<div id="satelliteSouthWest" name="satelliteSouthWest" class="Satellite">SW</div>';
    planetSatellites = planetSatellites + '<div id="satelliteSouth" name="satelliteSouth" class="Satellite">S</div>';
    planetSatellites = planetSatellites + '<div id="satelliteSouthEast" name="satelliteSouthEast" class="Satellite">SE</div>';

    combined = statDisplay + planetSatellites;

    document.getElementById("simulationCanvas").innerHTML = combined;
}

function SetDisplay(statuses) {
    statusUpdates = statuses;
    DisplaySatellite();
}

function DisplaySatellite() {
    var arrayPos = statusUpdates.length - 1;

    if (arrayPos >= 0) {
        var satellite = new Satellite(statusUpdates[arrayPos].SatellitePosition.X,
                                        statusUpdates[arrayPos].SatellitePosition.Y,
                                        statusUpdates[arrayPos].SourceX,
                                        statusUpdates[arrayPos].SourceY,
                                        statusUpdates[arrayPos].DestinationX,
                                        statusUpdates[arrayPos].DestinationY,
                                        statusUpdates[arrayPos].SatelliteName,
                                        statusUpdates[arrayPos].AscentDirection,
                                        statusUpdates[arrayPos].onStation,
                                        statusUpdates[arrayPos].solarPanelsDeployed,
                                        statusUpdates[arrayPos].fuel,
                                        statusUpdates[arrayPos].power,
                                        statusUpdates[arrayPos].PlanetShift);

        satellite.MoveSatellite();
        satellite.SetDisplay();
        satellite.DeploySolarPanels();
    }
}