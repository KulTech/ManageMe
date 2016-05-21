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
            $.getJSON("http://kultech.ddns.net/ManageMeService/api/Doc/GetETypes", function (data) {
                $.each(data, function (index) {
                    $("#sltETypes").append('<option value="' + data[index].Id + '">' + data[index].TypeName + '</option>');
                });
            });
            $.getJSON("http://kultech.ddns.net/ManageMeService/api/Stock/GetStocks", { username: "jay Pan" }, function (data) {
                $.each(data, function (index) {
                    alert("hello" + data[index].ticker);
                });
            });
            $.getJSON("http://kultech.ddns.net/ManageMeService/api/Doc/GetSubTypes/1", function (data) {
                $.each(data, function (index) {
                    $("#sltSub").append('<option value="' + data[index].Id + '">' + data[index].SubTypeName + '</option>');
                });
            });
            $.getJSON("http://kultech.ddns.net/ManageMeService/api/Doc/GetVendors", function (data) {
                $.each(data, function (index) {
                    $("#sltVendor").append('<option value="' + data[index].Id + '">' + data[index].Name + '</option>');
                });
                $("#sltVendor").append('<option value="0">Add New....</option>');
            });
            var imageLocation = "";
            //var mprogress = new Mprogress();
            document.getElementById("btnTakePicture").onclick = function () {
                navigator.camera.getPicture(function (imageData) {
                    var image = document.getElementById("pictureDisplayed");
                    imageLocation = "data:image/jpeg;base64," + imageData;
                    $("#DispArea").html("");
                    $("#DispArea").append("<img src= '" + imageLocation + "', style = 'width:95%;height:250px' />");
                }, null, {
                    quality: 50,
                    destinationType: Camera.DestinationType.DATA_URL,
                    saveToPhotoAlbum: false
                });
            };
            document.getElementById("btnClearPicture").onclick = function () {
                $("#DispArea").html("");
                imageLocation = "";
            };
            $("#sltETypes").change(function () {
                $("#sltSub").html("");
                $.getJSON("http://kultech.ddns.net/ManageMeService/api/Doc/GetSubTypes/" + jQuery("#sltETypes option:selected").val(), function (data) {
                    $.each(data, function (index) {
                        $("#sltSub").append('<option value="' + data[index].Id + '">' + data[index].SubTypeName + '</option>');
                    });
                });
            });
            $("#sltVendor").change(function () {
                if (jQuery("#sltVendor option:selected").val() == 0) {
                    $("input.vtxt:text").val("");
                    $("#VendorDetail").show();
                }
                else {
                    $("#VendorDetail").hide();
                }
            });
            $("#btnVSave").click(function () {
                var v = {
                    Name: $('#txtVName').val(),
                    Phone: $('#txtVPhone').val(),
                    location: $('#txtVlocation').val(),
                    email: $('#txtVemail').val()
                };
                $.ajax({
                    url: 'http://kultech.ddns.net/ManageMeService/api/Doc/PostVendors',
                    type: 'POST',
                    data: v,
                    dataType: 'json',
                    //beforeSend: function () {
                    //    $('#load').show();    /*showing  a div with spinning image */
                    //},
                    success: function (data) {
                        //$('#load').html("<b>Data saved successfully!</b>");
                        $('#VendorDetail').hide();
                        $('#sltVendor').html('');
                        $.getJSON("http://kultech.ddns.net/ManageMeService/api/Doc/GetVendors", function (data) {
                            $.each(data, function (index) {
                                $("#sltVendor").append('<option value="' + data[index].Id + '">' + data[index].Name + '</option>');
                            });
                            $("#sltVendor").append('<option value="0" class="bravo">Add New....</option>');
                        });
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        debugger;
                        $('#load').html("<b>Operation failed!</b>");
                        $('#DispArea').html(xhr.status.toString() + thrownError + ajaxOptions);
                        //alert(xhr.status);
                        //alert(thrownError);
                    }
                });
            });
            $("#btnNew").click(function () {
                $("#form1").trigger("reset");
                $("#btnSave").show();
                $("#btnUpdate").css('visibility', 'hidden');
                $("#btnNew").css('visibility', 'hidden');
                $('#docId').val("");
                $('#load').hide();
                $("#DispArea").html("");
            });
            $("#btnUpdate").click(function () {
                if ($("#form1").valid()) {
                    var d = {
                        Id: $("#docId").val(),
                        Notes: $('#txtNotes').val(),
                        Date: $('#txtDate').val(),
                        PropertyId: jQuery("#sltProperties option:selected").val(),
                        typeid: jQuery("#sltSub option:selected").val(),
                        amount: $('#txtAmount').val(),
                        fileContent: imageLocation,
                        VendorId: $('#sltVendor option:selected').val()
                    };
                    $.ajax({
                        url: 'http://kultech.ddns.net/ManageMeService/api/Doc/PostDocuments',
                        type: 'POST',
                        data: d,
                        dataType: 'json',
                        beforeSend: function () {
                            $('#load').html("<div style='color:red'><b>Update data,this might take several minutes......</b></div>");
                            $('#load').show();
                            $('#btnUpdate').css('visibility', 'hidden'); /*showing  a div with spinning image */
                        },
                        success: function (data) {
                            $('#load').html("<div style='color:green'><b>Data updated successfully!</b></div>");
                            $('#docId').val(data);
                            $('#btnUpdate').css('visibility', 'visible');
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            debugger;
                            $('#load').html("<b>Operation failed!</b>");
                            $('#DispArea').html(xhr.status.toString() + thrownError + ajaxOptions);
                            //alert(xhr.status);
                            //alert(thrownError);
                        }
                    });
                }
                //var myemail: CordovaPluginEmailComposer;
                //if (myemail.isAvailable) {
                //    $('#load').html("no email service");
                //    $('#load').show(); 
                //};
                //myemail.open(
                //    {
                //        to: ["wadepan@gmail.com"],
                //        cc: ["pliming@gmail.com"],
                //        subject: "congratulation",
                //        body: "You have a new expense!",
                //        isHtml: true
                //    }, null);
            });
            $("#btnSave").click(function () {
                if ($("#form1").valid()) {
                    var d = {
                        Notes: $('#txtNotes').val(),
                        Date: $('#txtDate').val(),
                        PropertyId: jQuery("#sltProperties option:selected").val(),
                        typeid: jQuery("#sltSub option:selected").val(),
                        amount: $('#txtAmount').val(),
                        fileContent: imageLocation,
                        VendorId: $('#sltVendor option:selected').val()
                    };
                    $.ajax({
                        url: 'http://kultech.ddns.net/ManageMeService/api/Doc/PostDocuments',
                        type: 'POST',
                        data: d,
                        dataType: 'json',
                        beforeSend: function () {
                            $('#load').html("<div style='color:red'><b>Save data,this might take several minutes......</b></div>");
                            $('#load').show();
                            $('#btnSave').hide(); /*showing  a div with spinning image */
                        },
                        success: function (data) {
                            $('#load').html("<div style='color:green'><b>Data saved successfully!</b></div>");
                            $('#btnUpdate').css('visibility', 'visible');
                            $('#btnNew').css('visibility', 'visible');
                            $('#docId').val(data);
                        },
                        error: function (xhr, ajaxOptions, thrownError) {
                            debugger;
                            $('#load').html("<b>Operation failed!</b>");
                            $('#btnNew').show();
                            $('#DispArea').html(xhr.status.toString() + thrownError + ajaxOptions);
                            //alert(xhr.status);
                            //alert(thrownError);
                        }
                    });
                }
            });
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