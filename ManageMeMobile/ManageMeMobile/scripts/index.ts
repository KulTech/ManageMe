﻿// For an introduction to the Blank template, see the following documentation:
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
            var imageLocation = ""; 
            document.getElementById("btnTakePicture").onclick = function () {
                navigator.camera.getPicture(function (imageUri) {
                    imageLocation = imageUri; 
                    var lastphoto = document.getElementById("pictureDisplayed");
                    lastphoto.innerHTML = "<img src='" + imageUri + "', style = 'width:75%;'/>"; 
                }, null, null)
            }
            document.getElementById("btnClearPicture").onclick = function () {
                var lastphoto = document.getElementById("pictureDisplayed");
                lastphoto.innerHTML = ""; 
            }
            
            $("#btnSave").click(function () { 
                $("#form1").validate();
                alert(imageLocation); 
                var d = {
                    Title: $('#txtTitle').val(),
                    Notes: $('#txtNotes').val(),
                    Date: $('#txtDate').val()
                };

                $.ajax({
                    url: 'http://managememobileservice.azurewebsites.net/api/Doc',
                    type: 'POST',
                    data: d,
                    dataType: 'json',
                    success: function (data) {
                        $('#pictureDisplayed').html("<div class='alert'>Data saved successfully!</div>");
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        $('#pictureDisplayed').html(xhr.status.toString() + thrownError + ajaxOptions);
                        //alert(xhr.status);
                        //alert(thrownError);
                        
                    }

                });

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
