document.addEventListener("DOMContentLoaded", function () {
    var sidebar = document.getElementById("sidebar");
    var content = document.getElementById("content");
    var toggleSidebarBtn = document.getElementById("toggleSidebarBtn");

    toggleSidebarBtn.addEventListener("click", function () {
        if (sidebar.style.display !== "none") {
            sidebar.style.display = "none";
            content.classList.remove("col-md-10");
            content.classList.add("col-md-12");
            toggleSidebarBtn.style.right = "0";
        } else {
            sidebar.style.display = "block";
            content.classList.remove("col-md-12");
            content.classList.add("col-md-10");
            toggleSidebarBtn.style.right = "-40px";
        }
    });
});