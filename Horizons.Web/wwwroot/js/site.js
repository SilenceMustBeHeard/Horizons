$(".btn-heart").on("click", function () {
    $(this).addClass("active-heart");
    setTimeout(() => $(this).removeClass("active-heart"), 300);
});
// ====== Footer Quote ======
async function fetchQuote() {
    const quotes = [
        "Travel is the only thing you buy that makes you richer.",
        "Adventure awaits. Go find it.",
        "Life is short and the world is wide.",
        "Collect moments, not things.",
        "To travel is to live."
    ];
    const randomIndex = Math.floor(Math.random() * quotes.length);
    document.getElementById("footer-quote").textContent = quotes[randomIndex];
}

document.getElementById("new-quote-btn")?.addEventListener("click", fetchQuote);
fetchQuote(); // load on page load

// ====== Smooth scroll for anchor links ======
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener("click", function (e) {
        e.preventDefault();
        document.querySelector(this.getAttribute("href"))?.scrollIntoView({
            behavior: "smooth"
        });
    });
});

// ====== Button click animation ======
document.querySelectorAll(".btn-heart").forEach(btn => {
    btn.addEventListener("click", () => {
        btn.classList.add("active-heart");
        setTimeout(() => btn.classList.remove("active-heart"), 300);
    });
});

// ====== Navbar toggle shadow on scroll ======
window.addEventListener("scroll", () => {
    const navbar = document.querySelector(".navbar");
    if (window.scrollY > 20) {
        navbar.classList.add("shadow-lg");
    } else {
        navbar.classList.remove("shadow-lg");
    }
});

// ====== Form validation highlight ======
document.querySelectorAll("form").forEach(form => {
    form.addEventListener("submit", (e) => {
        form.querySelectorAll("input, textarea, select").forEach(input => {
            if (!input.checkValidity()) {
                input.classList.add("is-invalid");
                setTimeout(() => input.classList.remove("is-invalid"), 2000);
            }
        });
    });
});

// ====== Hover scale effect (optional JS backup) ======
document.querySelectorAll(".hover-scale").forEach(el => {
    el.addEventListener("mouseenter", () => {
        el.style.transform = "translateY(-5px) scale(1.02)";
        el.style.boxShadow = "0 8px 20px rgba(0,0,0,0.2)";
    });
    el.addEventListener("mouseleave", () => {
        el.style.transform = "none";
        el.style.boxShadow = "none";
    });
});
