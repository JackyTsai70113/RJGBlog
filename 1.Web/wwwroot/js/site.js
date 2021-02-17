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
    window.ConfirmAlert = function (title, text) {
        return Swal.fire({
            icon: 'warning',
            title: title,
            showCancelButton: true,
            confirmButtonText: '確認',
            cancelButtonText: '取消',
            reverseButtons: true
        }).then((result) => {
            if (result.isConfirmed) {
                Swal.fire(
                    '成功',
                    text,
                    'success'
                )
            }
        })
    }
})(window.ConfirmAlert);

// extend range validator method to treat checkboxes differently
var defaultRangeValidator = $.validator.methods.range;
$.validator.methods.range = function (value, element, param) {
    if (element.type === 'checkbox') {
        // if it's a checkbox return true if it is checked
        return element.checked;
    } else {
        // otherwise run the default validation function
        return defaultRangeValidator.call(this, value, element, param);
    }
}


//Ajax
function postAjaxGetAlert(postData,url) {
    $.ajax({
        url: url,
        type: "post",
        data: postData,
        success: function (result) {
            //console.log(postData)
            //console.log(result);
            if (result.statusCode == 200) {
                alert(result.message);
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
                alert(result.message);
                //window.PTC.Loading(false);
                //sweetAlert("失敗!", result.Message, "error");
            }
        }
    })
}