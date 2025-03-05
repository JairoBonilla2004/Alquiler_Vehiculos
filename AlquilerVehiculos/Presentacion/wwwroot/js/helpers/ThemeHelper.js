document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById("toggleTheme");
    const themeIcon = document.getElementById("themeIcon");
    const htmlElement = document.documentElement;

    const savedTheme = localStorage.getItem("theme") || "light";
    htmlElement.setAttribute("data-bs-theme", savedTheme);
    themeIcon.textContent = savedTheme === "light" ? "🌙" : "☀️";

    toggleButton.addEventListener("click", function () {
        const currentTheme = htmlElement.getAttribute("data-bs-theme");
        const newTheme = currentTheme === "light" ? "dark" : "light";

        htmlElement.setAttribute("data-bs-theme", newTheme);
        localStorage.setItem("theme", newTheme);
        themeIcon.textContent = newTheme === "light" ? "🌙" : "☀️";
    });
});