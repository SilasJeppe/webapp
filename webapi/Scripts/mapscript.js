var map;
var markers = [];

function initMap() {
    var mapDiv = document.getElementById('map');
    var mapOptions = {
        center: { lat: 57.048820, lng: 9.921747 },
        zoom: 13,
        streetViewControl: false,
        mapTypeControl: false,
        mapTypeId: google.maps.MapTypeId.HYBRID,
        editable: true
    };

    map = new google.maps.Map(mapDiv, mapOptions);
    var latlng = { lat: 57.048820, lng: 9.921747 };
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        title: 'Nissemand'
    });
    markers.push(marker); 
}

function Redirect(id) {
    var latlng = new google.maps.LatLng(57.048820, 9.929000);
    //var latlng = { lat: 57.048820, lng: 9.929000 };
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        title: 'Hans'
    });
    markers.push(marker);
    //google.maps.event.trigger(map, 'resize');
    //alert(id);
}



function addMarkers() {
    //var latlng = new google.maps.LatLng(57.048820, 9.921747 );
    var latlng = { lat: 57.048820, lng: 9.929000 };
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        title: 'Hans'
    });
    markers.push(marker);
    //google.maps.event.trigger(map, 'resize');
    //alert('hest');
}

function clearOverlay()
{
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