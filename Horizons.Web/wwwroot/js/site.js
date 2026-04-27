
// Initialize GSAP Scroll Animations
gsap.registerPlugin(ScrollTrigger);

// ====== Card Animations ======
document.querySelectorAll('.dest-card, .sport-card').forEach(card => {
    card.addEventListener('mouseenter', () => {
        gsap.to(card, {
            scale: 1.02,
            duration: 0.3,
            ease: "power2.out"
        });
    });

    card.addEventListener('mouseleave', () => {
        gsap.to(card, {
            scale: 1,
            duration: 0.3,
            ease: "power2.in"
        });
    });
});

// ====== Hero Section Animations ======
if (document.querySelector('.sport-hero')) {
    gsap.from('.hero-badge', {
        opacity: 0,
        y: -50,
        duration: 1,
        ease: "back.out(1.7)"
    });

    gsap.from('.hero-title', {
        opacity: 0,
        scale: 0.5,
        duration: 1,
        delay: 0.3,
        ease: "elastic.out(1, 0.5)"
    });

    gsap.from('.hero-subtitle', {
        opacity: 0,
        x: -100,
        duration: 1,
        delay: 0.6,
        ease: "power2.out"
    });

    gsap.from('.sport-btn', {
        opacity: 0,
        y: 50,
        duration: 0.8,
        delay: 0.9,
        stagger: 0.2
    });
}

// ====== Button Click Animations ======
document.querySelectorAll('.sport-btn, .btn-locate, .filter-tag').forEach(btn => {
    btn.addEventListener('click', function (e) {
        gsap.to(this, {
            scale: 0.95,
            duration: 0.1,
            yoyo: true,
            repeat: 1
        });
    });
});

// ====== Heart Button Animation ======
document.querySelectorAll('.btn-heart').forEach(btn => {
    btn.addEventListener('click', function () {
        //  particle effect
        for (let i = 0; i < 10; i++) {
            const particle = document.createElement('div');
            particle.innerHTML = '❤️';
            particle.style.position = 'absolute';
            particle.style.left = btn.offsetLeft + btn.offsetWidth / 2 + 'px';
            particle.style.top = btn.offsetTop + btn.offsetHeight / 2 + 'px';
            particle.style.pointerEvents = 'none';
            particle.style.zIndex = '9999';
            document.body.appendChild(particle);

            gsap.to(particle, {
                x: (Math.random() - 0.5) * 200,
                y: -Math.random() * 150,
                opacity: 0,
                scale: Math.random() * 2,
                duration: 1,
                ease: "power2.out",
                onComplete: () => particle.remove()
            });
        }

        btn.classList.add("active-heart");
        setTimeout(() => btn.classList.remove("active-heart"), 300);
    });
});

// ====== Enhanced Footer Quote with Quotes ======
const sportQuotes = [
    "Push beyond your limits. Adventure awaits.",
    "Life is either a daring adventure or nothing at all.",
    "Go where you feel most alive.",
    "Fear is just excitement without breath.",
    "The only impossible journey is the one you never begin.",
    "Escape the ordinary. Embrace the extraordinary.",
    "Your comfort zone is not your friend.",
    "Adventure: the best way to learn.",
    "Don't count the days, make the days count.",
    "Leave nothing but footprints, take nothing but memories."
];

async function fetchQuote() {
    const randomIndex = Math.floor(Math.random() * sportQuotes.length);
    const quoteElement = document.getElementById("footer-quote");

    if (quoteElement) {
        gsap.to(quoteElement, {
            opacity: 0,
            duration: 0.2,
            onComplete: () => {
                quoteElement.textContent = sportQuotes[randomIndex];
                gsap.to(quoteElement, {
                    opacity: 1,
                    duration: 0.3,
                    ease: "power2.out"
                });
            }
        });
    }
}

document.getElementById("new-quote-btn")?.addEventListener("click", fetchQuote);
fetchQuote();

// ====== Navbar Scroll Effect ======
window.addEventListener("scroll", () => {
    const navbar = document.querySelector(".sport-navbar");
    if (window.scrollY > 20) {
        gsap.to(navbar, {
            padding: "0.5rem 2rem",
            background: "rgba(10, 15, 28, 0.98)",
            duration: 0.3
        });
    } else {
        gsap.to(navbar, {
            padding: "0.8rem 2rem",
            background: "rgba(10, 15, 28, 0.95)",
            duration: 0.3
        });
    }
});

// ====== Form Validation======
document.querySelectorAll("form").forEach(form => {
    form.addEventListener("submit", (e) => {
        let hasError = false;
        form.querySelectorAll("input, textarea, select").forEach(input => {
            if (!input.checkValidity()) {
                hasError = true;
                input.classList.add("is-invalid");

                //shake animation
                gsap.to(input, {
                    x: [0, -5, 5, -5, 5, 0],
                    duration: 0.4,
                    ease: "power2.inOut"
                });

                setTimeout(() => input.classList.remove("is-invalid"), 2000);
            }
        });

        if (hasError) e.preventDefault();
    });
});

// ====== Hover Scale Effect ======
document.querySelectorAll(".hover-scale").forEach(el => {
    el.addEventListener("mouseenter", () => {
        gsap.to(el, {
            scale: 1.02,
            y: -5,
            duration: 0.3,
            ease: "power2.out"
        });
    });

    el.addEventListener("mouseleave", () => {
        gsap.to(el, {
            scale: 1,
            y: 0,
            duration: 0.3,
            ease: "power2.in"
        });
    });
});

// ====== Scroll Progress Indicator ======
const progressBar = document.createElement('div');
progressBar.style.position = 'fixed';
progressBar.style.top = '0';
progressBar.style.left = '0';
progressBar.style.height = '3px';
progressBar.style.background = 'var(--sport-gradient)';
progressBar.style.zIndex = '9999';
progressBar.style.transition = 'width 0.1s';
document.body.appendChild(progressBar);

window.addEventListener('scroll', () => {
    const winScroll = document.body.scrollTop || document.documentElement.scrollTop;
    const height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
    const scrolled = (winScroll / height) * 100;
    progressBar.style.width = scrolled + '%';
});

// ====== Parallax Effect for Hero ======
window.addEventListener('scroll', () => {
    const hero = document.querySelector('.sport-hero');
    if (hero) {
        const scrolled = window.pageYOffset;
        hero.style.backgroundPositionY = scrolled * 0.5 + 'px';
    }
});

console.log('🔥 SPORT THEME ACTIVATED - LET\'S GO ADVENTURE! 🔥');