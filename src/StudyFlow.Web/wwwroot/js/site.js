// StudyFlow ortak arayüz etkileşimleri (mobil menü klavye ve aria-expanded desteği)
document.addEventListener("DOMContentLoaded", function () {
    var navPanel = document.getElementById("sfPrimaryNav");
    var toggleBtn = document.getElementById("sfMobileMenuToggle");

    if (!navPanel || !toggleBtn) {
        return;
    }

    navPanel.addEventListener("shown.bs.collapse", function () {
        toggleBtn.setAttribute("aria-expanded", "true");
    });

    navPanel.addEventListener("hidden.bs.collapse", function () {
        toggleBtn.setAttribute("aria-expanded", "false");
    });

    document.addEventListener("keydown", function (event) {
        if (event.key === "Escape" && navPanel.classList.contains("show")) {
            toggleBtn.click();
            toggleBtn.focus();
        }
    });
});
