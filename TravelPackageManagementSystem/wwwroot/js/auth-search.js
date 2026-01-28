document.addEventListener('DOMContentLoaded', function () {

    // --- 1. SEARCH LOGIC ---
    const searchForm = document.getElementById('searchForm');
    searchForm?.addEventListener('submit', function (e) {
        e.preventDefault();
        executeSearch();
    });

    function executeSearch() {
        const destination = (document.getElementById('destInput') || {}).value?.toLowerCase()?.trim() || '';
        const travelDate = (document.getElementById('dateInput') || {}).value || '';
        const travellers = (document.getElementById('travellerInput') || {}).value || '';

        if (!destination) {
            alert('Please enter a destination to search.');
            return;
        }

        if (destination.includes('meghalaya')) {
            window.location.href = `/Home/MeghalayaTD?date=${encodeURIComponent(travelDate)}&guests=${encodeURIComponent(travellers)}`;
        } else {
            alert("We currently only have packages available for 'Meghalaya'.");
        }
    }

    // --- 2. DATE INPUT SETUP ---
    const dateInput = document.getElementById('dateInput');
    if (dateInput) {
        const today = new Date();
        const formattedToday = today.toISOString().split('T')[0];
        dateInput.min = formattedToday;
        dateInput.value = formattedToday;
    }

    // --- 3. AUTH MODAL TOGGLING (Login/Signup) ---
    const toggleLink = document.getElementById('toggleAuth');
    const submitBtn = document.getElementById('submitBtn');
    const modalTitle = document.getElementById('modalTitle');
    const modalSubtitle = document.getElementById('modalSubtitle');
    const toggleText = document.getElementById('toggleText');
    const signupFields = document.querySelectorAll('.signup-only');
    const authForm = document.getElementById('authForm');

    function switchToLogin() {
        signupFields.forEach(f => f.style.display = 'none');
        modalTitle.innerText = 'Welcome Back';
        modalSubtitle.innerText = 'Log in to access your account.';
        submitBtn.innerText = 'Login →';
        toggleText.innerText = "Don't have an account?";
        toggleLink.innerText = 'Sign Up';
    }

    function switchToSignup() {
        signupFields.forEach(f => f.style.display = '');
        modalTitle.innerText = 'Create an Account';
        modalSubtitle.innerText = 'Join EasySafar to start your journey.';
        submitBtn.innerText = 'Sign Up →';
        toggleText.innerText = 'Already have an account?';
        toggleLink.innerText = 'Login';
    }

    // Initialize state
    if (toggleLink) switchToSignup();

    toggleLink?.addEventListener('click', function () {
        if (toggleLink.innerText.trim() === 'Login') switchToLogin();
        else switchToSignup();
    });

    // --- 4. FORM SUBMISSION (Mock Authentication) ---
    authForm?.addEventListener('submit', function (e) {
        e.preventDefault();
        const fd = new FormData(this);
        const email = (fd.get('email') || '').toString().trim();
        const password = (fd.get('password') || '').toString();
        const isLogin = submitBtn.innerText.includes('Login');

        if (!email || !password) { alert('Please fill required fields.'); return; }

        if (isLogin) {
            const saved = localStorage.getItem('user');
            if (!saved) { alert('No user exists. Please sign up.'); return; }
            const u = JSON.parse(saved);
            if (u && u.email === email && u.password === password) {
                localStorage.setItem('isLoggedIn', 'true');
                location.reload();
            } else {
                alert('Invalid credentials.');
            }
        } else {
            const firstName = (fd.get('firstName') || '').toString().trim();
            const lastName = (fd.get('lastName') || '').toString().trim();
            const confirmPassword = (fd.get('confirmPassword') || '').toString();
            if (!firstName || !lastName) { alert('Please provide full name.'); return; }
            if (password !== confirmPassword) { alert('Passwords do not match.'); return; }

            const user = { firstName, lastName, email, password };
            localStorage.setItem('user', JSON.stringify(user));
            localStorage.setItem('isLoggedIn', 'true');
            location.reload();
        }
    });

    // --- 5. LOGGED-IN USER MENU LOGIC ---
    (function renderCompactUser() {
        const authSection = document.getElementById('authSection');
        if (!authSection) return;

        const savedUserJson = localStorage.getItem('user');
        const isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
        const user = savedUserJson ? JSON.parse(savedUserJson) : null;

        if (isLoggedIn && user) {
            const initial = (user.firstName?.charAt(0) || user.email?.charAt(0) || 'U').toUpperCase();

            // Inject the Dropdown HTML
            authSection.innerHTML = `
                <div class="user-dropdown" id="userDropdownRoot" style="position:relative;">
                    <button id="userToggleBtn" class="user-toggle-avatar" type="button">${initial}</button>
                    
                    <div class="user-menu-compact" id="userMenu">
                        <div class="user-menu-hint">You are viewing your personal profile</div>
                        
                        <div class="user-menu-item" id="btn-go-profile">
                            <div class="user-menu-icon"><i class="fa-regular fa-user"></i></div>
                            <div style="flex:1">
                                <p class="user-menu-title">My Profile</p>
                                <p class="user-menu-desc">Manage your profile and login details</p>
                            </div>
                        </div>
                        
                        <div class="user-menu-item" id="btn-go-wishlist">
                            <div class="user-menu-icon"><i class="fa-regular fa-heart"></i></div>
                            <div style="flex:1">
                                <p class="user-menu-title">Wishlist</p>
                                <p class="user-menu-desc">Saved favorites and experiences</p>
                            </div>
                        </div>
                        
                        <div class="user-menu-footer">
                            <button id="logoutBtn" class="btn btn-outline-secondary btn-sm">Logout</button>
                        </div>
                    </div>
                </div>`;

            // Element Selectors
            const root = document.getElementById('userDropdownRoot');
            const toggle = document.getElementById('userToggleBtn');
            const menu = document.getElementById('userMenu');
            const profileBtn = document.getElementById('btn-go-profile');
            const wishlistBtn = document.getElementById('btn-go-wishlist');
            const logoutBtn = document.getElementById('logoutBtn');

            // --- Toggle Menu Visibility ---
            toggle.addEventListener('click', (ev) => {
                ev.stopPropagation();
                menu.classList.toggle('show');
            });

            // --- Navigation Handlers ---
            profileBtn.addEventListener('click', () => {
                window.location.href = '/User/Profile'; // Adjust if your controller is different
            });

            wishlistBtn.addEventListener('click', () => {
                window.location.href = '/User/Wishlist'; // Adjust if your controller is different
            });

            // --- Logout Handler ---
            logoutBtn.addEventListener('click', () => {
                if (confirm('Are you sure you want to logout?')) {
                    localStorage.removeItem('isLoggedIn');
                    location.reload();
                }
            });

            // --- Close menu when clicking outside ---
            document.addEventListener('click', (ev) => {
                if (!root.contains(ev.target)) {
                    menu.classList.remove('show');
                }
            });
        }
    })();
});