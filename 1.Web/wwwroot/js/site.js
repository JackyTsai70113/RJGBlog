// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

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