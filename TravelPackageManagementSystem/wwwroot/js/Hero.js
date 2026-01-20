    // 1. Lock the calendar to today
    const dateInput = document.getElementById('departureDate');
    const today = new Date().toISOString().split('T')[0];
    dateInput.min = today;
    dateInput.value = today;

    // 2. Fetch Destination Options from Backend
    document.getElementById('destinationSearch').addEventListener('input', function () {
        var input = this.value;
    if (input.length < 1) return; // Trigger after first character

    fetch('/Home/GetSuggestions?term=' + encodeURIComponent(input))
            .then(response => response.json())
            .then(data => {
                var dataList = document.getElementById('destinationOptions');
    dataList.innerHTML = ''; // Clear old options

    data.forEach(function (item) {
                    var option = document.createElement('option');
    option.value = item;
    dataList.appendChild(option);
                });
            })
            .catch(err => console.error('Error fetching destinations:', err));
    });
// --- 2. Auth Toggle Logic ---
function handleAuthToggle() {
    const signupFields = document.querySelectorAll('.signup-only');
    const modalTitle = document.getElementById('modalTitle');
    const submitBtn = document.getElementById('submitBtn');
    const toggleText = document.getElementById('toggleText');
    const toggleLink = document.getElementById('toggleAuth');
    const emailLabel = document.getElementById('emailLabel');
    const emailInput = document.getElementById('authEmail');

    const isCurrentlySignup = submitBtn.innerText.includes("Sign Up");

    // IMPORTANT: Clear all error messages when switching
    document.querySelectorAll('.err-msg').forEach(el => el.innerText = "");

    if (isCurrentlySignup) {
        // Switch to LOGIN mode
        signupFields.forEach(f => f.style.setProperty('display', 'none', 'important'));
        modalTitle.innerText = "Welcome Back";
        emailLabel.innerText = "Username or Email";
        emailInput.placeholder = "Enter your username or email"; // Login Placeholder
        submitBtn.innerText = "Login →";
        toggleText.innerText = "Don't have an account? ";
        toggleLink.innerText = "Sign Up";
    } else {
        // Switch to SIGNUP mode
        signupFields.forEach(f => f.style.setProperty('display', 'block', 'important'));
        modalTitle.innerText = "Create an Account";
        emailLabel.innerText = "Email Address";
        emailInput.placeholder = "Enter your email"; // Register Placeholder
        submitBtn.innerText = "Sign Up →";
        toggleText.innerText = "Already have an account? ";
        toggleLink.innerText = "Login";
    }
}
// --- 3. Form Submission & Global Event Listeners ---
document.addEventListener("DOMContentLoaded", function () {
    // A. Setup Date Input
    const dateInput = document.getElementById('dateInput');
    if (dateInput) {
        const today = new Date().toISOString().split('T')[0];
        dateInput.min = today;
        dateInput.value = today;
    }

    // B. Handle Form Submission
    const authForm = document.getElementById('authForm');
    if (authForm) {
        authForm.addEventListener('submit', async function (e) {
            e.preventDefault();

            const isLogin = submitBtn.innerText.includes("Login");
            const sharedValue = document.getElementById('authEmail').value; // The input field
            const passwordValue = document.getElementById('authPassword').value;

            let payload = {};
            let endpoint = isLogin ? '/Account/Login' : '/Account/Register';

            if (isLogin) {
                payload = {
                    Username: sharedValue, // Login uses 'Username' property
                    Password: passwordValue
                };
            } else {
                // REGISTER: We need Username, Email, and Password
                const regUsername = document.getElementById('regUsername').value;
                const confirmPassword = document.getElementById('authConfirmPassword').value;

                payload = {
                    Username: regUsername,
                    Email: sharedValue,    // Register uses 'Email' property
                    Password: passwordValue,
                    ConfirmPassword: confirmPassword
                };
            }

            try {
                const response = await fetch(endpoint, {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(payload)
                });

                const result = await response.json();

                if (response.ok) {
                    window.location.href = "/";
                } else {
                    // This is the CRITICAL fix: 
                    // If the server returns 400 (Validation Error), display them!
                    displayErrors(result);
                }
            } catch (error) {
                // Only alert if the fetch actually fails (network down)
                console.error("Fetch error:", error);
            }
        });
    }

    // C. Glow Effect Logic
    document.addEventListener('mousemove', (e) => {
        const authModal = document.getElementById('authModal');
        if (!authModal || !authModal.classList.contains('show')) return;

        const containers = authModal.querySelectorAll('.input-glow-container');
        containers.forEach(container => {
            const rect = container.getBoundingClientRect();
            container.style.setProperty('--mouse-x', `${e.clientX - rect.left}px`);
            container.style.setProperty('--mouse-y', `${e.clientY - rect.top}px`);
        });
    });
});

// --- 4. Error Display ---
function displayErrors(errors) {
    // Clear all previous errors
    document.querySelectorAll('.err-msg').forEach(el => el.innerText = "");

    if (!errors) return;

    Object.keys(errors).forEach(key => {
        // Try various ID formats used in your HTML
        const span = document.getElementById(`err-${key}`) || document.getElementById(`err-${key}-Reg`);
        if (span) {
            //span.innerText = Array.isArray(errors[key]) ? errors[key][0] : errors[key];
            const errorData = errors[key];
            span.innerText = Array.isArray(errorData) ? errorData[0] : errorData;

            span.style.display = "block";
            Console.log(`Setting error for ${key}: ${message}`);
        }
        else {
            console.warn(`Could not find span with id: err-${key}`);
        }
    });
}

// --- 5. Logout Function ---
async function handleLogout() {
    if (confirm("Are you sure you want to logout?")) {
        try {
            const response = await fetch('/Account/Logout', { method: 'POST' });
            if (response.ok) {
                window.location.href = '/';
            }
        } catch (error) {
            console.error("Logout error:", error);
            // Fallback: Force refresh
            window.location.reload();
        }
    }
}