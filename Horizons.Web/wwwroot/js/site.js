//  INITIALIZATION 
document.addEventListener('DOMContentLoaded', function () {
    initAnimations();
    initScrollProgress();
    initFooterQuote();
    initFormValidation();
    initHoverEffects();
    initHeartButtons();
});

//  GSAP Registration 
gsap.registerPlugin(ScrollTrigger);

//  DEBOUNCE HELPER (prevents excessive function calls) 
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

//  THROTTLE HELPER 
function throttle(func, limit) {
    let inThrottle;
    return function (...args) {
        if (!inThrottle) {
            func.apply(this, args);
            inThrottle = true;
            setTimeout(() => inThrottle = false, limit);
        }
    };
}

//  CARD ANIMATIONS 
function initAnimations() {
    // Use CSS transitions instead of GSAP for better performance
    const cards = document.querySelectorAll('.dest-card, .sport-card');
    cards.forEach(card => {
        card.style.transition = 'transform 0.3s ease';
        card.addEventListener('mouseenter', () => {
            card.style.transform = 'scale(1.02)';
        });
        card.addEventListener('mouseleave', () => {
            card.style.transform = 'scale(1)';
        });
    });
}

//  HEART BUTTON ANIMATION 
function initHeartButtons() {
    const heartBtns = document.querySelectorAll('.btn-heart');
    if (heartBtns.length === 0) return;

    heartBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            e.preventDefault();

            // Simple CSS animation instead of GSAP particles
            this.classList.add('active-heart');
            setTimeout(() => this.classList.remove('active-heart'), 300);

            // Submit form after animation
            const form = this.closest('form');
            if (form) setTimeout(() => form.submit(), 150);
        });
    });
}

//  FOOTER QUOTE 
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

let currentQuoteIndex = Math.floor(Math.random() * sportQuotes.length);

function initFooterQuote() {
    const quoteElement = document.getElementById("footer-quote");
    const newQuoteBtn = document.getElementById("new-quote-btn");

    if (quoteElement) {
        quoteElement.textContent = sportQuotes[currentQuoteIndex];
    }

    if (newQuoteBtn) {
        newQuoteBtn.addEventListener("click", () => {
            let newIndex;
            do {
                newIndex = Math.floor(Math.random() * sportQuotes.length);
            } while (newIndex === currentQuoteIndex && sportQuotes.length > 1);

            currentQuoteIndex = newIndex;

            if (quoteElement) {
                quoteElement.style.opacity = '0';
                setTimeout(() => {
                    quoteElement.textContent = sportQuotes[currentQuoteIndex];
                    quoteElement.style.opacity = '1';
                }, 150);
            }
        });
    }
}

//  NAVBAR SCROLL EFFECT 
const navbar = document.querySelector(".sport-navbar");
if (navbar) {
    const handleScroll = throttle(() => {
        if (window.scrollY > 20) {
            navbar.style.padding = "0.5rem 2rem";
            navbar.style.background = "rgba(10, 15, 28, 0.98)";
        } else {
            navbar.style.padding = "0.8rem 2rem";
            navbar.style.background = "rgba(10, 15, 28, 0.95)";
        }
    }, 50);

    window.addEventListener("scroll", handleScroll);
}

//  SCROLL PROGRESS INDICATOR 
function initScrollProgress() {
    const progressBar = document.createElement('div');
    progressBar.style.cssText = `
        position: fixed;
        top: 0;
        left: 0;
        width: 0%;
        height: 3px;
        background: var(--sport-gradient);
        z-index: 9999;
        transition: width 0.05s linear;
        pointer-events: none;
    `;
    document.body.appendChild(progressBar);

    const updateProgress = throttle(() => {
        const winScroll = document.documentElement.scrollTop;
        const height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
        const scrolled = height > 0 ? (winScroll / height) * 100 : 0;
        progressBar.style.width = scrolled + '%';
    }, 30);

    window.addEventListener('scroll', updateProgress);
}

//  FORM VALIDATION 
function initFormValidation() {
    const forms = document.querySelectorAll("form");
    if (forms.length === 0) return;

    forms.forEach(form => {
        form.addEventListener("submit", (e) => {
            let hasError = false;
            const inputs = form.querySelectorAll("input, textarea, select");

            inputs.forEach(input => {
                if (!input.checkValidity()) {
                    hasError = true;
                    input.classList.add("is-invalid");

                    // Simple CSS animation instead of GSAP
                    input.style.transform = 'translateX(0)';
                    input.style.transition = 'transform 0.2s ease';
                    input.style.transform = 'translateX(5px)';
                    setTimeout(() => {
                        if (input) input.style.transform = '';
                    }, 200);

                    setTimeout(() => input.classList.remove("is-invalid"), 2000);
                }
            });

            if (hasError) e.preventDefault();
        });
    });
}

//  HOVER SCALE EFFECT (CSS only) 
function initHoverEffects() {
    const hoverElements = document.querySelectorAll(".hover-scale");
    if (hoverElements.length === 0) return;

    hoverElements.forEach(el => {
        el.style.transition = 'transform 0.3s ease, box-shadow 0.3s ease';

        el.addEventListener('mouseenter', () => {
            el.style.transform = 'translateY(-5px) scale(1.02)';
            el.style.boxShadow = '0 8px 20px rgba(0,0,0,0.2)';
        });

        el.addEventListener('mouseleave', () => {
            el.style.transform = 'none';
            el.style.boxShadow = 'none';
        });
    });
}

//  HERO SECTION ANIMATION 
const heroSection = document.querySelector('.sport-hero, .welcome-section');
if (heroSection) {
    const heroBadge = document.querySelector('.hero-badge');
    const heroTitle = document.querySelector('.hero-title, .welcome-section h1');
    const heroSubtitle = document.querySelector('.hero-subtitle, .welcome-section .lead');
    const heroBtn = document.querySelector('.sport-btn, .welcome-section .btn');

    if (heroBadge) heroBadge.style.opacity = '1';
    if (heroTitle) heroTitle.style.opacity = '1';
    if (heroSubtitle) heroSubtitle.style.opacity = '1';
    if (heroBtn) heroBtn.style.opacity = '1';
}

//  PARALLAX EFFECT (disabled by default) 
// Uncomment only if needed and use throttling
/*
const hero = document.querySelector('.sport-hero');
if (hero) {
    const parallaxScroll = throttle(() => {
        const scrolled = window.pageYOffset;
        hero.style.backgroundPositionY = scrolled * 0.3 + 'px';
    }, 50);
    window.addEventListener('scroll', parallaxScroll);
}
*/

console.log('🏔️ Horizons - Adventure theme loaded');