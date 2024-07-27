document.addEventListener("DOMContentLoaded", function() {
    // Dropdown menu functionality
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

    // Update header background height
    function updateHeaderBackgroundHeight() {
        var headerHeight = document.querySelector('header').offsetHeight;
        var headerBackground = document.querySelector('.header-background');
        var slideShow = document.querySelector('.slide-container');
        var offsetHeight = document.querySelector('.section-offset');

        headerBackground.style.height = headerHeight + 'px';
        offsetHeight.style.paddingTop = headerHeight + 'px';
        offsetHeight.style.marginTop = '-' + headerHeight + 'px';
        var slideShowHeight = window.innerHeight - headerHeight;
        slideShow.style.height = slideShowHeight + 'px';
    }

    window.onload = updateHeaderBackgroundHeight;
    window.onresize = updateHeaderBackgroundHeight;

    // Language switch functionality
    document.getElementById('lang-switch').addEventListener('click', function(e) {
        var hash = window.location.hash;
        var langSwitchHref = e.currentTarget.getAttribute('href');
        if (hash) {
            e.currentTarget.setAttribute('href', langSwitchHref + hash);
        }
    });

    // Slideshow functionality
    let slideIndex = 0;

    function showSlides() {
        const slides = document.getElementsByClassName("background-slide");
        for (let i = 0; i < slides.length; i++) {
            slides[i].style.opacity = "0";
        }
        slideIndex++;
        if (slideIndex > slides.length) {
            slideIndex = 1;
        }
        slides[slideIndex - 1].style.opacity = "1";
        setTimeout(showSlides, 5000);
    }

    showSlides();

    // Set back button link
    var backButton = document.getElementById('back-button');
    if (backButton) {
        backButton.href = document.referrer;
    }
});
