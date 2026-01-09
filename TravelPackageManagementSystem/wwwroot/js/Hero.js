function handleFullSearch() {
    // 1. Get values from all three columns
    const destination = document.getElementById('destInput').value.toLowerCase().trim();
    const travelDate = document.getElementById('dateInput').value;
    const travellers = document.getElementById('travellerInput').value;

    // 2. Validation: Ensure destination is entered
    if (destination === "") {
        alert("Please enter a destination to search.");
        return;
    }

    // 3. Logic for Redirection or Filtering
    console.log(`Searching for: ${destination}, Date: ${travelDate}, Guests: ${travellers}`);

    if (destination.includes("meghalaya")) {
        // You can also pass these values to the next page via URL parameters
        window.location.href = `/Home/MeghalayaTD?date=${travelDate}&guests=${travellers}`;
    } else {
        alert("We currently only have packages available for 'Meghalaya'.");
    }
}
// Run this as soon as the page loads
document.addEventListener("DOMContentLoaded", function () {
    const dateInput = document.getElementById('dateInput');

    // Get today's date in YYYY-MM-DD format
    const today = new Date();
    const dd = String(today.getDate()).padStart(2, '0');
    const mm = String(today.getMonth() + 1).padStart(2, '0'); // January is 0!
    const yyyy = today.getFullYear();

    const formattedToday = yyyy + '-' + mm + '-' + dd;

    // Set the 'min' attribute to today's date
    dateInput.min = formattedToday;

    // Optional: Default the calendar to today's date
    dateInput.value = formattedToday;
});
// Search panel end here //
function handleAuthToggle() {
    const signupFields = document.querySelectorAll('.signup-only');
    const modalTitle = document.getElementById('modalTitle');
    const modalSubtitle = document.getElementById('modalSubtitle');
    const submitBtn = document.getElementById('submitBtn');
    const toggleText = document.getElementById('toggleText');
    const toggleLink = document.getElementById('toggleAuth');

    // Use a data attribute or check the button text more reliably
    const isCurrentlySignup = submitBtn.innerText.includes("Sign Up");

    if (isCurrentlySignup) {
        // --- SWITCH TO LOGIN ---
        signupFields.forEach(f => f.style.setProperty('display', 'none', 'important'));
        modalTitle.innerText = "Welcome Back";
        modalSubtitle.innerText = "Log in to access your account.";
        submitBtn.innerText = "Login →";
        toggleText.innerText = "Don't have an account?";
        toggleLink.innerText = "Sign Up";
    } else {
        // --- SWITCH TO SIGNUP ---
        signupFields.forEach(f => f.style.setProperty('display', 'block', 'important'));
        modalTitle.innerText = "Create an Account";
        modalSubtitle.innerText = "Join EasySafar to start your journey.";
        submitBtn.innerText = "Sign Up →";
        toggleText.innerText = "Already have an account?";
        toggleLink.innerText = "Login";
    }
}

document.getElementById('authForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    const isLogin = document.getElementById('submitBtn').innerText.includes("Login");

    // Values from HTML
    const emailValue = document.getElementById('authEmail').value;
    const passwordValue = document.getElementById('authPassword').value;
    const regUsernameValue = document.getElementById('regUsername').value;
    const confirmPasswordValue = document.getElementById('authConfirmPassword').value;

    let payload = {};
    let endpoint = isLogin ? '/Account/Login' : '/Account/Register';

    if (isLogin) {
        payload = {
            Username: emailValue, // LoginViewModel expects 'Username'
            Password: passwordValue
        };
    } else {
        // Validation: Passwords must match before sending to server
        if (passwordValue !== confirmPasswordValue) {
            document.getElementById('err-ConfirmPassword').innerText = "Passwords do not match";
            return;
        }

        payload = {
            Username: regUsernameValue, // Must match User.cs or RegisterViewModel property
            Email: emailValue,
            Password: passwordValue,
            ConfirmPassword: confirmPasswordValue
        };
    }

    try {
        const response = await fetch(endpoint, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(payload)
        });

        if (response.ok) {
            alert(isLogin ? "Welcome back!" : "Registration Successful!");
            window.location.reload(); // Refresh to update the UI with Session data
        } else {
            const errorData = await response.json();
            displayErrors(errorData);
        }
    } catch (error) {
        console.error("Critical Auth Error:", error);
    }
});

function displayErrors(errors) {
    // Clear previous errors first
    document.querySelectorAll('.err-msg').forEach(el => el.innerText = "");

    Object.keys(errors).forEach(key => {
        // Find the span with ID err-Username, err-Email, etc.
        const span = document.getElementById(`err-${key}`);
        if (span) {
            // Handle both array of strings (ModelState) or single string
            const message = Array.isArray(errors[key]) ? errors[key][0] : errors[key];
            span.innerText = message;
        }
    });
}
	});

// Glow effect logic
document.addEventListener('DOMContentLoaded', function () {

    const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
    const authSection = document.getElementById('authSection');
    const user = JSON.parse(localStorage.getItem('user'));

    if (isLoggedIn && user) {
        // Clear the "Sign in" link and add the Name + separate Logout button
        authSection.innerHTML = `
			<span class="text-white fw-bold me-3">
				<i class="fa-solid fa-user-circle me-1"></i>Hi, ${user.firstName}
			</span>
			<button id="logoutBtn" class="btn btn-sm btn-outline-light" style="border-radius: 20px; font-size: 0.8rem;">
				Logout <i class="fa-solid fa-right-from-bracket ms-1"></i>
			</button>
		`;

        // Attach event listener specifically to the new Logout button
        document.getElementById('logoutBtn').addEventListener('click', function () {
            if (confirm("Are you sure you want to logout?")) {
                localStorage.removeItem('isLoggedIn');
                window.location.reload();
            }
        });
    }

    // Track movement on the entire document for maximum responsiveness
    document.addEventListener('mousemove', (e) => {
        // Only run the logic if the modal is actually open/visible
        const authModal = document.getElementById('authModal');
        if (!authModal || !authModal.classList.contains('show')) return;

        const containers = authModal.querySelectorAll('.input-glow-container');

        containers.forEach(container => {
            const rect = container.getBoundingClientRect();

            // Precise coordinate calculation
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            container.style.setProperty('--mouse-x', `${x}px`);
            container.style.setProperty('--mouse-y', `${y}px`);
        });
    });
});

document.addEventListener("DOMContentLoaded", function () {
    const holidayLink = document.querySelector('a[href="#top-destinations"]');

    if (holidayLink) {
        holidayLink.addEventListener("click", function (e) {
            e.preventDefault(); // Stop the "jump"

            const target = document.getElementById("top-destinations");
            if (target) {
                const headerOffset = 100; // Adjust this for your navbar height
                const elementPosition = target.getBoundingClientRect().top;
                const offsetPosition = elementPosition + window.pageYOffset - headerOffset;

                window.scrollTo({
                    top: offsetPosition,
                    behavior: "smooth"
                });
            }
        });
    }
});

document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        const target = document.querySelector(this.getAttribute('href'));
        if (target) {
            target.scrollIntoView({
                behavior: 'smooth'
            });
        }
    });
});