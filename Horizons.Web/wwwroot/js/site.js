

document.addEventListener('DOMContentLoaded', function () {
    initNavbarScroll();
    initScrollProgress();
    initFooterQuote();
    initHeartButtons();
    initImageFadeIn();
    initMobileMenu();
});

//  UTILITIES 
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

//  NAVBAR SCROLL EFFECT 
function initNavbarScroll() {
    const navbar = document.querySelector('.navbar');
    if (!navbar) return;

    const handleScroll = throttle(() => {
        if (window.scrollY > 20) {
            navbar.style.padding = '0.5rem 2rem';
            navbar.style.boxShadow = '0 2px 10px rgba(0,0,0,0.05)';
        } else {
            navbar.style.padding = '1rem 2rem';
            navbar.style.boxShadow = 'none';
        }
    }, 50);

    window.addEventListener('scroll', handleScroll);
}

//  SCROLL PROGRESS 
function initScrollProgress() {
    const progressBar = document.createElement('div');
    progressBar.className = 'scroll-progress';
    progressBar.style.cssText = `
        position: fixed;
        top: 0;
        left: 0;
        width: 0%;
        height: 3px;
        background: var(--accent-primary);
        z-index: 1001;
        transition: width 0.1s linear;
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

//  FOOTER QUOTE 
const travelQuotes = [
    "Not all those who wander are lost.",
    "The world is a book, and those who do not travel read only one page.",
    "Adventure is worthwhile in itself.",
    "Take only memories, leave only footprints.",
    "Travel far enough to meet yourself.",
    "Wherever you go becomes a part of you somehow.",
    "Life is either a daring adventure or nothing at all.",
    "Wander often, wonder always.",
    "Collect moments, not things.",
    "The journey is the destination."
];

let currentQuoteIndex = Math.floor(Math.random() * travelQuotes.length);

function initFooterQuote() {
    const quoteElement = document.getElementById('footer-quote');
    const newQuoteBtn = document.getElementById('new-quote-btn');

    if (quoteElement) {
        quoteElement.textContent = travelQuotes[currentQuoteIndex];
    }

    if (newQuoteBtn) {
        newQuoteBtn.addEventListener('click', () => {
            let newIndex;
            do {
                newIndex = Math.floor(Math.random() * travelQuotes.length);
            } while (newIndex === currentQuoteIndex && travelQuotes.length > 1);

            currentQuoteIndex = newIndex;

            if (quoteElement) {
                quoteElement.style.opacity = '0';
                setTimeout(() => {
                    quoteElement.textContent = travelQuotes[currentQuoteIndex];
                    quoteElement.style.opacity = '1';
                }, 150);
            }
        });
    }
}

//  HEART BUTTONS (Favorites) 
function initHeartButtons() {
    const heartBtns = document.querySelectorAll('.btn-heart, .favorite-btn');

    heartBtns.forEach(btn => {
        btn.addEventListener('click', function (e) {
            if (this.tagName === 'BUTTON' || this.tagName === 'A') {
                // Let the form submit normally, just add animation
                this.classList.add('active');
                setTimeout(() => this.classList.remove('active'), 300);
            }
        });
    });
}

//  IMAGE FADE-IN ON LOAD 
function initImageFadeIn() {
    const images = document.querySelectorAll('img');

    images.forEach(img => {
        if (img.complete) {
            img.style.opacity = '1';
        } else {
            img.style.opacity = '0';
            img.addEventListener('load', () => {
                img.style.transition = 'opacity 0.3s ease';
                img.style.opacity = '1';
            });
        }
    });
}

//  MOBILE MENU IMPROVEMENTS 
function initMobileMenu() {
    const toggler = document.querySelector('.navbar-toggler');
    const collapse = document.querySelector('.navbar-collapse');

    if (toggler && collapse) {
        // Close menu when clicking outside on mobile
        document.addEventListener('click', (e) => {
            if (window.innerWidth < 992) {
                if (!collapse.contains(e.target) && !toggler.contains(e.target)) {
                    const bsCollapse = bootstrap.Collapse.getInstance(collapse);
                    if (bsCollapse && collapse.classList.contains('show')) {
                        bsCollapse.hide();
                    }
                }
            }
        });
    }
}
//  SMOOTH SCROLL 
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            e.preventDefault();
            target.scrollIntoView({
                behavior: 'smooth',
                block: 'start'
            });
        }
    });
});

//  FORM VALIDATION 
function initFormValidation() {
    const forms = document.querySelectorAll('form');

    forms.forEach(form => {
        form.addEventListener('submit', (e) => {
            let hasError = false;
            const inputs = form.querySelectorAll('input[required], textarea[required], select[required]');

            inputs.forEach(input => {
                if (!input.value.trim()) {
                    hasError = true;
                    input.classList.add('is-invalid');

                    // Add shake animation
                    input.style.transform = 'translateX(0)';
                    input.style.transform = 'translateX(4px)';
                    setTimeout(() => {
                        if (input) input.style.transform = '';
                    }, 200);

                    setTimeout(() => input.classList.remove('is-invalid'), 2000);
                } else {
                    input.classList.remove('is-invalid');
                }
            });

            if (hasError) e.preventDefault();
        });

        // Real-time validation
        const inputs = form.querySelectorAll('input[required], textarea[required]');
        inputs.forEach(input => {
            input.addEventListener('input', function () {
                if (this.value.trim()) {
                    this.classList.remove('is-invalid');
                }
            });
        });
    });
}

// Initialize validation when dynamic content loads
document.addEventListener('DOMContentLoaded', initFormValidation);

//  STATS COUNTER ANIMATION 
function animateNumber(element, start, end, duration) {
    if (!element) return;
    const range = end - start;
    const increment = range / (duration / 16);
    let current = start;
    const timer = setInterval(() => {
        current += increment;
        if (current >= end) {
            clearInterval(timer);
            element.textContent = Math.round(end).toLocaleString();
        } else {
            element.textContent = Math.round(current).toLocaleString();
        }
    }, 16);
}

// Expose to global scope
window.animateNumber = animateNumber;

console.log('✨ Wanderlog - Modern Travel Blog Loaded');