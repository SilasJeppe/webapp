﻿function initMap() {
    var mapDiv = document.getElementById('map');
    var map = new google.maps.Map(mapDiv, {
        center: { lat: 57.048820, lng: 9.921747 },
        zoom: 13,
        streetViewControl: false,
        mapTypeControl: false,
        mapTypeId: google.maps.MapTypeId.HYBRID
    });
    var latlng = { lat: 57.048820, lng: 9.921747 };
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        title: 'Nissemand'
    });
 

}



function addMarkers() {
    var latlng = { lat: 57.048820, lng: 9.921747 };
    var marker = new google.maps.Marker({
        position: latlng,
        map: map
    }
        )
}