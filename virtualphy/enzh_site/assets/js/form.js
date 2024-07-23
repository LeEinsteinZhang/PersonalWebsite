document.addEventListener('DOMContentLoaded', (event) => {
    document.querySelector('form').addEventListener('submit', function(e) {
        var fileInput = document.getElementById('file-upload');
        if(fileInput.files.length === 0){
            alert('Please select a file to upload.');
            e.preventDefault(); // 阻止表单提交
        }
    });
});