document.addEventListener("DOMContentLoaded", function() {
    var switchLink = document.getElementById("lang-switch");
    if (switchLink) {
        var currentUrl = window.location.href;
        var hashIndex = currentUrl.indexOf("#");
        if (hashIndex !== -1) {
            var hash = currentUrl.substring(hashIndex);
            switchLink.href += hash;
        }
    }
});
