var dropdownBtn = document.querySelector(".dropdown-btn");
var dropdownContent = document.querySelector(".dropdown-content");
var dropdownLinks = document.querySelectorAll(".dropdown-content a");

dropdownBtn.addEventListener("click", function(event) {
    dropdownContent.classList.toggle("show");
    event.stopPropagation();
});

window.addEventListener("click", function(event) {
    if (!event.target.matches('.dropdown-btn') && !event.target.matches('.dropdown-content *')) {
        if (dropdownContent.classList.contains('show')) {
            dropdownContent.classList.remove('show');
        }
    }
});

dropdownLinks.forEach(function(link) {
    link.addEventListener("click", function() {
        dropdownContent.classList.remove("show");
    });
});
