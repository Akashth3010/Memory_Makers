/* HERO SLIDER */
const slides = document.querySelectorAll('.slides');
let index = 0;

setInterval(() => {
    slides[index].classList.remove('active');
    index = (index + 1) % slides.length;
    slides[index].classList.add('active');
}, 2000);

/* FORM SUBMIT */
document.getElementById("hostForm").addEventListener("submit", function (e) {
    e.preventDefault();
    document.getElementById("successPopup").style.display = "flex";
    this.reset();
});

/* APPROVAL FLOW ANIMATION */
const observer = new IntersectionObserver(entries => {
    entries.forEach(entry => {
        const observer = new IntersectionObserver(entries => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.classList.add("show");
                } else {
                    entry.target.classList.remove("show");
                }
            });
        }, {
            threshold: 0.3
        });

        document.querySelectorAll(".step").forEach(step => observer.observe(step));
    });
});

document.querySelectorAll(".step").forEach(step => observer.observe(step));

function closePopup() {
    document.getElementById("successPopup").style.display = "none";
}