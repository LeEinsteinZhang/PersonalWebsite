// JavaScript Document
var userLang = navigator.language || navigator.userLanguage; 
if (userLang.includes("zh")) {
    window.location.href = '/zh/';
} else {
    window.location.href = '/en/';
}