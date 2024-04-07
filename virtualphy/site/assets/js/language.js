// JavaScript Document
var userLang = navigator.language || navigator.userLanguage; 
if (userLang.includes("zh")) {
    window.location.href = '/cn/';
} else {
    window.location.href = '/en/';
}