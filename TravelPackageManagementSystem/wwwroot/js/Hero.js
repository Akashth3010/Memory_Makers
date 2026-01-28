// --- 1. Destination & Calendar Logic ---
document.addEventListener("DOMContentLoaded", function () {
    // Lock the calendar to today
    const departureInput = document.getElementById('departureDate');
    if (departureInput) {
        const today = new Date().toISOString().split('T')[0];
        departureInput.min = today;
        departureInput.value = today;
    }

    // Fetch Destination Options from Backend
    const destSearch = document.getElementById('destinationSearch');
    if (destSearch) {
        destSearch.addEventListener('input', function () {
            var input = this.value;
            if (input.length < 1) return;

            fetch('/Home/GetSuggestions?term=' + encodeURIComponent(input))
                .then(response => response.json())
                .then(data => {
                    var dataList = document.getElementById('destinationOptions');
                    dataList.innerHTML = '';

                    data.forEach(function (item) {
                        var option = document.createElement('option');
                        option.value = item;
                        dataList.appendChild(option);
                    });
                })
                .catch(err => console.error('Error fetching destinations:', err));
        });
    }
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

    // Clear all error messages when switching
    document.querySelectorAll('.err-msg').forEach(el => el.innerText = "");

    if (isCurrentlySignup) {
        // Switch to LOGIN mode
        signupFields.forEach(f => f.style.setProperty('display', 'none', 'important'));
        modalTitle.innerText = "Welcome Back";
        emailLabel.innerText = "Username or Email";
        emailInput.placeholder = "Enter your username or email";
        submitBtn.innerText = "Login →";
        toggleText.innerText = "Don't have an account? ";
        toggleLink.innerText = "Sign Up";
    } else {
        // Switch to SIGNUP mode
        signupFields.forEach(f => f.style.setProperty('display', 'block', 'important'));
        modalTitle.innerText = "Create an Account";
        emailLabel.innerText = "Email Address";
        emailInput.placeholder = "Enter your email";
        submitBtn.innerText = "Sign Up →";
        toggleText.innerText = "Already have an account? ";
        toggleLink.innerText = "Login";
    }
}

// --- 3. Form Submission & Global Event Listeners ---
document.addEventListener("DOMContentLoaded", function () {
    const authForm = document.getElementById('authForm');

    if (authForm) {
        authForm.addEventListener('submit', async function (e) {
            e.preventDefault();

            // Re-identify submitBtn inside the event to get current state
            const submitBtn = document.getElementById('submitBtn');
            const isLogin = submitBtn.innerText.includes("Login");

            const sharedValue = document.getElementById('authEmail').value;
            const passwordValue = document.getElementById('authPassword').value;

            let payload = {};
            let endpoint = isLogin ? '/Account/Login' : '/Account/Register';

            if (isLogin) {
                payload = {
                    Username: sharedValue,
                    Password: passwordValue
                };
            } else {
                // REGISTER: Grab all fields including regPhone
                const regUsername = document.getElementById('regUsername').value;
                const regPhoneValue = document.getElementById('regPhone').value;
                const confirmPassword = document.getElementById('authConfirmPassword').value;

                payload = {
                    Username: regUsername,
                    Email: sharedValue,
                    PhoneNumber: regPhoneValue, // Correctly mapped for C# ViewModel
                    Password: passwordValue,
                    ConfirmPassword: confirmPassword
                };
            }

            console.log("Attempting to send payload:", payload);

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
                    displayErrors(result);
                }
            } catch (error) {
                console.error("Fetch error:", error);
            }
        });
    }

    // Glow Effect Logic
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
        // Match span ID: err-PhoneNumber, err-Email, etc.
        const span = document.getElementById(`err-${key}`) || document.getElementById(`err-${key}-Reg`);
        if (span) {
            const errorData = errors[key];
            span.innerText = Array.isArray(errorData) ? errorData[0] : errorData;
            span.style.display = "block";
        } else {
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
                // CLEAR LOCAL STORAGE ON LOGOUT
                localStorage.clear();
                window.location.href = '/';
            }
        } catch (error) {
            console.error("Logout error:", error);
            window.location.reload();
        }
    }
}