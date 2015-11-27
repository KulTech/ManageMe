// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397705
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
module ManageMeMobile {
    "use strict";

    export module Application {
        export function initialize() {
            document.addEventListener('deviceready', onDeviceReady, false);
        }

        function onDeviceReady() {
            // Handle the Cordova pause and resume events
            document.addEventListener('pause', onPause, false);
            document.addEventListener('resume', onResume, false);
            
            $.getJSON("http://managememobileservice.azurewebsites.net/api/Doc/GetProperties", function (data) {
                $.each(data, function (index) {
                    $("#sltProperties").append('<option value="' + data[index].Id + '">' + data[index].Name+'</option>');
                });
            }); 
            var imageLocation = ""; 
            document.getElementById("btnTakePicture").onclick = function () {
                navigator.camera.getPicture(function (imageData) {
                    var image = document.getElementById("pictureDisplayed");
                    imageLocation = "data:image/jpeg;base64," + imageData;
                    $("#DispArea").append("<img src= '" + imageLocation + "', style = 'width:95%;height:75px;' />"); 
              }, null, {
                    quality: 50,
                    destinationType: Camera.DestinationType.DATA_URL,
                    saveToPhotoAlbum: false
                    })
            }
            document.getElementById("btnClearPicture").onclick = function () {
                $("#DispArea").html(""); 
            }
            
            $("#btnSave").click(function () { 
                $("#form1").validate();
                var d = {
                    Title: $('#txtTitle').val(),
                    Notes: $('#txtNotes').val(),
                    Date: $('#txtDate').val(),
                    PropertyId: jQuery("#sltProperties option:selected").val(),
                    fileContent: imageLocation
                };

                $.ajax({
                    url: 'http://managememobileservice.azurewebsites.net/api/Doc/PostDocuments',
                    type: 'POST',
                    data: d,
                    dataType: 'json',
                    success: function (data) {
                        $('#DispArea').html("<div class='info'>Data saved successfully!</div>");
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        $('#DispArea').html(xhr.status.toString() + thrownError + ajaxOptions);
                        //alert(xhr.status);
                        //alert(thrownError);
                        
                    }

                });
                //remove the picture
                //navigator.camera.cleanup(function () {
                //    alert("cleaned up successfully");
                //}, function (message) {
                //    alert("cleaned up failed" + message);
                //});
            });
            
            // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.
        }

        function onPause() {
            // TODO: This application has been suspended. Save application state here.
        }

        function onResume() {
            // TODO: This application has been reactivated. Restore application state here.
        }

    }

    window.onload = function () {
        Application.initialize();
    }
}
