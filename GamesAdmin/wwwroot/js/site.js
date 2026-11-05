// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
document.querySelectorAll(".image-upload").forEach(input => {
    input.addEventListener("change", function (event) {
        const preview = document.getElementById(input.dataset.preview);
        const file = event.target.files[0];

        if (file && preview) {
            
            const reader = new FileReader();

            //Do onload (display image)
            reader.onload = function (e) {      
                preview.src = e.target.result;
                preview.style.display = "block";
            };
            //This does the loading, which triggers the above
            reader.readAsDataURL(file)
        }
    })
})
// Write your JavaScript code.
