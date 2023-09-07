const element = document.getElementById('resizableElement');
const initialHeight = window.innerHeight;
const targetHeight = 1000;

window.addEventListener('scroll', function() {
    let scrollY = window.scrollY;
    let newHeight = Math.max(targetHeight, initialHeight - scrollY);
    element.style.height = `${newHeight}px`;
});
