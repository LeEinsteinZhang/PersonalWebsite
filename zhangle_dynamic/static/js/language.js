document.getElementById('lang-switch').addEventListener('click', function(e) {
    var hash = window.location.hash;
    var langSwitchHref = e.currentTarget.getAttribute('href');
    if (hash) {
        e.currentTarget.setAttribute('href', langSwitchHref + hash);
    }
})