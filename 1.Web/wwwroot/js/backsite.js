//遮罩(Mask)
$(function () {
    var mask = document.createElement("div");
    mask.className = "mask";
    mask.setAttribute("hidden", "hidden");
    document.querySelector('body').appendChild(mask);

    $(".mask").html(
        "<div class='coffee VerticleCenter'> " +
        "<div></div>" +
        "<div></div>" +
        "<div></div>" +
        "</div>" +
        "<div class='VerticleCenter' style='position:relative;text-align:center;'>" +
        "<div style='margin-top:20px;color:gray;font-weight:bold;'>Loading...</div>" +
        "</div>"
    );
})

function showMask() {
    $(".mask").removeAttr("hidden")
}

function hideMask() {
    $(".mask").attr("hidden", "hidden")
}

//Sweet Alert
(function () {
    window.WarmAlert = function (text) {
        return Swal.fire({
            icon: 'error',
            title: '錯誤...',
            text: text,
        })
    };
})(window.WarmAlert);

(function () {
    window.SuccessAlert = function (text) {
        return Swal.fire({
            icon: 'success',
            title: '成功!',
            text: text,
        })
    };
})(window.SuccessAlert);

(function () {
    window.ConfirmAlert = function (title, parameter, fun) {
        return Swal.fire({
            icon: 'warning',
            title: title,
            showCancelButton: true,
            confirmButtonText: '確認',
            cancelButtonText: '取消',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                fun(parameter);
            }
        })
    }
})(window.ConfirmAlert);


//Ajax
function postAjaxGetAlert(postData, url) {
    $.ajax({
        url: url,
        type: "post",
        data: postData,
        success: function (result) {
            //console.log(postData)
            //console.log(result);
            if (result.statusCode == 200) {
                alert("success, " + result.message);
                //window.PTC.Loading(false);
                //swal({
                //    title: '成功!',
                //    text: result.Message,
                //    type: 'success'
                //}, function () {
                //    history.back();
                //    return;
                //});
            }
            else {
                alert("fail, " + result.message);
                //window.PTC.Loading(false);
                //sweetAlert("失敗!", result.Message, "error");
            }
        }
    })
}