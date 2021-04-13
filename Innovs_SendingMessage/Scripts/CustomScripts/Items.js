
$(function () {
    //Type On change
    $("#TypeID").on('change', function () {
        var TypeID = $(this).val();
        var typeCode = "";
        var subTypeCode = "";
        var categoryCode = "";
        var subCategoryCode = "";
        $.ajax({
            type: "POST",
            url: location.origin + "/Items/GetTypeCode?TypeID=" + TypeID,
            data: {},
            success: function (data) {
                typeCode = data;
                //alert(data);
            }
        });

        $.ajax({
            type: "POST",
            url: location.origin + "/Items/GetSubtype?TypeID=" + TypeID,
            data: {},
            success: function (data) {
                if (data.length != 0) {
                    $("#SubtypeID").empty();
                    $.each(data, function (i, item) {
                        $("#SubtypeID").append("<option value='" + item.ID + "'>" + item.Name + "</option>");
                        subTypeCode = item.Code;
                    });

                    var SubtypeID = $("#SubtypeID").val();

                    $.ajax({
                        type: "POST",
                        url: location.origin + "/Items/GetCategory?SubtypeID=" + SubtypeID,
                        data: {},
                        success: function (data2) {
                            if (data2.length != 0) {
                                $("#CategoryID").empty();
                                $.each(data2, function (i2, item2) {
                                    $("#CategoryID").append("<option value='" + item2.ID + "'>" + item2.Name + "</option>");
                                    categoryCode = item2.Code;
                                });

                                var CategoryID = $("#CategoryID").val();

                                $.ajax({
                                    type: "POST",
                                    url: location.origin + "/Items/GetSubcategory?CategoryID=" + CategoryID,
                                    data: {},
                                    success: function (data3) {
                                        if (data3.length != 0) {
                                            $("#SubcategoryID").empty();
                                            $.each(data3, function (i3, item3) {
                                                $("#SubcategoryID").append("<option value='" + item3.ID + "'>" + item3.Name + "</option>");
                                                subCategoryCode = item3.Code;
                                            });

                                            $("#Barcode").val(typeCode + subTypeCode + categoryCode + subCategoryCode);

                                        }
                                        else {
                                            $("#SubcategoryID").empty();
                                            $("#Barcode").val("");

                                        }
                                    }
                                });

                            }
                            else {
                                $("#CategoryID").empty();
                                $("#Barcode").val("");
                            }
                        }
                    });
                }
                else {
                    $("#SubtypeID").empty();
                    $("#CategoryID").empty();
                    $("#SubcategoryID").empty();
                    $("#Barcode").empty();

                }
            }
        });


    });




    //Suptype On change
    $("#SubtypeID").on('change', function () {
        var SubtypeID = $(this).val();
        var typeCode = "";
        var subTypeCode = "";
        var categoryCode = "";
        var subCategoryCode = "";
        $.ajax({
            type: "POST",
            url: location.origin + "/Items/GetCategory?SubtypeID=" + SubtypeID,
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
                        url: location.origin + "/Items/GetSubcategory?CategoryID=" + CategoryID,
                        data: {},
                        success: function (data3) {
                            if (data3.length != 0) {
                                $("#SubcategoryID").empty();
                                $.each(data3, function (i3, item3) {
                                    $("#SubcategoryID").append("<option value='" + item3.ID + "'>" + item3.Name + "</option>");
                                });
                                $("#Barcode").val(typeCode + subTypeCode + categoryCode + subCategoryCode);
                            }
                            else {
                                $("#SubcategoryID").empty();
                                $("#Barcode").empty();
                            }
                        }
                    });


                }
                else {
                    $("#CategoryID").empty();
                    $("#SubcategoryID").empty();
                    $("#Barcode").empty();
                }
            }
        });
    });



    //Category On Change
    $("#CategoryID").on('change', function () {
        var CategoryID = $(this).val();
        var typeCode = "";
        var subTypeCode = "";
        var categoryCode = "";
        var subCategoryCode = "";
        //get type , subType and categoryCode
        $.ajax({
            type: "POST",
            url: location.origin + "/Items/GetRelatedItemCode?CategoryID=" + CategoryID,
            data: {},
            success: function (data) {
                typeCode = data.TypeCode;
                subTypeCode = data.SubTypeCode;
                categoryCode = data.CategoryCode;
            }
        });
        $.ajax({
            type: "POST",
            url: location.origin + "/Items/GetSubcategory?CategoryID=" + CategoryID,
            data: {},
            success: function (data) {
                if (data.length != 0) {
                    $("#SubcategoryID").empty();
                    $.each(data, function (i, item) {
                        $("#SubcategoryID").append("<option value='" + item.ID + "'>" + item.Name + "</option>");
                        subCategoryCode = item.Code;
                    });
                    $("#Barcode").val(typeCode + subTypeCode + categoryCode + subCategoryCode);
                }
                else {
                    $("#SubcategoryID").empty();
                    $("#Barcode").empty();
                }
            }
        });
    });


    //SubCategory On Change
    $("#SubcategoryID").on('change', function () {
        var CategoryID = $(this).val();
        var typeCode = "";
        var subTypeCode = "";
        var categoryCode = "";
        var subCategoryCode = "";
        //get type , subType and categoryCode
        $.ajax({
            type: "POST",
            url: location.origin + "/Items/GetRelatedSubCategoryCode?subCategoryID=" + CategoryID,
            data: {},
            success: function (data) {
                typeCode = data.TypeCode;
                subTypeCode = data.SubTypeCode;
                categoryCode = data.CategoryCode;
                subCategoryCode = data.SubCategoryCode;
                $("#Barcode").val(typeCode + subTypeCode + categoryCode + subCategoryCode);
            }
        });


    });

});



