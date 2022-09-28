// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let baseUrl = "https://localhost:1235";
let userInfo = get_cookie('X-UserName');

window.onbeforeunload = function (e) {

};
function LogOut() {
    fetch(baseUrl + "/api/User/LogOut", {
        method: 'GET',
        credentials: 'include',
        headers: {
            'Content-Type': 'application/json',
        },
    }).then(() => {
        window.location.href = "/User/Login"
    });
}



function get_cookie(name) {
    var value = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
    return value ? value[2] : null;
}