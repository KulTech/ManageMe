// For an introduction to the Blank template, see the following documentation:
// http://go.microsoft.com/fwlink/?LinkID=397705
// To debug code on page load in Ripple or on Android devices/emulators: launch your app, set breakpoints, 
// and then run "window.location.reload()" in the JavaScript Console.
var ManageMeMobile;
(function (ManageMeMobile) {
    "use strict";
    var Application;
    (function (Application) {
        function initialize() {
            document.addEventListener('deviceready', onDeviceReady, false);
        }
        Application.initialize = initialize;
        function onDeviceReady() {
            // Handle the Cordova pause and resume events
            document.addEventListener('pause', onPause, false);
            document.addEventListener('resume', onResume, false);
            document.getElementById("btnTakePicture").onclick = function () {
                navigator.camera.getPicture(function (imageUri) {
                    var lastphoto = document.getElementById("pictureDisplayed");
                    lastphoto.innerHTML = "<img src='" + imageUri + "', style = 'width:75%;'/>";
                }, null, null);
            };
            document.getElementById("btnClearPicture").onclick = function () {
                var lastphoto = document.getElementById("pictureDisplayed");
                lastphoto.innerHTML = "";
            };
            document.getElementById("btnSave").onclick = function () {
                alert("world");
                var document = {
                    Title: 'testtitle',
                    Path: 'testpath',
                    Notes: 'testnotes'
                };
                $.ajax({
                    url: 'http://managememobileservice.azurewebsites.net/api/Doc',
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        $('#lastphoto').html(data.msg);
                    },
                    data: document
                });
            };
            // TODO: Cordova has been loaded. Perform any initialization that requires Cordova here.
        }
        function onPause() {
            // TODO: This application has been suspended. Save application state here.
        }
        function onResume() {
            // TODO: This application has been reactivated. Restore application state here.
        }
    })(Application = ManageMeMobile.Application || (ManageMeMobile.Application = {}));
    window.onload = function () {
        Application.initialize();
    };
})(ManageMeMobile || (ManageMeMobile = {}));
//# sourceMappingURL=appBundle.js.map