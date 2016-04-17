(function ($) {
    "use strict";

    var mapBtn = document.querySelector(".mapBtn");
    var listBtn = document.querySelector(".listBtn");
    var map;

    function displayMap() {
        document.getElementById('courseMap').style.display = "block";
        document.getElementById('mapContainer').style.display = "block";
        if (document.getElementById('courseList') != undefined || document.getElementById('courseList') != null)
        document.getElementById('courseList').style.display = "none";
        initMap();
    }

    function displayList() {
        document.getElementById('courseList').style.display = "block";
        document.getElementById('courseMap').style.display = "none";
        document.getElementById('mapContainer').style.display = "none";
    }

    function initMap() {
       
        map = new google.maps.Map(document.getElementById('courseMap'), {
            center: new google.maps.LatLng(56.412758, 10.877843),
            zoom: 12
        });

        var geocoder = new google.maps.Geocoder();
        if (document.getElementById('submit') != undefined || document.getElementById('submit') != null)
        document.getElementById('submit').addEventListener('click', function() {
            geocodeAddress(geocoder, map);
        })
    }

    //https://developers.google.com/maps/documentation/javascript/examples/geocoding-simple

    function geocodeAddress(geocoder, resultsMap) {
        var address = document.getElementById('address').value;
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === google.maps.GeocoderStatus.OK) {
                resultsMap.setCenter(results[0].geometry.location);
                var marker = new google.maps.Marker({
                    map: resultsMap,
                    position: results[0].geometry.location
                });
            } else {
                alert('Geocode fejlede grundet følgende fejl: ' + status);
            }
        });
    }

    if (mapBtn != undefined || mapBtn != null) {
        mapBtn.addEventListener("click", function (e) {
            displayMap();
        });
    }
    if (listBtn != undefined || listBtn != null) {
        listBtn.addEventListener("click", function (e) {
            displayList();
        });
    }

    var xsDevice = window.matchMedia("(max-width:767px)");
    var smDevice = window.matchMedia("(min-width:768px) and (max-width: 991px)");
    var mdDevice = window.matchMedia("(min-width:992px) and (max-width: 1199px)");
    var lgDevice = window.matchMedia("(min-width:1200px)");

    if (xsDevice.matches) {
        console.log("xs");
    }
    else if (smDevice.matches) {
        console.log("sm");
    }
    else if (mdDevice.matches) {
        console.log("md");
    }
    else {
        console.log("lg");
    }

    google.maps.event.addDomListener(window, 'load', initMap);

    $('.birthday').datepicker({
        changeMonth: true,
        changeYear: true,
        regional: ["da"],
        minDate: "-120Y",
        maxDate: 0,
        showOtherMonths: true,
        selectOtherMonths: true,
        defaultDate: "-18Y"
    });

    $('.datetime').datepicker({
        changeMonth: true,
        changeYear: true,
        regional: ["da"],
        minDate: 0,
        maxDate: "+1Y",
        showWeek: true,
        showOtherMonths: true,
        selectOtherMonths: true
    });

    // https://stackoverflow.com/questions/5966244/jquery-datepicker-chrome
    $(function () {
        $.validator.methods.date = function (value, element) {
            if ($.browser.webkit) {

                //ES - Chrome does not use the locale when new Date objects instantiated:
                var d = new Date();

                return this.optional(element) || !/Invalid|NaN/.test(new Date(d.toLocaleDateString(value)));
            }
            else {

                return this.optional(element) || !/Invalid|NaN/.test(new Date(value));
            }
        };
    });

})(jQuery);

