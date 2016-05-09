//Global fields used in the scripts
var map;
//array for the markers. Used when drawing the points to the map. And emptied when removing the markers from the map
var markers = [];
//var bounds = new google.maps.LatLngBounds();

//Creating the map with the given mapOptions.
function initMap() {
    var mapDiv = document.getElementById('map');
    var mapOptions = {
        center: { lat: 57.048820, lng: 9.921747 },
        zoom: 14,
        streetViewControl: false,
        mapTypeControl: false,
        mapTypeId: google.maps.MapTypeId.HYBRID,
        editable: true
    };
    map = new google.maps.Map(mapDiv, mapOptions);
}

//This function adds the marks to an array and draws all the array to the map.
function AddMarkers(array) {
    clearOverlay();  //delete old markers
    var latlngList = [];
    for (var i = 0; i < array.length; i++) {
        var latlng = new google.maps.LatLng(array[i].Coords.Y, array[i].Coords.X);
        latlngList.push(latlng);
        var marker = new google.maps.Marker({
            position: latlng,
            map: map
        });
        
        //bounds.extend(marker.position);
        markers.push(marker);
    }
    
    for (var i = 0; i < latlngList.length; i++)
    {
        bounds.extend(latlngList[i]);
    }
    
    setMapOnAll(map); //add markers to map
    showMarkers(); // show markers on map
    map.fitBounds(bounds);
    
    alert(map.getBounds());
    
    //map.panToBounds(bounds);
}

//This function is clearing the map, using another method to null the array and clear map
function clearOverlay() {
    clearMarkers();
    deleteMarkers();
}

// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (var i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}