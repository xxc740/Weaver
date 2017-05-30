$(function () {
    $("input").iCheck({
        checkboxClass: "icheckbox_square-blue",
        radioClass: "iradio_square-blue",
        increaseArea: "20%"
    });

    if ($("#errorInfo").val()) {
        layer.tips($("#errorInfo").val(), "#btnLogin");
    }

    if (Cookies.get("weaver_username") != undefined) {
        $("input").iCheck("check");
    } else {
        $("#RememberMe").removeAttr("checked");
    }

    if ($("#RememberMe:checked").length > 0) {
        $("#UserName").val(Cookies.get("weaver_username"));
        $("#Password").val(Cookies.get("weaver_password"));
    }
});

function onSubmit() {
    if ($("#RememberMe:checked").length > 0) {
        Cookies.set("weaver_username", $("#UserName").val());
        Cookies.set("weaver_password", $("#Password").val());
    } else {
        Cookies.remove("weaver_username");
        Cookies.remove("weaver_password");
    }
}

//$(window).on("load", function () {
//    if (Cookies.get("weaver_username") != undefined) {
//        $("#RememberMe").attr("checked", "checked");
//    } else {
//        $("#RememberMe").removeAttr("checked");
//    }

//    if ($("#RememberMe:checked").length > 0) {
//        $("#UserName").val(Cookies.get("weaver_username"));
//        $("#Password").val(Cookies.get("weaver_password"));
//    }
//});