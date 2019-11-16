// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
console.log('Javascript working !')

//Javascript code for google maps
function initAutocomplete() {
	// Create the autocomplete object, restricting the search predictions to
	// location types.
	autocomplete = new google.maps.places.Autocomplete((document.getElementById('autocomplete')),
		{ types: ['geocode'] });

}

// Gets the users location so that, the search results can be reduced and performance can be improved
//Its provided if the user allows to access his/her location
function geolocate() {
	if (navigator.geolocation) {
		navigator.geolocation.getCurrentPosition(function (position) {
			var geolocation = {
				lat: position.coords.latitude,
				lng: position.coords.longitude
			};
			var circle = new google.maps.Circle({
				center: geolocation,
				radius: position.coords.accuracy
			});
			autocomplete.setBounds(circle.getBounds());
		});
	}
}