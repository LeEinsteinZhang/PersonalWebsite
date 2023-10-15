// JavaScript Document

document.addEventListener("DOMContentLoaded", function() {
    const headers = document.querySelectorAll('.accordion-header');
    headers.forEach(header => {
        header.addEventListener('click', function() {
            const content = this.nextElementSibling;
            const arrow = this.querySelector('.arrow');
            
            if(content.style.maxHeight !== '0px') {
                content.style.maxHeight = '0px'; // 折叠内容
            } else {
                content.style.maxHeight = '1000px'; // 展开内容
            }
            
            // 切换箭头的方向
            arrow.classList.toggle('up');
        });
    });
});
