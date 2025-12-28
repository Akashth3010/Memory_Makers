// Banaras page JS - place in wwwroot/js/Banaras.js
document.addEventListener('DOMContentLoaded', function () {
    const main = document.getElementById('mainImage');
    const thumbs = document.querySelectorAll('.thumb');
    const lightbox = document.getElementById('lightbox');
    const lightboxImg = document.getElementById('lightboxImg');

    function setMain(src) {
        if (!main) return;
        main.style.backgroundImage = `url('${src}')`;
        if (lightboxImg) lightboxImg.src = src;
    }

    thumbs.forEach(t => {
        t.addEventListener('click', (e) => {
            if (e && typeof e.preventDefault === 'function') e.preventDefault();
            thumbs.forEach(x => x.classList.remove('active'));
            t.classList.add('active');
            setMain(t.dataset.src);
        });

        t.addEventListener('keydown', (e) => {
            if (e.key === 'Enter' || e.key === ' ') {
                e.preventDefault();
                t.click();
            }
        });
    });

    if (main) {
        main.addEventListener('click', (e) => {
            if (e && typeof e.preventDefault === 'function') e.preventDefault();
            if (lightbox) {
                lightbox.style.display = 'flex';
                lightbox.setAttribute('aria-hidden', 'false');
            }
        });
    }

    if (lightbox) {
        lightbox.addEventListener('click', (e) => {
            if (e && typeof e.preventDefault === 'function') e.preventDefault();
            lightbox.style.display = 'none';
            lightbox.setAttribute('aria-hidden', 'true');
        });
    }

    // Preload gallery images
    thumbs.forEach(t => {
        const img = new Image();
        img.src = t.dataset.src;
    });

    // Itinerary accordion
    const headers = document.querySelectorAll('.day-header');
    headers.forEach(h => {
        h.addEventListener('click', () => {
            const body = h.nextElementSibling;
            const opened = body && body.style.display === 'block';
            document.querySelectorAll('.day-body').forEach(d => d.style.display = 'none');
            document.querySelectorAll('.day-header .toggle').forEach(t => t.textContent = '+');

            if (body && !opened) {
                body.style.display = 'block';
                h.querySelector('.toggle').textContent = '−';
            } else if (body) {
                body.style.display = 'none';
                h.querySelector('.toggle').textContent = '+';
            }
        });
    });

    // Pricing calculation based on guests
    const guests = document.getElementById('guests');
    const pricePerPersonElm = document.getElementById('pricePerPerson');
    const totalPayableElm = document.getElementById('totalPayable');
    const priceSummary = document.getElementById('priceSummary');

    const basePrice = 12000;
    const taxes = 1200;

    function updatePrice() {
        if (!guests) return;
        const g = parseInt(guests.value, 10) || 1;
        const total = basePrice * g + taxes;
        if (pricePerPersonElm) pricePerPersonElm.textContent = `₹${basePrice.toLocaleString()} / person`;
        if (totalPayableElm) totalPayableElm.textContent = `₹${total.toLocaleString()}`;
        if (priceSummary) priceSummary.textContent = `${g} ${g > 1 ? 'persons' : 'person'}`;
    }

    if (guests) {
        guests.addEventListener('change', updatePrice);
        updatePrice();
    }

    // Simple client-side form validation
    const bookingForm = document.getElementById('bookingForm');
    if (bookingForm) {
        bookingForm.addEventListener('submit', function (e) {
            const date = document.getElementById('travelDate').value;
            if (!date) {
                e.preventDefault();
                alert('Please select a travel date.');
                return;
            }
        });
    }
});