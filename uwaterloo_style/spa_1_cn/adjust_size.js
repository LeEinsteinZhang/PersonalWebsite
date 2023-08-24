const element = document.getElementById('resizableElement');
const initialHeight = window.innerHeight; // 100vh 的像素值
const targetHeight = 1000;  // 目标高度

window.addEventListener('scroll', function() {
    // 获取页面的滚动高度
    let scrollY = window.scrollY;

    // 计算新高度。将滚动的高度从总高度中减去，但确保不小于目标高度
    let newHeight = Math.max(targetHeight, initialHeight - scrollY);

    // 应用新高度
    element.style.height = `${newHeight}px`;
});
