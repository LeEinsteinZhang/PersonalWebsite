function updateHeaderBackgroundHeight() {
    // 获取header元素的高度
    var headerHeight = document.querySelector('header').offsetHeight;

    // 获取.header-background元素
    var headerBackground = document.querySelector('.header-background');

    var slideShow = document.querySelector('.slide-container');

    var offsetHeight = document.querySelector('.section-offset');

    // 设置.header-background的高度与header元素的高度相同
    headerBackground.style.height = headerHeight + 'px';

    // 设置.section-offset的padding-top与header元素的高度相同
    offsetHeight.style.paddingTop = headerHeight + 'px';
    offsetHeight.style.marginTop = '-' + headerHeight + 'px';

    // 计算SlideShow的高度并设置
    var slideShowHeight = window.innerHeight - headerHeight;
    slideShow.style.height = slideShowHeight + 'px';
}

// 当窗口加载完成时执行
window.onload = updateHeaderBackgroundHeight;

// 当窗口大小改变时也执行
window.onresize = updateHeaderBackgroundHeight;
