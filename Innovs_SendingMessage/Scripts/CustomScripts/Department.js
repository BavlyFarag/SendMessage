

$(function () {
    //Type On change
    $("#TypeID").on('change', function () {
        var TypeID = $(this).val();

        $.ajax({
            type: "POST",
            url: location.origin + "/Departments/GetSubtype?TypeID=" + TypeID,
            data: {},
            success: function (data) {
                if (data.length != 0) {
                    $("#SubtypeID").empty();
                    $.each(data, function (i, item) {
                        $("#SubtypeID").append("<option value='" + item.ID + "'>" + item.Name + "</option>");
                    });

                    var SubtypeID = $("#SubtypeID").val();

                    $.ajax({
                        type: "POST",
                        url: location.origin + "/Departments/GetCategory?SubtypeID=" + SubtypeID,
                        data: {},
                        success: function (data2) {
                            if (data2.length != 0) {
                                $("#CategoryID").empty();
                                $.each(data2, function (i2, item2) {
                                    $("#CategoryID").append("<option value='" + item2.ID + "'>" + item2.Name + "</option>");
                                });

                                var CategoryID = $("#CategoryID").val();

                                $.ajax({
                                    type: "POST",
                                    url: location.origin + "/Departments/GetSubcategory?CategoryID=" + CategoryID,
                                    data: {},
                                    success: function (data3) {
                                        if (data3.length != 0) {
                                            $("#SubcategoryID").empty();
                                            $.each(data3, function (i3, item3) {
                                                $("#SubcategoryID").append("<option value='" + item3.ID + "'>" + item3.Name + "</option>");
                                            });
                                        }
                                        else {
                                            $("#SubcategoryID").empty();
                                        }
                                    }
                                });

                            }
                            else {
                                $("#CategoryID").empty();
                            }
                        }
                    });
                }
                else {
                    $("#SubtypeID").empty();
                    $("#CategoryID").empty();
                    $("#SubcategoryID").empty();
                }
            }
        });


    });




    //Suptype On change
    $("#SubtypeID").on('change', function () {
        var SubtypeID = $(this).val();

        $.ajax({
            type: "POST",
            url: location.origin + "/Departments/GetCategory?SubtypeID=" + SubtypeID,
            data: {},
            success: function (data) {
                if (data.length != 0) {
                    $("#CategoryID").empty();
                    $.each(data, function (i, item) {
                        $("#CategoryID").append("<option value='" + item.ID + "'>" + item.Name + "</option>");
                    });


                    var CategoryID = $("#CategoryID").val();

                    $.ajax({
                        type: "POST",
                        url: location.origin + "/Departments/GetSubcategory?CategoryID=" + CategoryID,
                        data: {},
                        success: function (data3) {
                            if (data3.length != 0) {
                                $("#SubcategoryID").empty();
                                $.each(data3, function (i3, item3) {
                                    $("#SubcategoryID").append("<option value='" + item3.ID + "'>" + item3.Name + "</option>");
                                });
                            }
                            else {
                                $("#SubcategoryID").empty();
                            }
                        }
                    });


                }
                else {
                    $("#CategoryID").empty();
                    $("#SubcategoryID").empty();
                }
            }
        });
    });



    //Category On Change
    $("#CategoryID").on('change', function () {
        var CategoryID = $(this).val();

        $.ajax({
            type: "POST",
            url: location.origin + "/Departments/GetSubcategory?CategoryID=" + CategoryID,
            data: {},
            success: function (data) {
                if (data.length != 0) {
                    $("#SubcategoryID").empty();
                    $.each(data, function (i, item) {
                        $("#SubcategoryID").append("<option value='" + item.ID + "'>" + item.Name + "</option>");
                    });
                }
                else {
                    $("#SubcategoryID").empty();
                }
            }
        });
    });




    //Department on change
    $("#DepartmentID").on('change', function () {
        var DepartmentID = $(this).val();
        $.ajax({
            url: location.origin + "/Departments/UpdateViewByDepartmentID?DepartmentID=" + DepartmentID
        }).done(function (data) {
            $("#DepartmentAssigneeView").html(data);
        });

    });


    // DepartmentAssigneeGrid Delete Button

    $("body").on("click", ".delete", function () {
        var id = $(this).val();
        $.ajax({
            url: location.origin + "/Departments/DeleteDepartmentAssignee?id=" + id
        }).done(function (data) {
            $("#DepartmentAssigneeView").html(data);
        });

    });




});



