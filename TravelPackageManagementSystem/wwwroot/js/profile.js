// Toggle the visibility of the menu
function toggleMenu() {
    const menu = document.getElementById('profileMenu');
    menu.style.display = (menu.style.display === 'block') ? 'none' : 'block';
}

// Close menu if user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.profile-trigger') && !event.target.closest('.profile-trigger')) {
        document.getElementById('profileMenu').style.display = 'none';
    }
}

// Extra Feature: Logout logic
function logoutUser() {
    if (confirm("Are you sure you want to logout?")) {
        // Add your logout logic here
        console.log("User logged out");
        window.location.href = "/login";
    }
}