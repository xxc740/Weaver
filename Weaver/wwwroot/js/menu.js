var selectedMenuId = "00000000-0000-0000-0000-000000000000";

$(function () {
    $("#btnAddRoot").click(function () { add(0); });
    $("#btnAdd").click(function () { add(1); });
    $("#btnSave").click(function () { save(); });
    $("#btnDelete").click(function () { deletemulti(); });
    $("#btnLoadRoot").click(function () {
        selectedMenuId = "00000000-0000-0000-0000-000000000000";
        loadTables(1, 10);
    });
    $("#checkAll").click(function () { checkAll(this) });
    initTree();
});

function initTree() {
    $.jstree.destroy();
    $.ajax({
        type: "Get",
        url: "/Menu/GetMenuTreeData?_t="+ new Date().getTime(),
        success: function (data) {
            $("TreeDiv").jstree({
                "core": {
                    "data": data,
                    "multiple": false
                },
                "plugins":["state","types","wholerow"]
            });
            $("#TreeDiv").on("ready.jstree", function (e,data) {
                data.instance.open_all();
            });
            $("#TreeDiv").on("changed.jstree", function (e,data) {
                var node = data.instance.get_node(data.selected[0]);
                if (node) {
                    selectedMenuId = node.id;
                    loadTables(1, 10);
                }
            });
        }
    });
}

function loadTables(startPage, pageSize) {
    $("#tableBody").html("");
    $("#checkAll").prop("checked", false);
    $.ajax({
        type: "Get",
        url: "/Menu/GetMenuByParent?parentId=" + selectedMenuId + "&startPage=" + startPage + "&pageSize=" + pageSize + "&_t=" + new Date().getTime(),
        success: function (data) {
            $.each(data.rows, function (i, item) {
                var tr = "<tr>";
                tr += "<td align='center'><input type='checkbox' class='checkboxs' value='" + item.id + "'/></td>";
                tr += "<td>" + item.name + "</td>";
                tr += "<td>" + (item.code == null ? "" : item.code) + "</td>";
                tr += "<td>" + (item.type == 0 ? "Feature Menu" : "Operation") + "</td>";
                tr += "<td>" + (item.remarks == null ? "" : item.remarks) + "</td>";
                tr += "<td><button class='btn btn-info btn-xs' href='javascript:;' onclick='edit(\"" + item.id + "\")'><i class='fa fa-edit'></i> 编辑 </button> <button class='btn btn-danger btn-xs' href='javascript:;' onclick='deleteSingle(\"" + item.id + "\")'><i class='fa fa-trash-o'></i> 删除 </button> </td>"
                tr += "</tr>";
                $("#tableBody").append(tr);
            });

            var element = $("$grid_paging_part");
            if (data.rowCount > 0) {
                var options = {
                    bootstrapMajorVersion: 4,
                    currentPage: startPage,
                    numberOfPages: data.rowCount,
                    totalPages: data.pageCount,
                    onPageChanged: function (event, oldPage, newPage) {
                        loadTables(newPage, pageSize);
                    }
                }
                element.bootstrapPaginator(options);
            }
        }
    });
}

function checkAll(obj) {
    $(".checkboxs").each(function () {
        if (obj.checked == true) {
            $(this).prop("checked", true);
        }
        else {
            $(this).prop("checked", false);
        }
    });
}

function add(type) {
    if (type === 1) {
        if (selectedMenuId === "00000000-0000-0000-0000-000000000000") {
            layer.alert("please choose feature !");
            return;
        }
        $("#ParentId").val(selectedMenuId);
    }
    else {
        $("#ParentId").val("00000000-0000-0000-0000-000000000000");
    }

    $("#Id").val("00000000-0000-0000-0000-000000000000");
    $("#Code").val("");
    $("#Name").val("");
    $("#Type").val(0);
    $("#Url").val("");
    $("#Icon").val("");
    $("#SerialNumber").val(0);
    $("#Remarks").val("");
    $("#title").text("Add New Root");
    $("#addRootModal").modal("show");
}

function edit(id) {
    $.ajax({
        type: "Get",
        url: "/Department/Get?id=" + id + "&_t=" + new Date().getTime(),
        success: function (data) {
            $("#Id").val(data.id);
            $("#ParentId").val(data.parentId);
            $("#Name").val(data.name);
            $("#Code").val(data.code);
            $("#Type").val(data.type);
            $("#Url").val(data.url);
            $("#Icon").val(data.icon);
            $("#SerialNumber").val(data.serialNumber);
            $("#Remarks").val("");
            $("#title").text("Edit Feature");
            $("#addRootModal").modal("show");
        }
    });
}

function save() {
    var id = $("#Id").val();
    var parentId = ("#ParentId").val();
    var name = ("#Name").val();
    var code = ("#Code").val();
    var type = $("#Type").val();
    var url = $("#Url").val();
    var icon = $("#Icon").val();
    var serialNumber = $("#SerialNumber").val();
    var remarks = $("#Remarks").val(); 

    var postData = {
        "data":
        {
            "Id": id,
            "ParentId": parentId,
            "Name": name,
            "Code": code,
            "Type": type,
            "Url": url,
            "Icon": icon,
            "SerialNumber": serialNumber,
            "Remarks": remarks
        }
    };

    $.ajax({
        type: "Post",
        url: "/Menu/Edit",
        data: postData,
        success: function (data) {
            if (data.result == "Success") {
                initTree();
                $("#addRootModal").modal("hide");
            } else {
                layer.tips(data.message, "btnSave");
            };
        }
    });
}

function deleteMulti() {
    var ids = "";
    $(".checkboxs").each(function () {
        if ($(this).prop("checked") == true) {
            ids += $(this).val() + ",";
        }
    });

    ids = ids.substring(0, ids.length - 1);
    if (ids.length == 0) {
        layer.alert("please choose delete item");
        return;
    }

    layer.confirm("Are you sure you want to delete the selected record?", {
        btn:["Sure","Cancel"]
    }, function () {
        var sendData = { "ids": ids };
        $.ajax({
            type: "Post",
            url: "/Menu/DeleteMuti",
            data: sendData,
            success: function (data) {
                if (data.result == "Success") {
                    initTree();
                    layer.closeAll();
                } else {
                    layer.alert("Delete Failure !");
                }
            }
        });
    });
}

function deleteSingle(id) {
    layer.confirm("Are you sure you want to delete the selected record?", {
        btn: ["Sure", "Cancel"]
    }, function () {
        $.ajax({
            type: "POST",
            url: "/Menu/Delete",
            data: { "id": id },
            success: function (data) {
                if (data.result == "Success"){
                    initTree();
                    layer.closeAll();
                } else {
                    layer.alert("Delete Failure !");
                }
            }
        });
    });
}
